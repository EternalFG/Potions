using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Serializable
{
	public class SArmorPotion : SPotion
	{
		public float DamageResistancePercentage { get; set; }
		public float Duration { get; set; }
		public override IPotion Wrap()
		{
			return new ArmorPotion(ItemId, DamageResistancePercentage, Duration);
		}
	}
}
