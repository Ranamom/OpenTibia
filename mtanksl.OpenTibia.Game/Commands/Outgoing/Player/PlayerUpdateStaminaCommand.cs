﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System;

namespace OpenTibia.Game.Commands
{
    public class PlayerUpdateStaminaCommand : Command
    {
        public PlayerUpdateStaminaCommand(Player player, int stamina)
        {
            Player = player;

            Stamina = (ushort)Math.Max(0, Math.Min(2520, stamina) );
        }

        public Player Player { get; set; }

        public ushort Stamina { get; set; }

        public override Promise Execute()
        {
            if (Player.Vocation == Vocation.Gamemaster)
            {
                return Promise.Completed;
            }

            if (Player.Stamina != Stamina)
            {
                Player.Stamina = Stamina;

                Context.AddPacket(Player.Client.Connection, new SendStatusOutgoingPacket(Player.Health, Player.MaxHealth, Player.Capacity, Player.Experience, Player.Level, Player.LevelPercent, Player.Mana, Player.MaxMana, Player.Skills.MagicLevel, Player.Skills.MagicLevelPercent, Player.Soul, Player.Stamina) );
               
                Context.AddEvent(Player, new PlayerUpdateStaminaEventArgs(Player, Stamina) );

                Context.AddEvent(new PlayerUpdateStaminaEventArgs(Player, Stamina) );
            }

            return Promise.Completed;
        }
    }
}