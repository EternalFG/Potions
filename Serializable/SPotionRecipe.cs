using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PotionsMaking.Serializable
{
	public class SPotionRecipe
	{
		[XmlAttribute] public ushort PotionId { get; set; }
		[XmlAttribute] public float Duration { get; set; }

		[XmlArrayItem("Ingredients")] public List<ushort> Ingredients { get; set; }
	}
}
