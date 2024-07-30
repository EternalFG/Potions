using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Serializable
{
	public class SRateLootPotion : SPotion
	{
		public int PlusLootDropped { get; set; }
		public float Duration { get; set; }
		public override IPotion Wrap()
		{
			return new RateLootPotion(ItemId, PlusLootDropped, Duration);
		}
	}
}
