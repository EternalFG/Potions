using PotionsMaking.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RocketLogger = Rocket.Core.Logging.Logger;

namespace PotionsMaking.Services
{
	public class PotionsService
	{
		private readonly PotionsMakingConfiguration m_configuration;
		public readonly Dictionary<ushort, IPotion> _potions;

		public PotionsService(PotionsMakingConfiguration configuration)
		{
			m_configuration = configuration;


			_potions = new Dictionary<ushort, IPotion>(m_configuration.Potions.Count);

			foreach (var spotion in m_configuration.Potions)
			{
				if (_potions.ContainsKey(spotion.ItemId))
				{
					RocketLogger.LogWarning($"A Potion with the same {spotion.ItemId}ID already exists!");
					continue;
				}

				_potions.Add(spotion.ItemId, spotion.Wrap());

			}
		}

		public IPotion CreatePotionByItemId(ushort id)
		{
			if (_potions.TryGetValue(id, out var potion))
			{
				return (IPotion)potion.Clone();
			}
			return null;
		}

		public IPotion GetPotionByItemId(ushort id)
		{
			if (_potions.TryGetValue(id, out var result))
			{
				return result;
			}
			return null;
		}
	}
}
