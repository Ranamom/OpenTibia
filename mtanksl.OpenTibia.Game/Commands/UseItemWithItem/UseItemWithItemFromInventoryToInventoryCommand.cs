﻿using OpenTibia.Common.Objects;

namespace OpenTibia.Game.Commands
{
    public class UseItemWithItemFromInventoryToInventoryCommand : UseItemWithItemCommand
    {
        public UseItemWithItemFromInventoryToInventoryCommand(Player player, byte fromSlot, ushort fromItemId, byte toSlot, ushort toItemId)
        {
            Player = player;

            FromSlot = fromSlot;

            FromItemId = fromItemId;

            ToSlot = toSlot;

            ToItemId = toItemId;
        }

        public Player Player { get; set; }

        public byte FromSlot { get; set; }

        public ushort FromItemId { get; set; }

        public byte ToSlot { get; set; }

        public ushort ToItemId { get; set; }

        public override void Execute(Server server, CommandContext context)
        {
            //Arrange

            Inventory fromInventory = Player.Inventory;

            Item fromItem = fromInventory.GetContent(FromSlot) as Item;

            if (fromItem != null && fromItem.Metadata.TibiaId == FromItemId)
            {
                Inventory toInventory = Player.Inventory;

                Item toItem = toInventory.GetContent(ToSlot) as Item;

                if (toItem != null && toItem.Metadata.TibiaId == ToItemId)
                {
                    //Act

                    base.Execute(server, context);
                }
            }
        }
    }
}