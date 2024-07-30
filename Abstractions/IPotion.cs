using PotionsMaking.Models;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Abstractions
{
	public interface IPotion : ICloneable
	{
		ushort ItemId { get; }
		void Apply(PotionPlayerComponent player);
	}
}
