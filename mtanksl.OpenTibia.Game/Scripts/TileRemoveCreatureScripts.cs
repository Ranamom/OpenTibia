﻿using OpenTibia.Game.CommandHandlers;

namespace OpenTibia.Game.Scripts
{
    public class TileRemoveCreatureScripts : Script
    {
        public override void Start(Server server)
        {
            server.EventHandlers.Subscribe(new TileDepressHandler() );

            server.EventHandlers.Subscribe(new CloseDoorAutomaticallyHandler() );
        }

        public override void Stop(Server server)
        {
            
        }
    }
}