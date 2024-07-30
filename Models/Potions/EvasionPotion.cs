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
	public class EvasionPotion : DurationPotion
	{
		public float AttackEvasionPercentage;
		public EvasionPotion(ushort itemId, float attackEvasionPercentage, float duration) : base(itemId, 0f, duration)
		{
			AttackEvasionPercentage = attackEvasionPercentage;
		}

		public override void Apply(PotionPlayerComponent player)
		{
			player.AttackEvasionPercentage = AttackEvasionPercentage;
		}

		public override void End(PotionPlayerComponent player)
		{
			player.AttackEvasionPercentage = null;
			UnturnedChat.Say(player.Player, $"Действие зелья {nameof(EvasionPotion)} закончилось.", Color.white);
		}
	}
}
