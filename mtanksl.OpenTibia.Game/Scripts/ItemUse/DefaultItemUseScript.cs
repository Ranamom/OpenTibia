﻿using OpenTibia.Common.Objects;
using OpenTibia.Game.Commands;
using System.Collections.Generic;

namespace OpenTibia.Game.Scripts
{
    public class DefaultItemUseScript : IItemUseScript
    {
        private Dictionary<ushort, ushort> items = new Dictionary<ushort, ushort>()
        {
            { 6356, 6357 },
            { 6357, 6356 },
            { 6358, 6359 },
            { 6359, 6358 },
            { 6360, 6361 },
            { 6361, 6360 },
            { 6362, 6363 },
            { 6363, 6362 }
        };

        public void Start(Server server)
        {
            foreach (var item in items)
            {
                server.Scripts.ItemUseScripts.Add(item.Key, this);
            }
        }

        public void Stop(Server server)
        {

        }

        public bool OnItemUse(Player player, Item fromItem, Context context)
        {
            ushort toOpenTibiaId;

            if ( items.TryGetValue(fromItem.Metadata.OpenTibiaId, out toOpenTibiaId) )
            {
                new ItemTransformCommand(fromItem, toOpenTibiaId).Execute(context);

                return true;
            }

            return false;
        }
    }
}