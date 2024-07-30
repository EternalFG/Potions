using HarmonyLib;
using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using PotionsMaking.Services;
using Rocket.API.Extensions;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RocketLogger = Rocket.Core.Logging.Logger;

namespace PotionsMaking
{
	public class PotionsMaking : RocketPlugin<PotionsMakingConfiguration>
	{
		public static PotionsMaking Instance { get; private set; }

		private PotionsService _potionsService;
		public PotionsCraftService PotionsCraftService;
		private System.Random _random;
		private Harmony _harmony;

		protected override void Load()
		{
			Instance = this;
			_harmony = new Harmony("Potion");
			_harmony.PatchAll();

			_potionsService = new(Configuration.Instance);
			PotionsCraftService = new(Configuration.Instance, _potionsService);
			_random = new System.Random();
			PlayerAnimator.OnGestureChanged_Global += OnGestureChanged;
			UseableConsumeable.onConsumePerformed += onConsumePerformed;
			Level.onPostLevelLoaded += onPostLevelLoaded;
			BarricadeManager.onDeployBarricadeRequested += onDeployBarricade;
			BarricadeManager.onBarricadeSpawned += onBarricadeSpawned;
			DamageTool.damagePlayerRequested += damagePlayerRequested;
		}


		protected override void Unload()
		{
			DamageTool.damagePlayerRequested -= damagePlayerRequested;
			BarricadeManager.onDeployBarricadeRequested -= onDeployBarricade;
			Level.onPostLevelLoaded -= onPostLevelLoaded;
			UseableConsumeable.onConsumePerformed -= onConsumePerformed;
			PlayerAnimator.OnGestureChanged_Global -= OnGestureChanged;
			BarricadeManager.onBarricadeSpawned -= onBarricadeSpawned;
			_harmony.UnpatchAll("Potion");
			Instance = null;
		}

		private void damagePlayerRequested(ref DamagePlayerParameters parameters, ref bool shouldAllow)
		{
			if (parameters.player.TryGetComponent(out PotionPlayerComponent component)) /// mb govnokod
			{
				if (component.AttackEvasionPercentage != null)
				{
					if (_random.NextDouble() <= component.AttackEvasionPercentage)
					{
						shouldAllow = false;
						return;
					}
				}

				if (component.DamageResistancePercentage != null)
				{
					parameters.damage -= parameters.damage * (float)component.DamageResistancePercentage;
				}
			}

		}

		private void onBarricadeSpawned(BarricadeRegion region, BarricadeDrop drop)
		{
			if (drop.asset.id == Configuration.Instance.BoilerItemID)
			{
				drop.model.gameObject.TryAddComponent<PotionBoiler>();
			}
		}
		private void onDeployBarricade(Barricade barricade, ItemBarricadeAsset asset, Transform hit, ref Vector3 point, ref float angle_x, ref float angle_y, ref float angle_z, ref ulong owner, ref ulong group, ref bool shouldAllow)
		{
			if (asset.id == Configuration.Instance.BoilerItemID && hit != null)
			{
				shouldAllow = false;
				UnturnedChat.Say(new CSteamID(owner), "Запрещенно ставить котёл на транспорт!", Color.red);
			}
		}
		private void onPostLevelLoaded(int level)
		{
			foreach (var barricadeRegion in BarricadeManager.BarricadeRegions)
			{
				for (int i = 0; i < barricadeRegion.drops.Count; i++)
				{
					BarricadeDrop drop = barricadeRegion.drops[i];
					if (drop.asset.id == Configuration.Instance.BoilerItemID)
					{
						drop.model.gameObject.TryAddComponent<PotionBoiler>();
					}
				}
			}
		}

		private void onConsumePerformed(Player instigatingPlayer, ItemConsumeableAsset consumeableAsset)
		{
			if (instigatingPlayer == null)
			{
				return;
			}

			if (consumeableAsset == null)
			{
				return;
			}

			var config = Configuration.Instance;

			foreach (var potion in _potionsService._potions)
			{
				if (potion.Key.Equals(consumeableAsset.id))
				{
					var potionPlayerComponent = instigatingPlayer.GetComponent<PotionPlayerComponent>();

					if (potionPlayerComponent == null)
					{
						return;
					}

					potionPlayerComponent.DrinkPotion(potion.Value);
					break;
				}
			}
			
		}


		private void OnGestureChanged(PlayerAnimator arg1, EPlayerGesture arg2)
		{
			if (!arg2.Equals(EPlayerGesture.POINT))
			{
				return;
			}

			var player = arg1.channel.owner.player;
			if (player == null)
			{
				return;
			}

			RaycastInfo info = DamageTool.raycast(new UnityEngine.Ray(player.look.aim.position, player.look.aim.forward), 5f, RayMasks.BARRICADE_INTERACT);
			if (info.transform == null)
			{
				return;
			}

			var barricadeDrop = BarricadeManager.FindBarricadeByRootTransform(info.transform);
			if (barricadeDrop == null)
			{
				return;
			}

			
			if (barricadeDrop.GetServersideData().barricade.asset.id != Configuration.Instance.BoilerItemID)
			{
				return;
			}

			
			info.transform.TryGetComponent(out PotionBoiler component);
			var potionBoiler = component;

			if (potionBoiler == null)
			{
				return;
			}

			if (potionBoiler.Busy)
			{
				UnturnedChat.Say(player.channel.owner.playerID.steamID, "Уже варится зелье!", Color.red);
				return;
			}

			potionBoiler.StartBoiler(player);
		}
	}
}
