﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using System;
using System.Collections.Generic;

namespace OpenTibia.Game.CommandHandlers
{
    public class FluidItem2Handler : CommandHandler<PlayerUseItemWithItemCommand>
    {
        private HashSet<ushort> drawWell = new HashSet<ushort> { 1368, 1369 };

        private HashSet<ushort> shallowWaters = new HashSet<ushort>() { 4608, 4609, 4610, 4611, 4612, 4613, 4614, 4615, 4616, 4617, 4618, 4619, 4620, 4621, 4622, 4623, 4624, 4625, 4820, 4821, 4822, 4823, 4824, 4825 };

        private HashSet<ushort> swamps = new HashSet<ushort>() { 4691, 4692, 4693, 4694, 4695, 4696, 4697, 4698, 4699, 4700, 4701, 4702, 4703, 4704, 4705, 4706, 4707, 4708, 4709, 4710, 4711, 4712 };

        private HashSet<ushort> lavas = new HashSet<ushort>() { 598, 599, 600, 601 };

        private HashSet<ushort> distillingMachines = new HashSet<ushort>() { 5513, 5514 };

        private HashSet<ushort> waterCask = new HashSet<ushort> { 1771 };

        private HashSet<ushort> beerCask = new HashSet<ushort> { 1772 };

        private HashSet<ushort> wineCask = new HashSet<ushort> { 1773 };

        private HashSet<ushort> lemonadeCask = new HashSet<ushort> { 1776 };

        private HashSet<ushort> rumCask = new HashSet<ushort> { 5539 };

        public override Promise Handle(Func<Promise> next, PlayerUseItemWithItemCommand command)
        {
            FluidItem fromItem = command.Item as FluidItem;

            if (fromItem != null)
            {
                if (fromItem.FluidType == FluidType.Empty)
                {
                    if (drawWell.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Water) );
                    }
                    else if (shallowWaters.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new ShowMagicEffectCommand( ( (Tile)command.ToItem.Parent).Position, MagicEffectType.BlueRings) ).Then( () =>
                        {
                            return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Water) );
                        } );
                    }
                    else if (swamps.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new ShowMagicEffectCommand( ( (Tile)command.ToItem.Parent).Position, MagicEffectType.GreenRings) ).Then( () =>
                        {
                            return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Slime) );
                        } );
                    }
                    else if (lavas.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new ShowMagicEffectCommand( ( (Tile)command.ToItem.Parent).Position, MagicEffectType.FirePlume) ).Then( () =>
                        {
                            return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Lava) );
                        } );
                    }
                    else if (distillingMachines.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Rum) );
                    }
                    else if (waterCask.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Water) );
                    }
                    else if (beerCask.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Beer) );
                    }
                    else if (wineCask.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Wine) );
                    }
                    else if (lemonadeCask.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Lemonade) );
                    }
                    else if (rumCask.Contains(command.ToItem.Metadata.OpenTibiaId) )
                    {
                        return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Rum) );
                    }
                }
                else
                {
                    FluidItem toItem = command.ToItem as FluidItem;

                    if (toItem != null)
                    {
                        if (toItem.FluidType == FluidType.Empty)
                        {
                            return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(toItem, fromItem.FluidType) ).Then( () =>
                            {
                                return Context.AddCommand(new FluidItemUpdateFluidTypeCommand(fromItem, FluidType.Empty) );
                            } );
                        }
                    }
                }
            }

            return next();
        }
    }
}