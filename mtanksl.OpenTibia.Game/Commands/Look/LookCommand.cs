﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public abstract class LookCommand : Command
    {
        protected void Look(Player player, Item item, Server server, CommandContext context)
        {
            //Act

            //Notify

            context.Write(player.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.GreenCenterGameWindowAndServerLog, "You see nothing special.") );
        }
    }
}