using PotionsMaking.Abstractions;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PotionsMaking.Models.Potions
{
	public class RateLootPotion : DurationPotion
	{
		public int PlusLootDropped;
		public RateLootPotion(ushort itemId, int plusLootDropped, float duration) : base(itemId, 0f, duration)
		{
			PlusLootDropped = plusLootDropped;
		}

		public override void Apply(PotionPlayerComponent player)
		{
			player.PlusLootDropped = PlusLootDropped;
		}

		public override void End(PotionPlayerComponent player)
		{
			player.PlusLootDropped = null;
			UnturnedChat.Say(player.Player, $"Действие зелья {nameof(RateLootPotion)} закончилось.", Color.white);
		}
	}
}
