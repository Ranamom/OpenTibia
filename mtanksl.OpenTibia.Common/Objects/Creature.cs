﻿using OpenTibia.Common.Structures;

namespace OpenTibia.Common.Objects
{
    public abstract class Creature : GameObject, IContent
    {
        public Creature()
        {
            Health = 100;

            MaxHealth = 100;

            Direction = Direction.South;

            Light = new Light(0, 0);

            Outfit = new Outfit(266, 0, 0, 0, 0, Addon.None);

            Speed = 220;

            SkullIcon = SkullIcon.None;

            PartyIcon = PartyIcon.None;

            WarIcon = WarIcon.None;

            Block = true;
        }

        public TopOrder TopOrder
        {
            get
            {
                return TopOrder.Creature;
            }
        }

        public IContainer Container { get; set; }

        public Tile Tile
        {
            get
            {
                return Container as Tile;
            }
        }
        
        public string Name { get; set; }

        public ushort Health { get; set; }

        public ushort MaxHealth { get; set; }

        public Direction Direction { get; set; }

        public Light Light { get; set; }

        public Outfit Outfit { get; set; }

        public ushort Speed { get; set; }

        public SkullIcon SkullIcon { get; set; }

        public PartyIcon PartyIcon { get; set; }

        public WarIcon WarIcon { get; set; }

        public bool Block { get; set; }
        
        public Creature AttackTarget { get; set; }

        public Creature FollowTarget { get; set; }
    }
}