﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class CreatureUpdatePartyIconCommand : Command
    {
        public CreatureUpdatePartyIconCommand(Creature creature, PartyIcon partyIcon)
        {
            Creature = creature;

            PartyIcon = partyIcon;
        }

        public Creature Creature { get; set; }

        public PartyIcon PartyIcon { get; set; }

        public override Promise Execute()
        {
            if (Creature.PartyIcon != PartyIcon)
            {
                Creature.PartyIcon = PartyIcon;

                foreach (var observer in Context.Server.Map.GetObservers(Creature.Tile.Position).OfType<Player>() )
                {
                    byte clientIndex;

                    if (observer.Client.TryGetIndex(Creature, out clientIndex) )
                    {
                        Context.AddPacket(observer.Client.Connection, new SetPartyIconOutgoingPacket(Creature.Id, Creature.PartyIcon) );
                    }
                }

                Context.AddEvent(new CreatureUpdatePartyIconEventArgs(Creature, PartyIcon) );
            }

            return Promise.Completed;
        }
    }
}