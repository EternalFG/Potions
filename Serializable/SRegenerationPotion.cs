using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Serializable
{
	public class SRegenerationPotion : SPotion
	{
		public float Duration { get; set; }
		public byte HealAmountPerSecond { get; set; }
		public override IPotion Wrap()
		{
			return new RegenerationPotion(ItemId, Duration, HealAmountPerSecond);
		}
	}
}
