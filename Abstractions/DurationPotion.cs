using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Abstractions
{
	public abstract class DurationPotion : IPotion
	{
		public ushort ItemId { get; }

		public float PerTime { get; protected set; }
		public float Duration { get; protected set; }

		public DurationPotion(ushort itemId, float perTime, float duration)
		{
			ItemId = itemId;
			PerTime = perTime;
			Duration = duration;
		}

		public abstract void Apply(PotionPlayerComponent player);
		public virtual void End(PotionPlayerComponent player) { }


		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}
