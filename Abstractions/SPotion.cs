using PotionsMaking.Serializable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PotionsMaking.Abstractions
{
	[XmlInclude(typeof(SRegenerationPotion))]
	[XmlInclude(typeof(SSpeedPotion))]
	[XmlInclude(typeof(SArmorPotion))]
	[XmlInclude(typeof(SEvasionPotion))]
	[XmlInclude(typeof(SRateLootPotion))]
	[XmlInclude(typeof(SFeatherPotion))]
	public abstract class SPotion
	{
		[XmlAttribute] public ushort ItemId { get; set; }

		public abstract IPotion Wrap();
	}
}
