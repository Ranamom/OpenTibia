﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Commands;
using System;
using System.Collections.Generic;

namespace OpenTibia.Game.CommandHandlers
{
    public class LumpOfChocolateDoughHandler : CommandHandler<PlayerUseItemWithItemCommand>
    {
        private HashSet<ushort> lumpOfChocolateDough = new HashSet<ushort>() { 8846 };

        private HashSet<ushort> ovens = new HashSet<ushort>() { 1786, 1788, 1790, 1792, 6356, 6358, 6360, 6362 };

        private ushort chocolateCake = 8847;

        public override Promise Handle(Func<Promise> next, PlayerUseItemWithItemCommand command)
        {
            if (lumpOfChocolateDough.Contains(command.Item.Metadata.OpenTibiaId) && ovens.Contains(command.ToItem.Metadata.OpenTibiaId) )
            {
                return Context.AddCommand(new ItemDecrementCommand(command.Item, 1) ).Then( () =>
                {
                    return Context.AddCommand(new TileIncrementOrCreateItemCommand( (Tile)command.ToItem.Parent, chocolateCake, 1) );
                } );
            }

            return next();
        }
    }
}