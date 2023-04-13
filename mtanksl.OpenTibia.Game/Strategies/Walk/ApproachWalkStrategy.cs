﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using System.Collections.Generic;
using System.Linq;

namespace OpenTibia.Game.Strategies
{
    public class ApproachWalkStrategy : IWalkStrategy
    {
        public Tile GetNext(Server server, Tile spawn, Creature creature, Creature target)
        {
            int deltaY = creature.Tile.Position.Y - target.Tile.Position.Y;

            int deltaX = creature.Tile.Position.X - target.Tile.Position.X;

            HashSet<Direction> directions = new HashSet<Direction>();

            if (deltaY < 0)
            {
                directions.Add(Direction.South);
            }
            else if (deltaY > 0)
            {
                directions.Add(Direction.North);
            }

            if (deltaX < 0)
            {
                directions.Add(Direction.East);
            }
            else if (deltaX > 0)
            {
                directions.Add(Direction.West);
            }

            if (directions.Count > 0)
            {
                // Try approaching

                foreach (var direction in server.Randomization.Shuffle(directions.ToArray() ) )
                {
                    Tile toTile = server.Map.GetTile(creature.Tile.Position.Offset(direction) );

                    if (toTile == null ||
                        
                        toTile.GetItems().Any(i => i.Metadata.Flags.Is(ItemMetadataFlags.NotWalkable) || i.Metadata.Flags.Is(ItemMetadataFlags.BlockPathFinding) ) ||
                        
                        toTile.GetCreatures().Any(c => c.Block) )
                    {

                    }
                    else
                    {
                        return toTile;
                    }
                }
            }

            // Otherwise, random walk

            foreach (var direction in server.Randomization.Shuffle(new[] { Direction.North, Direction.East, Direction.South, Direction.West } ) )
            {
                Tile toTile = server.Map.GetTile(creature.Tile.Position.Offset(direction) );

                if (toTile == null || 
                    
                    toTile.GetItems().Any(i => i.Metadata.Flags.Is(ItemMetadataFlags.NotWalkable) || i.Metadata.Flags.Is(ItemMetadataFlags.BlockPathFinding) ) || 
                    
                    toTile.GetCreatures().Any(c => c.Block) )
                {

                }
                else
                {
                    return toTile;
                }
            }

            return null;
        }
    }
}