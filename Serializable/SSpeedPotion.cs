using PotionsMaking.Abstractions;
using PotionsMaking.Models.Potions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Serializable
{
	public class SSpeedPotion : SPotion
	{
		public float Duration { get; set; }
		public float NewSpeed { get; set; }

		public override IPotion Wrap()
		{
			return new SpeedPotion(ItemId, Duration, NewSpeed);
		}
	}
}
