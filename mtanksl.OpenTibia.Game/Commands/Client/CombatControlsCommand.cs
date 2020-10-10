﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;

namespace OpenTibia.Game.Commands
{
    public class CombatControlsCommand : Command
    {
        public CombatControlsCommand(Player player, FightMode fightMode, ChaseMode chaseMode, SafeMode safeMode)
        {
            Player = player;

            FightMode = fightMode;

            ChaseMode = chaseMode;

            SafeMode = safeMode;
        }

        public Player Player { get; set; }

        public FightMode FightMode { get; set; }

        public ChaseMode ChaseMode { get; set; }

        public SafeMode SafeMode { get; set; }

        public override void Execute(Context context)
        {
            if (FightMode != Player.Client.FightMode)
            {
                Player.Client.FightMode = FightMode;
            }

            if (ChaseMode != Player.Client.ChaseMode)
            {
                Player.Client.ChaseMode = ChaseMode;

                if (Player.AttackTarget != null)
                {
                    if (Player.Client.ChaseMode == ChaseMode.StandWhileFighting)
                    {
                        Player.FollowTarget = null;

                        context.Server.CancelQueueForExecution(Constants.CreatureAttackOrFollowSchedulerEvent(Player) );
                    }
                    else
                    {
                        Player.FollowTarget = Player.AttackTarget;

                        new FollowCommand(Player, Player.FollowTarget).Execute(context);
                    }
                }
            }

            if (SafeMode != Player.Client.SafeMode)
            {
                Player.Client.SafeMode = SafeMode;
            }

            base.OnCompleted(context);
        }
    }
}