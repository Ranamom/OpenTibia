﻿using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.EventHandlers;
using OpenTibia.Game.Events;
using System.Collections.Generic;

namespace OpenTibia.Game.CommandHandlers
{
    public class CampfireHandler : EventHandler<TileAddCreatureEventArgs>
    {
        private HashSet<ushort> campfires = new HashSet<ushort>() { 1423, 1424, 1425 };

        public override Promise Handle(TileAddCreatureEventArgs e)
        {
            foreach (var topItem in e.Tile.GetItems() )
            {
                if (campfires.Contains(topItem.Metadata.OpenTibiaId) )
                {
                    return Context.AddCommand(new CombatConditionCommand(null, e.Creature, SpecialCondition.Burning, MagicEffectType.FirePlume, new[] { -20, -10, -10, -10, -10, -10, -10, -10 }, 2000) );
                }
            }

            return Promise.Completed;
        }
    }
}