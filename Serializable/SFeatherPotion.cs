using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Rendering;

namespace PotionsMaking.Serializable
{
	public class SFeatherPotion : SPotion
	{
		public float NewGravity { get; set; }
		public float Duration { get; set; }
		public override IPotion Wrap()
		{
			return new FeatherPotion(ItemId, NewGravity, Duration);
		}
	}
}
