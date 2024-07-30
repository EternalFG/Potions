using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PotionsMaking.Services
{
	public static class BoilerHelper
	{
		public static void RemoveAllItems(this InteractableStorage interactableStorage)
		{
			for (int i = interactableStorage.items.items.Count - 1; i >= 0; i--)
			{
				interactableStorage.items.removeItem((byte)i);
			}
		}
	}
}
