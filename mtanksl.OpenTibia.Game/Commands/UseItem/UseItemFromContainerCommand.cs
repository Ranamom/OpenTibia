﻿using OpenTibia.Common.Objects;

namespace OpenTibia.Game.Commands
{
    public class UseItemFromContainerCommand : UseItemCommand
    {
        public UseItemFromContainerCommand(Player player, byte fromContainerId, byte fromContainerIndex, ushort itemId, byte containerId) : base(player)
        {
            FromContainerId = fromContainerId;

            FromContainerIndex = fromContainerIndex;

            ItemId = itemId;

            ContainerId = containerId;
        }

        public byte FromContainerId { get; set; }

        public byte FromContainerIndex { get; set; }

        public ushort ItemId { get; set; }

        public byte ContainerId { get; set; }

        public override void Execute(Context context)
        {
            Container fromContainer = Player.Client.ContainerCollection.GetContainer(FromContainerId);

            if (fromContainer != null)
            {
                Item fromItem = fromContainer.GetContent(FromContainerIndex) as Item;

                if (fromItem != null && fromItem.Metadata.TibiaId == ItemId)
                {
                    UseItem(fromItem, FromContainerId, ContainerId, context);
                }
            }
        }
    }
}