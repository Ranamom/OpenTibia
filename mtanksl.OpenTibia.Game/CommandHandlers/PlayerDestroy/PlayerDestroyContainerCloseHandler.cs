﻿using OpenTibia.Game.Commands;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class PlayerDestroyContainerCloseHandler : CommandHandler<PlayerDestroyCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerDestroyCommand command)
        {
            return next().Then( () =>
            {
                foreach (var pair in command.Player.Client.ContainerCollection.GetIndexedContainers() )
                {
                    command.Player.Client.ContainerCollection.CloseContainer(pair.Key);
                }

                return Promise.Completed;
            } );
        }
    }
}