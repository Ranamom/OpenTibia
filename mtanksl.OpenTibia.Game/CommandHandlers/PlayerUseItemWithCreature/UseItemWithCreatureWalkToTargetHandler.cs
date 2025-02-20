﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Components;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class UseItemWithCreatureWalkToTargetHandler : CommandHandler<PlayerUseItemWithCreatureCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerUseItemWithCreatureCommand command)
        {
            if ( !command.Player.Tile.Position.IsNextTo(command.ToCreature.Tile.Position) )
            {
                if (command.Item.Parent is Tile || command.Item.Parent is Container container && container.Root() is Tile)
                {
                    return Context.AddCommand(new PlayerMoveItemCommand(command.Player, command.Item, command.Player.Inventory, (byte)Slot.Extra, 1, false) ).Then( () =>
                    {
                        return Context.Server.GameObjectComponents.AddComponent(command.Player, new PlayerActionDelayBehaviour() ).Promise;

                    } ).Then( () =>
                    {
                        if (command.ToCreature.Tile == null)
                        {
                            return Promise.Break;
                        }

                        return Context.AddCommand(new ParseWalkToUnknownPathCommand(command.Player, command.ToCreature.Tile) );

                    } ).Then( () =>
                    {
                        return Context.Server.GameObjectComponents.AddComponent(command.Player, new PlayerActionDelayBehaviour() ).Promise;

                    } ).Then( () =>
                    {
                        Item item = command.Player.Inventory.GetContent( (byte)Slot.Extra) as Item;

                        if (item == null || item.Metadata.OpenTibiaId != command.Item.Metadata.OpenTibiaId)
                        {
                            return Promise.Break;
                        }

                        return Context.AddCommand(new PlayerUseItemWithCreatureCommand(command.Player, item, command.ToCreature) );
                    } );
                }
                else
                {
                    IContainer beforeContainer = command.Item.Parent;

                    byte beforeIndex = beforeContainer.GetIndex(command.Item);

                    return Context.AddCommand(new ParseWalkToUnknownPathCommand(command.Player, command.ToCreature.Tile) ).Then( () =>
                    {
                        return Context.Server.GameObjectComponents.AddComponent(command.Player, new PlayerActionDelayBehaviour() ).Promise;

                    } ).Then( () =>
                    {
                        IContainer afterContainer = command.Item.Parent;

                        if (beforeContainer != afterContainer)
                        {
                            return Promise.Break;
                        }

                        byte afterIndex = afterContainer.GetIndex(command.Item);

                        if (beforeIndex != afterIndex)
                        {
                            return Promise.Break;
                        }

                        return next();
                    } );
                }
            }

            return next();
        }
    }
}