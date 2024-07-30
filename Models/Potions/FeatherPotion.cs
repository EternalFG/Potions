using PotionsMaking.Abstractions;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RocketLogger = Rocket.Core.Logging.Logger;

namespace PotionsMaking.Models.Potions
{
	public class FeatherPotion : DurationPotion
	{
		private float? _firstGravity = 1;
		public float NewGravity;
		public FeatherPotion(ushort itemId, float newGravity, float duration) : base(itemId, 0f, duration)
		{
			NewGravity = newGravity;
		}


		public override void Apply(PotionPlayerComponent player)
		{
			player.Player.Player.movement.sendPluginGravityMultiplier(NewGravity);
		}

		public override void End(PotionPlayerComponent player)
		{
			player.Player.Player.movement.sendPluginGravityMultiplier((float)_firstGravity);
			UnturnedChat.Say(player.Player, $"Действие зелья {nameof(FeatherPotion)} закончилось.", Color.white);
		}
	}
}
