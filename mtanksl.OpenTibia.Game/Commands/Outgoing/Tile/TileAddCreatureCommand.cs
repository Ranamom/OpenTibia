﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class TileAddCreatureCommand : Command
    {
        public TileAddCreatureCommand(Tile tile, Creature creature)
        {
            Tile = tile;

            Creature = creature;
        }

        public Tile Tile { get; set; }

        public Creature Creature { get; set; }

        public override Promise Execute()
        {
            byte index = Tile.AddContent(Creature);

            Context.Server.Map.AddObserver(Tile.Position, Creature);

            foreach (var observer in Context.Server.Map.GetObservers(Tile.Position).OfType<Player>() )
            {
                if (observer != Creature)
                {
                    byte clientIndex;

                    if (observer.Client.TryGetIndex(Creature, out clientIndex) )
                    {
                        uint removeId;

                        if (observer.Client.CreatureCollection.IsKnownCreature(Creature.Id, out removeId) )
                        {
                            Context.AddPacket(observer.Client.Connection, new ThingAddOutgoingPacket(Tile.Position, clientIndex, Creature) );
                        }
                        else
                        {
                            Context.AddPacket(observer.Client.Connection, new ThingAddOutgoingPacket(Tile.Position, clientIndex, removeId, Creature) );
                        }
                    }
                }
            }

            Context.AddEvent(new TileAddCreatureEventArgs(Tile, Creature, index) );

            if (Creature is Player player)
            {
                Context.AddEvent(new PlayerLoginEventArgs(Tile, player) );
            }

            return Promise.Completed;
        }
    }
}