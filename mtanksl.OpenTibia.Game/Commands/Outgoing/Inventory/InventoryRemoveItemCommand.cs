﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class InventoryRemoveItemCommand : Command
    {
        public InventoryRemoveItemCommand(Inventory inventory, Item item)
        {
            Inventory = inventory;

            Item = item;
        }

        public Inventory Inventory { get; set; }

        public Item Item { get; set; }

        public override Promise Execute()
        {
            byte slot = Inventory.GetIndex(Item);

            Inventory.RemoveContent(slot);

            Context.AddPacket(Inventory.Player.Client.Connection, new SlotRemoveOutgoingPacket(slot) );

            Context.AddEvent(Inventory.Player, new InventoryRemoveItemEventArgs(Inventory, Item, slot) );

            Context.AddEvent(new InventoryRemoveItemEventArgs(Inventory, Item, slot) );

            return Promise.Completed;
        }
    }
}