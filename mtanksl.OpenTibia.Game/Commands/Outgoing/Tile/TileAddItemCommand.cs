﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class TileAddItemCommand : Command
    {
        public TileAddItemCommand(Tile tile, Item item)
        {
            Tile = tile;

            Item = item;
        }

        public Tile Tile { get; set; }

        public Item Item { get; set; }

        public override Promise Execute()
        {
            byte index = Tile.AddContent(Item);

            foreach (var observer in Context.Server.Map.GetObservers(Tile.Position).OfType<Player>() )
            {
                byte clientIndex;

                if (observer.Client.TryGetIndex(Item, out clientIndex) )
                {
                    Context.AddPacket(observer.Client.Connection, new ThingAddOutgoingPacket(Tile.Position, clientIndex, Item) );
                }
            }
            
            Context.AddEvent(new TileAddItemEventArgs(Tile, Item, index) );

            return Promise.Completed;
        }
    }
}