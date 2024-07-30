using PotionsMaking.Abstractions;
using PotionsMaking.Serializable;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PotionsMaking.Models.Potions
{
	public class RegenerationPotion : DurationPotion
	{
		private byte _healAmountPerSecond;
		private uint _counter;
		public RegenerationPotion(ushort itemId, float duration, byte healAmountPerSecond) : base(itemId, 1f, duration)
		{
			_healAmountPerSecond = healAmountPerSecond;
		}

		public override void Apply(PotionPlayerComponent player)
		{
			unchecked
			{
				if (_counter++ % 5 == 0)
				{
					player.Player.Heal(_healAmountPerSecond);
				}
				else
				{
					player.Player.Heal(_healAmountPerSecond, false, false);
				}
			}

		}

		public override void End(PotionPlayerComponent player)
		{
			UnturnedChat.Say(player.Player, $"Действие зелья {nameof(RegenerationPotion)} закончилось.", Color.white);
		}

	}
}
