﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;
using System.Linq;

namespace OpenTibia.Game.Commands
{
    public class ShowTextCommand : Command
    {
        public ShowTextCommand(Creature creature, TalkType talkType, string message)
        {
            Creature = creature;

            TalkType = talkType;

            Message = message;
        }

        public Creature Creature { get; set; }

        public TalkType TalkType { get; set; }

        public string Message { get; set; }

        public override Promise Execute()
        {
            foreach (var observer in Context.Server.Map.GetObservers(Creature.Tile.Position).OfType<Player>() )
            {
                if (observer.Tile.Position.CanSee(Creature.Tile.Position) )
                {
                    Context.AddPacket(observer.Client.Connection, new ShowTextOutgoingPacket(0, Creature.Name, 0, TalkType, Creature.Tile.Position, Message) );
                }
            }

            return Promise.Completed;
        }
    }
}