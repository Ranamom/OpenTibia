﻿using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Events;
using System;
using System.Collections.Generic;

namespace OpenTibia.Game.CommandHandlers
{
    public class JungleMawHandler : EventHandlers.EventHandler<TileAddCreatureEventArgs>
    {
        private Dictionary<ushort, ushort> jungleMaws = new Dictionary<ushort, ushort>()
        {
            { 4208, 4209 }
        };

        private Dictionary<ushort, ushort> decay = new Dictionary<ushort, ushort>() 
        {
            { 4209, 4208 }
        };

        public override Promise Handle(TileAddCreatureEventArgs e)
        {
            foreach (var topItem in e.Tile.GetItems() )
            {
                ushort toOpenTibiaId;

                if (jungleMaws.TryGetValue(topItem.Metadata.OpenTibiaId, out toOpenTibiaId) )
                {
                    return Context.AddCommand(new CreatureAttackCreatureCommand(null, e.Creature, new SimpleAttack(null, MagicEffectType.BlackSpark, AnimatedTextColor.DarkRed, -30) ) ).Then( () =>
                    {
                        return Context.AddCommand(new ItemTransformCommand(topItem, toOpenTibiaId, 1) );

                    } ).Then( (item) =>
                    {
                        _ = Context.AddCommand(new ItemDecayTransformCommand(item, TimeSpan.FromSeconds(10), decay[toOpenTibiaId], 1) );

                        return Promise.Completed;
                    } );
                }
            }

            return Promise.Completed;
        }
    }
}