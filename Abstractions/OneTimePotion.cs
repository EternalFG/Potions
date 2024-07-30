using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Abstractions
{
	public abstract class OneTimePotion : IPotion
	{
		public ushort ItemId { get; }

		public OneTimePotion(ushort itemId)
		{
			ItemId = itemId;
		}	

		public abstract void Apply(PotionPlayerComponent player);

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
