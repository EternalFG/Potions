using PotionsMaking.Abstractions;
using PotionsMaking.Models;
using PotionsMaking.Serializable;
using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking
{
	public class PotionsMakingConfiguration : IRocketPluginConfiguration
	{
		public List<SPotion> Potions { get; set; }
		public List<SPotionRecipe> PotionRecipes { get; set; }

		public ushort BoilerItemID { get; set; }
		public ushort BoilerEffectID { get; set; }
		public void LoadDefaults()
		{
			BoilerItemID = 26702;
			BoilerEffectID = 26703;

			Potions = new List<SPotion>()
			{
				new SRegenerationPotion()
				{
					ItemId = 26707,
					HealAmountPerSecond = 5,
					Duration = 240,
				},
				new SSpeedPotion()
				{
					ItemId = 26705,
					Duration = 180,
					NewSpeed = 1.35f,
				},
				new SArmorPotion()
				{
					ItemId = 26714,
					Duration = 120,
					DamageResistancePercentage = 0.3f,
				},
				new SEvasionPotion()
				{
					ItemId = 26709,
					Duration = 180,
					AttackEvasionPercentage = 0.25f
				},
				new SRateLootPotion()
				{
					ItemId = 26713,
					Duration = 300,
					PlusLootDropped = 12
				},
				new SFeatherPotion()
				{
					ItemId = 26710,
					Duration = 240,
					NewGravity = 0.7f
				}
			};

			PotionRecipes = new List<SPotionRecipe>()
			{
				new SPotionRecipe()
				{
					PotionId = 26707,
					Duration = 30,
					Ingredients = new List<ushort>()
					{
						398, 395, 340
					}
				},
				new SPotionRecipe()
				{
					PotionId = 26705,
					Duration = 15,
					Ingredients = new List<ushort>()
					{
						398, 93
					}
				},
				new SPotionRecipe()
				{
					PotionId = 26705,
					Duration = 15,
					Ingredients = new List<ushort>()
					{
						398, 86
					}
				},
				new SPotionRecipe()
				{
					PotionId = 26714,
					Duration = 15,
					Ingredients = new List<ushort>()
					{
						398, 72, 68, 74, 342
					}
				},
				new SPotionRecipe()
				{
					PotionId = 26709,
					Duration = 15,
					Ingredients = new List<ushort>()
					{
						398, 75, 338
					}
				},
				new SPotionRecipe()
				{
					PotionId = 26713,
					Duration = 30,
					Ingredients = new List<ushort>()
					{
						398, 67, 41, 74, 264
					}
				},
				new SPotionRecipe()
				{
					PotionId = 26710,
					Duration = 30,
					Ingredients = new List<ushort>()
					{
						398, 70, 344
					}
				}
			};
		}
	}
}
