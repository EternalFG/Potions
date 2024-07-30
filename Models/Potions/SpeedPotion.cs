using PotionsMaking.Abstractions;
using PotionsMaking.Serializable;
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
	public class SpeedPotion : DurationPotion
	{
		private float? _firstSpeed = 1;
		public float _newSpeed;
		public SpeedPotion(ushort itemId, float duration, float newSpeed) : base(itemId, 0f, duration)	 
		{
			_newSpeed = newSpeed;
		}
		public override void Apply(PotionPlayerComponent player)
		{
			player.Player.Player.movement.sendPluginSpeedMultiplier(_newSpeed);
		}

		public override void End(PotionPlayerComponent player)
		{
			player.Player.Player.movement.sendPluginSpeedMultiplier((float)_firstSpeed);
			UnturnedChat.Say(player.Player, $"Действие зелья {nameof(SpeedPotion)} закончилось.", Color.white);
		}

	}
}
