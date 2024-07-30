using PotionsMaking.Models;
using PotionsMaking.Services;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RocketLogger = Rocket.Core.Logging.Logger;

namespace PotionsMaking
{
	public class PotionBoiler : MonoBehaviour
	{
		public InteractableStorage Storage;
		public bool Busy = false;

		private PotionsCraftService _potionsCraftService;
		private PotionRecipe PotionRecipe = null;
		private Transform _boilerEffectTransform = null;

		private float _startCraftingPotion = 0;
		private void Awake()
		{
			if (transform.TryGetComponent(out InteractableStorage interactableStorage))
			{
				Storage = interactableStorage;
			}

			_potionsCraftService = PotionsMaking.Instance.PotionsCraftService;
		}

		private void OnDisable()
		{
			if (Busy)
			{
				var barricadeDrop = BarricadeManager.FindBarricadeByRootTransform(_boilerEffectTransform);
				if (barricadeDrop == null)
				{
					return;
				}
				if (BarricadeManager.tryGetRegion(_boilerEffectTransform, out byte x, out byte y, out ushort plant, out _))
				{
					BarricadeManager.destroyBarricade(barricadeDrop, x, y, plant);
				}
				Busy = false;
			}
		}

		private void FixedUpdate()
		{

			if (Busy)
			{
				if (Time.realtimeSinceStartup - _startCraftingPotion >= PotionRecipe.Duration)
				{
					Storage.items.items.Add(new ItemJar(new Item(PotionRecipe.BuildPotion.ItemId, true)));
					var barricadeDrop = BarricadeManager.FindBarricadeByRootTransform(_boilerEffectTransform);

					if (barricadeDrop == null)
					{
						return;
					}

					if (BarricadeManager.tryGetRegion(_boilerEffectTransform, out byte x, out byte y, out ushort plant, out _))
					{
						BarricadeManager.destroyBarricade(barricadeDrop, x, y, plant);
					}

					Busy = false;
					PotionRecipe = null;
				}
			}

		}

		public void StartBoiler(Player player)
		{
			var potionRecipe = _potionsCraftService.GetPotionRecipeFromPotionBoiler(this);
			if (potionRecipe == null)
			{
				return;
			}

			var config = PotionsMaking.Instance.Configuration.Instance;

			Storage.RemoveAllItems();
			_startCraftingPotion = Time.realtimeSinceStartup;
			PotionRecipe = potionRecipe;

			var asset = Assets.find(EAssetType.ITEM, config.BoilerEffectID);
			if (asset != null && asset is ItemBarricadeAsset itemBarricadeAsset) 
			{
				_boilerEffectTransform = BarricadeManager.dropNonPlantedBarricade(new Barricade(itemBarricadeAsset), transform.position, transform.rotation, 0, 0);
			}
			Busy = true;
		}
	}
}
