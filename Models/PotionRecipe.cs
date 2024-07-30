using PotionsMaking.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Models
{
	public class PotionRecipe
	{
		public IPotion BuildPotion;
		public float Duration;
		public ushort[] Ingredients;
	}
}
