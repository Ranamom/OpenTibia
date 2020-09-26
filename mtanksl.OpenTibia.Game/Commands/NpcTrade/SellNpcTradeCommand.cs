﻿using OpenTibia.Common.Objects;
using OpenTibia.Network.Packets.Incoming;

namespace OpenTibia.Game.Commands
{
    public class SellNpcTradeCommand : Command
    {
        public SellNpcTradeCommand(Player player, SellNpcTradeIncommingPacket packet)
        {
            Player = player;

            Packet = packet;
        }

        public Player Player { get; set; }

        public SellNpcTradeIncommingPacket Packet { get; set; }

        public override void Execute(Context context)
        {
            //Arrange

            //Act

            //Notify

            base.Execute(context);
        }
    }
}