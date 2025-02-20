﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;

namespace OpenTibia.Game.Commands
{
    public class ParseUseItemWithItemFromContainerToTileCommand : ParseUseItemWithItemCommand
    {
        public ParseUseItemWithItemFromContainerToTileCommand(Player player, byte fromContainerId, byte fromContainerIndex, ushort fromItemId, Position toPosition, byte toIndex, ushort toItemId) :base(player)
        {
            FromContainerId = fromContainerId;

            FromContainerIndex = fromContainerIndex;

            FromItemId = fromItemId;

            ToPosition = toPosition;

            ToIndex = toIndex;

            ToItemId = toItemId;
        }

        public byte FromContainerId { get; set; }

        public byte FromContainerIndex { get; set; }

        public ushort FromItemId { get; set; }

        public Position ToPosition { get; set; }

        public byte ToIndex { get; set; }

        public ushort ToItemId { get; set; }

        public override Promise Execute()
        {
            Container fromContainer = Player.Client.ContainerCollection.GetContainer(FromContainerId);

            if (fromContainer != null)
            {
                Item fromItem = fromContainer.GetContent(FromContainerIndex) as Item;

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

            return Promise.Break;
        }
    }
}