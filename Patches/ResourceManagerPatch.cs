using HarmonyLib;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RocketLogger = Rocket.Core.Logging.Logger;

namespace PotionsMaking.Patches
{
	[HarmonyPatch]
	public static class ResourceManagerPatch
	{
		[HarmonyPatch(typeof(ResourceManager), nameof(ResourceManager.damage))]
		[HarmonyPrefix]
		public static bool DamagePatch(ResourceManager __instance, Transform resource, Vector3 direction, float damage, float times, float drop, out EPlayerKill kill, out uint xp, CSteamID instigatorSteamID = default(CSteamID), EDamageOrigin damageOrigin = EDamageOrigin.Unknown, bool trackKill = true)
		{
			xp = 0U;
			kill = EPlayerKill.NONE;
			ushort num = (ushort)(damage * times);
			bool flag = true;
			if (ResourceManager.onDamageResourceRequested != null)
			{
				ResourceManager.onDamageResourceRequested(instigatorSteamID, resource, ref num, ref flag, damageOrigin);
			}
			if (!flag || num < 1)
			{
				return false;
			}
			byte b;
			byte b2;
			if (Regions.tryGetCoordinate(resource.position, out b, out b2))
			{
				List<ResourceSpawnpoint> list = LevelGround.trees[(int)b, (int)b2];
				ushort num2 = 0;
				while ((int)num2 < list.Count)
				{
					if (resource == list[(int)num2].model)
					{
						if (list[(int)num2].isDead || !list[(int)num2].canBeDamaged)
						{
							break;
						}
						list[(int)num2].askDamage(num);
						if (list[(int)num2].isDead)
						{
							kill = EPlayerKill.RESOURCE;
							ResourceAsset asset = list[(int)num2].asset;
							if (list[(int)num2].asset != null)
							{
								if (asset.explosion != 0)
								{
									if (asset.hasDebris)
									{
										EffectManager.sendEffect(asset.explosion, b, b2, ResourceManager.RESOURCE_REGIONS, resource.position + Vector3.up * 8f);
									}
									else
									{
										EffectManager.sendEffect(asset.explosion, b, b2, ResourceManager.RESOURCE_REGIONS, resource.position);
									}
								}
								if (!asset.isForage)
								{
									float num3 = Provider.modeConfigData.Objects.Resource_Drops_Multiplier;
									num3 *= drop;
									if (asset.rewardID != 0)
									{
										direction.y = 0f;
										direction.Normalize();
										int num4 = Mathf.CeilToInt((float)UnityEngine.Random.Range((int)asset.rewardMin, (int)(asset.rewardMax + 1)) * num3);
										num4 = Mathf.Clamp(num4, 0, 100);

										/// mb govnokod
										UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromCSteamID(instigatorSteamID);
										if (unturnedPlayer != null)
										{
											var potionComponent = unturnedPlayer.GetComponent<PotionPlayerComponent>();
											if (potionComponent != null)
											{
												if (potionComponent.PlusLootDropped != null)
												{
													num4 += (int)potionComponent.PlusLootDropped;
												}
											}
										}
										/// mb govnokod
										
										for (int i = 0; i < num4; i++)
										{
											ushort num5 = SpawnTableTool.resolve(asset.rewardID);
											if (num5 != 0)
											{
												if (asset.hasDebris)
												{
													ItemManager.dropItem(new Item(num5, EItemOrigin.NATURE), resource.position + direction * (float)(2 + i) + new Vector3(0f, 2f, 0f), false, true, true);
												}
												else
												{
													ItemManager.dropItem(new Item(num5, EItemOrigin.NATURE), resource.position + new Vector3(UnityEngine.Random.Range(-2f, 2f), 2f, UnityEngine.Random.Range(-2f, 2f)), false, true, true);
												}
											}
										}
									}
									else
									{
										if (asset.log != 0)
										{
											int num6 = Mathf.CeilToInt((float)UnityEngine.Random.Range(3, 7) * num3);
											num6 = Mathf.Clamp(num6, 0, 100);
											for (int j = 0; j < num6; j++)
											{
												ItemManager.dropItem(new Item(asset.log, EItemOrigin.NATURE), resource.position + direction * (float)(2 + j * 2) + Vector3.up, false, true, true);
											}
										}
										if (asset.stick != 0)
										{
											int num7 = Mathf.CeilToInt((float)UnityEngine.Random.Range(2, 5) * num3);
											num7 = Mathf.Clamp(num7, 0, 100);
											for (int k = 0; k < num7; k++)
											{
												float f = UnityEngine.Random.Range(0f, 6.2831855f);
												ItemManager.dropItem(new Item(asset.stick, EItemOrigin.NATURE), resource.position + new Vector3(Mathf.Sin(f) * 3f, 1f, Mathf.Cos(f) * 3f), false, true, true);
											}
										}
									}
									xp = asset.rewardXP;
									Vector3 point = list[(int)num2].point;
									Guid guid = asset.GUID;
									for (int l = 0; l < Provider.clients.Count; l++)
									{
										SteamPlayer steamPlayer = Provider.clients[l];
										if (!(steamPlayer.player == null) && !(steamPlayer.player.movement == null) && !(steamPlayer.player.life == null) && !steamPlayer.player.life.isDead && (steamPlayer.player.transform.position - point).sqrMagnitude < 90000f)
										{
											steamPlayer.player.quests.trackTreeKill(guid);
										}
									}
								}
							}
							ResourceManager.ServerSetResourceDead(b, b2, num2, direction * (float)num);
							return false;
						}
						break;
					}
					else
					{
						num2 += 1;
					}
				}
			}
			return false;
		}
	}
}
