﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;

namespace OpenTibia.Game.Commands
{
    public class ParseUseItemWithItemFromTileToTileCommand : ParseUseItemWithItemCommand
    {
        public ParseUseItemWithItemFromTileToTileCommand(Player player, Position fromPosition, byte fromIndex, ushort fromItemId, Position toPosition, byte toIndex, ushort toItemId) : base(player)
        {
            FromPosition = fromPosition;

            FromIndex = fromIndex;

            FromItemId = fromItemId;

            ToPosition = toPosition;

            ToIndex = toIndex;

            ToItemId = toItemId;
        }
        
        public Position FromPosition { get; set; }

        public byte FromIndex { get; set; }

        public ushort FromItemId { get; set; }

        public Position ToPosition { get; set; }

        public byte ToIndex { get; set; }

        public ushort ToItemId { get; set; }

        public override Promise Execute()
        {
            Tile fromTile = Context.Server.Map.GetTile(FromPosition);

            if (fromTile != null)
            {
                if (Player.Tile.Position.CanSee(fromTile.Position) )
                {
                    Item fromItem = Player.Client.GetContent(fromTile, FromIndex) as Item;

                    if (fromItem != null && fromItem.Metadata.TibiaId == FromItemId)
                    {
                        Tile toTile = Context.Server.Map.GetTile(ToPosition);

                        if (toTile != null)
                        {
                            switch (Player.Client.GetContent(toTile, ToIndex) )
                            {
                                case Item toItem:

                                    if (toItem.Metadata.TibiaId == ToItemId)
                                    {
                                        if ( IsUseable(fromItem) )
                                        {
                                            return Context.AddCommand(new PlayerUseItemWithItemCommand(Player, fromItem, toItem) );
                                        }
                                    }

                                    break;

                                case Creature toCreature:

                                    if (ToItemId == 99)
                                    {
                                        if ( IsUseable(fromItem) )
                                        {
                                            return Context.AddCommand(new PlayerUseItemWithCreatureCommand(Player, fromItem, toCreature) );
                                        }
                                    }

                                    break;
                            }
                        }
                    }
                }
            }

            return Promise.Break;
        }
    }
}