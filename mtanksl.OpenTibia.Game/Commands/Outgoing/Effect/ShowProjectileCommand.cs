﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class ShowProjectileCommand : Command
    {
        public ShowProjectileCommand(Position fromPosition, Position toPosition, ProjectileType projectileType)
        {
            FromPosition = fromPosition;

            ToPosition = toPosition;

            ProjectileType = projectileType;
        }

        public Position FromPosition { get; set; }

        public Position ToPosition { get; set; }

        public ProjectileType ProjectileType { get; set; }

        public override Promise Execute()
        {
            foreach (var observer in Context.Server.Map.GetObservers(ToPosition).OfType<Player>() )
            {
                if (observer.Tile.Position.CanSee(ToPosition) )
                {
                    Context.AddPacket(observer.Client.Connection, new ShowProjectileOutgoingPacket(FromPosition, ToPosition, ProjectileType) );
                }
            }

            return Promise.Completed;
        }
    }
}