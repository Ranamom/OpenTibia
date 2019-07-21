﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;

namespace OpenTibia.Game.Commands
{
    public class UseItemWithItemFromInventoryToTileCommand : UseItemWithItemCommand
    {
        public UseItemWithItemFromInventoryToTileCommand(Player player, byte fromSlot, ushort fromItemId, Position toPosition, byte toIndex, ushort toItemId)
        {
            Player = player;

            FromSlot = fromSlot;

            FromItemId = fromItemId;

            ToPosition = toPosition;

            ToIndex = ToIndex;

            ToItemId = toItemId;
        }

        public Player Player { get; set; }

        public byte FromSlot { get; set; }

        public ushort FromItemId { get; set; }

        public Position ToPosition { get; set; }

        public byte ToIndex { get; set; }

        public ushort ToItemId { get; set; }

        public override void Execute(Server server, CommandContext context)
        {
            //Arrange

            Inventory fromInventory = Player.Inventory;

            Item fromItem = fromInventory.GetContent(FromSlot) as Item;

            if (fromItem != null && fromItem.Metadata.TibiaId == FromItemId)
            {
                Tile toTile = server.Map.GetTile(ToPosition);

                if (toTile != null)
                {
                    Item toItem = toTile.GetContent(ToIndex) as Item;

                    if (toItem != null && toItem.Metadata.TibiaId == ToItemId)
                    {
                        //Act

                        base.Execute(server, context);
                    }
                }
            }
        }
    }
}