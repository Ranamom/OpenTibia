﻿using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Events;

namespace OpenTibia.Game.Scripts
{
    public class GlobalScripts : Script
    {
        public override void Start(Server server)
        {
            CreatureThink(server);

            PlayerPing(server);

            ClockTick(server);
        }

        private void CreatureThink(Server server)
        {
            Promise.Delay("CreatureThink", 100).Then( () =>
            {
                CreatureThink(server);

                Context.AddEvent(new CreatureThinkGameEventArgs() );

                return Promise.Completed;

            } ).Catch( (ex) =>
            {
                if (ex is PromiseCanceledException)
                {
                                
                }
                else
                {
                    server.Logger.WriteLine(ex.ToString(), LogLevel.Error);
                }
            } );
        }

        private void PlayerPing(Server server)
        {
            Promise.Delay("PlayerPing", 60000).Then( () =>
            {
                PlayerPing(server);

                Context.AddEvent(new PlayerPingGameEventArgs() );

                return Promise.Completed;

            } ).Catch( (ex) =>
            {
                if (ex is PromiseCanceledException)
                {
                                
                }
                else
                {
                    server.Logger.WriteLine(ex.ToString(), LogLevel.Error);
                }
            } );
        }

        private void ClockTick(Server server)
        {
            Promise.Delay("ClockTick", Clock.Interval).Then( () =>
            {
                ClockTick(server);

                Context.Server.Clock.Tick();

                Context.AddEvent(new ClockTickGameEventArgs(Context.Server.Clock.Hour, Context.Server.Clock.Minute) );

                return Promise.Completed;

            } ).Catch( (ex) =>
            {
                if (ex is PromiseCanceledException)
                {
                                
                }
                else
                {
                    server.Logger.WriteLine(ex.ToString(), LogLevel.Error);
                }
            } );
        }

        public override void Stop(Server server)
        {
            server.CancelQueueForExecution("CreatureThink");

            server.CancelQueueForExecution("PlayerPing");

            server.CancelQueueForExecution("ClockTick");
        }
    }
}