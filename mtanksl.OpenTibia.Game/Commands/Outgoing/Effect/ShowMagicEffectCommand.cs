﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class ShowMagicEffectCommand : Command
    {
        public ShowMagicEffectCommand(Position position, MagicEffectType magicEffectType)
        {
            Position = position;

            MagicEffectType = magicEffectType;
        }

        public Position Position { get; set; }

        public MagicEffectType MagicEffectType { get; set; }

        public override Promise Execute()
        {
            foreach (var observer in Context.Server.Map.GetObservers(Position).OfType<Player>() )
            {
                if (observer.Tile.Position.CanSee(Position) )
                {
                    Context.AddPacket(observer.Client.Connection, new ShowMagicEffectOutgoingPacket(Position, MagicEffectType) );
                }
            }

            return Promise.Completed;
        }
    }
}