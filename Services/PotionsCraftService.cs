using PotionsMaking.Models;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Services
{
	public class PotionsCraftService
	{
		private readonly PotionsService m_potionsService;
		private readonly PotionsMakingConfiguration m_configuration;

		private List<PotionRecipe> _potionsRecipes;

		public PotionsCraftService(PotionsMakingConfiguration configuration, PotionsService potionsService)
		{
			m_configuration = configuration;
			_potionsRecipes = new List<PotionRecipe>();
			m_potionsService = potionsService;

			foreach (var spotionRecipe in m_configuration.PotionRecipes)
			{
				var potion = m_potionsService.GetPotionByItemId(spotionRecipe.PotionId);

				_potionsRecipes.Add(new PotionRecipe()
				{
					BuildPotion = potion,
					Duration = spotionRecipe.Duration,
					Ingredients = spotionRecipe.Ingredients.ToArray()
				});
			}
		}

		public PotionRecipe GetPotionRecipeFromPotionBoiler(PotionBoiler potionBoiler)
		{
			List<ushort> itemsStorage = new List<ushort>();
			for (int i = 0; i < potionBoiler.Storage.items.items.Count; i++)
			{
				ItemJar item = potionBoiler.Storage.items.items[i];
				itemsStorage.Add(item.item.id);
			}
			
			foreach (var potionRecipe in _potionsRecipes)
			{
				if (ItemsContainIngredients(itemsStorage, potionRecipe.Ingredients.ToList()))
				{
					return potionRecipe;
				}
			}
			return null;
		}

		private bool ItemsContainIngredients(List<ushort> items, List<ushort> ingredients)
		{
			return ingredients.All(ingredient => items.Contains(ingredient));
		}
	}
}
