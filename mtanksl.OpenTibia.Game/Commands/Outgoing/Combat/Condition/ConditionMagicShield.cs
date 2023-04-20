﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Components;

namespace OpenTibia.Game.Commands
{
    public class ConditionMagicShield : Condition
    {
        private DelayBehaviour delayBehaviour;

        public ConditionMagicShield(int durationInMilliseconds) : base(ConditionSpecialCondition.MagicShield)
        {
            DurationInMilliseconds = durationInMilliseconds;
        }

        public int DurationInMilliseconds { get; set; }

        public override Promise Update(Creature target)
        {
            delayBehaviour = Context.Current.Server.Components.AddComponent(target, new DelayBehaviour(DurationInMilliseconds) );

            return delayBehaviour.Promise;
        }

        public override void Stop(Server server)
        {
            if (delayBehaviour != null)
            {
                delayBehaviour.Stop(server);
            }            
        }
    }
}