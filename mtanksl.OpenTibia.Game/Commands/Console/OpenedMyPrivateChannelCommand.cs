﻿using OpenTibia.Common.Objects;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class OpenedMyPrivateChannelCommand : Command
    {
        public OpenedMyPrivateChannelCommand(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        public override void Execute(Server server, CommandContext context)
        {
            //Arrange

            PrivateChannel privateChannel = server.Channels.GetPrivateChannel(Player);

            //Act

            if (privateChannel == null)
            {
                privateChannel = new PrivateChannel()
                {
                    Owner = Player,

                    Name = Player.Name + " Channel"
                };

                server.Channels.AddChannel(privateChannel);
            }

            //Notify

            context.Write(Player.Client.Connection, new OpenMyPrivateChannel(privateChannel.Id, privateChannel.Name) );
        }
    }
}