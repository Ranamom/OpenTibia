﻿using OpenTibia.Common.Objects;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class ParseAddVipCommand : Command
    {
        public ParseAddVipCommand(Player player, string name)
        {
            Player = player;

            Name = name;
        }

        public Player Player { get; set; }

        public string Name { get; set; }

        public override Promise Execute()
        {
            Player player = Context.Server.GameObjects.GetPlayers()
                .Where(p => p.Name == Name)
                .FirstOrDefault();

            if (player != null && player != Player)
            {
                Vip vip = Player.Client.VipCollection.AddVip(player.Name);

                Context.AddPacket(Player.Client.Connection, new VipOutgoingPacket(vip.Id, vip.Name, false) );

                return Promise.Completed;
            }

            return Promise.Break;
        }
    }
}