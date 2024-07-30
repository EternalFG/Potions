using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RocketLogger = Rocket.Core.Logging.Logger;

namespace PotionsMaking
{
	public class PotionPlayerComponent : UnturnedPlayerComponent
	{
		private List<DPotion> _durationPotions = new List<DPotion>();

		private Queue<DPotion> _removeDurationPotions = new Queue<DPotion>();

		public float? DamageResistancePercentage = null;
		public float? AttackEvasionPercentage = null;
		public int? PlusLootDropped = null;

		private class DPotion
		{
			public DurationPotion Potion;
			public float UseTime;
			public float LastApply;
		}

		protected override void Load()
		{

		}

		protected override void Unload()
		{
			
		}
		public void DrinkPotion(IPotion potion)
		{
			if (potion is OneTimePotion oneTimePotion)
			{
				oneTimePotion.Apply(this);
			}
			else if (potion is DurationPotion durationPotion)
			{
				if (durationPotion.PerTime == 0f)
				{
					durationPotion.Apply(this);
				}

				var upotion = _durationPotions.FirstOrDefault(x => x.Potion.ItemId == potion.ItemId);
				if (upotion != null)
				{
					upotion.UseTime = Time.realtimeSinceStartup;
				}
				else
				{
					_durationPotions.Add(new DPotion()
					{
						UseTime = Time.realtimeSinceStartup,
						Potion = durationPotion
					});
				}

				
			}
			else
			{
				RocketLogger.LogWarning($"{potion} not implemented!");
			}
		}
		private void FixedUpdate()
		{
			foreach (var durationPotion in _durationPotions)
			{
				float time = Time.realtimeSinceStartup; 
				
				if (time - durationPotion.UseTime >= durationPotion.Potion.Duration)
				{
					durationPotion.Potion.End(this);
					_removeDurationPotions.Enqueue(durationPotion);
				}
				else if (durationPotion.Potion.PerTime > 0f && time - durationPotion.LastApply >= durationPotion.Potion.PerTime)
				{
					durationPotion.Potion.Apply(this);
					durationPotion.LastApply = time;
				}
			}

			DPotion dPotion;
			while (_removeDurationPotions.Count > 0 && (dPotion = _removeDurationPotions.Dequeue()) != null)
			{
				_durationPotions.Remove(dPotion);
			}
		}
	}
}
