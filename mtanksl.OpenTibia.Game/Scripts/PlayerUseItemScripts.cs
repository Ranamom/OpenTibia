﻿using OpenTibia.Game.CommandHandlers;

namespace OpenTibia.Game.Scripts
{
    public class PlayerUseItemScripts : IScript
    {
        public void Start(Server server)
        {
            server.CommandHandlers.Add(new UseItemWalkToSourceHandler() );

            server.CommandHandlers.Add(new ContainerOpenHandler() );

            server.CommandHandlers.Add(new LadderHandler() );

            server.CommandHandlers.Add(new SewerHandler() );

            server.CommandHandlers.Add(new BookHandler() );

            server.CommandHandlers.Add(new WatchHandler() );

            server.CommandHandlers.Add(new FoodHandler() );

            server.CommandHandlers.Add(new BlueberryBushHandler() );

            server.CommandHandlers.Add(new UseItemTransformHandler() );
        }

        public void Stop(Server server)
        {
            
        }
    }
}