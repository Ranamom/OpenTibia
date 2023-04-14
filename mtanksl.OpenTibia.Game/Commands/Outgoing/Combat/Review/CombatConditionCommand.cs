﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Components;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class CombatConditionCommand : Command
    {
        public CombatConditionCommand(Creature attacker, Creature target, SpecialCondition specialCondition, MagicEffectType? magicEffectType, int[] health, int cooldownInMilliseconds)
        {
            Attacker = attacker;

            Target = target;

            SpecialCondition = specialCondition;

            MagicEffectType = magicEffectType;

            Health = health;

            CooldownInMilliseconds = cooldownInMilliseconds;
        }

        public Creature Attacker { get; set; }

        public Creature Target { get; set; }
        
        public SpecialCondition SpecialCondition { get; set; }

        public MagicEffectType? MagicEffectType { get; set; }

        public int[] Health { get; set; }

        public int CooldownInMilliseconds { get; set; }

        private int index;

        public override Promise Execute()
        {
            if (MagicEffectType != null)
            {
                Context.AddCommand(new ShowMagicEffectCommand(Target.Tile.Position, MagicEffectType.Value) );
            }

            Context.AddCommand(new CombatChangeHealthCommand(Attacker, Target, MagicEffectType.ToAnimatedTextColor(), Health[index] ) );

            if (Target.Tile != null)
            {
                SpecialConditionBehaviour component = Context.Server.Components.GetComponent<SpecialConditionBehaviour>(Target);

                if (component != null)
                {
                    if (index < Health.Length - 1)
                    {
                        if ( !component.HasSpecialCondition(SpecialCondition) )
                        {
                            component.AddSpecialCondition(SpecialCondition);

                            if (Target is Player player)
                            {
                                Context.AddPacket(player.Client.Connection, new SetSpecialConditionOutgoingPacket(component.SpecialConditions) );
                            }
                        }

                        Context.Server.Components.AddComponent(Target, new DelayBehaviour("Combat_Condition_" + SpecialCondition, CooldownInMilliseconds) ).Promise.Then( () =>
                        {
                            index++;

                            return Execute();
                        } );
                    }
                    else
                    {
                        if (component.HasSpecialCondition(SpecialCondition) )
                        {
                            component.RemoveSpecialCondition(SpecialCondition);

                            if (Target is Player player)
                            {
                                Context.AddPacket(player.Client.Connection, new SetSpecialConditionOutgoingPacket(component.SpecialConditions) );
                            }
                        }

                        Context.Server.CancelQueueForExecution("Combat_Condition_" + SpecialCondition + Target.Id);
                    }
                }
            }

            return Promise.Completed;
        }
    }
}