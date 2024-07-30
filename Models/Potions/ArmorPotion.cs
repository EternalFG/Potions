using PotionsMaking.Abstractions;
using PotionsMaking.Serializable;
using Rocket.Unturned.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PotionsMaking.Models.Potions
{
	public class ArmorPotion : DurationPotion
	{
		public float DamageResistancePercentage;

		public ArmorPotion(ushort itemId, float damageResistancePercentage, float duration) : base(itemId, 0f, duration)
		{
			DamageResistancePercentage = damageResistancePercentage;
		}
		public override void Apply(PotionPlayerComponent player)
		{
			player.DamageResistancePercentage = DamageResistancePercentage;
		}

		public override void End(PotionPlayerComponent player)
		{
			player.DamageResistancePercentage = null;
			UnturnedChat.Say(player.Player, $"Действие зелья {nameof(ArmorPotion)} закончилось.", Color.white);
		}

	}
}
