﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;

namespace OpenTibia.Game.Commands
{
    public class UseItemFromTileCommand : UseItemCommand
    {
        public UseItemFromTileCommand(Player player, Position fromPosition, byte fromIndex, ushort itemId) : base(player)
        {
            FromPosition = fromPosition;

            FromIndex = fromIndex;

            ItemId = itemId;
        }

        public Position FromPosition { get; set; }

        public byte FromIndex { get; set; }

        public ushort ItemId { get; set; }

        public override void Execute(Context context)
        {
            Tile fromTile = context.Server.Map.GetTile(FromPosition);

            if (fromTile != null)
            {
                Item fromItem = fromTile.GetContent(FromIndex) as Item;

                if (fromItem != null && fromItem.Metadata.TibiaId == ItemId)
                {    
                    UseItem(context, fromItem, null);
                }
            }
        }
    }
}