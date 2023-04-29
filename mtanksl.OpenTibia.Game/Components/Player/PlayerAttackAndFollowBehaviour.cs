﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using OpenTibia.Game.Events;
using OpenTibia.Network.Packets.Outgoing;
using System;
using System.Collections.Generic;

namespace OpenTibia.Game.Components
{
    public class PlayerAttackAndFollowBehaviour : Behaviour
    {
        private enum State
        {
            None,

            Attack,

            Follow,

            AttackAndFollow
        }

        private IAttackStrategy attackStrategy;

        private TimeSpan cooldown;

        private IWalkStrategy walkStrategy;

        public PlayerAttackAndFollowBehaviour(IAttackStrategy attackStrategy, TimeSpan cooldown, IWalkStrategy walkStrategy)
        {
            this.attackStrategy = attackStrategy;

            this.cooldown = cooldown;

            this.walkStrategy = walkStrategy;
        }

        private Guid token;

        private State state;

        private Creature target;

        public override void Start(Server server)
        {
            Player player = (Player)GameObject;

            DateTime attackCooldown = DateTime.MinValue;

            DateTime walkCooldown = DateTime.MinValue;

            token = Context.Server.EventHandlers.Subscribe<GlobalTickEventArgs>( (context, e) =>
            {
                if (target == null)
                {
                    return Promise.Completed;
                }

                if (target.IsDestroyed || !player.Tile.Position.CanHearSay(target.Tile.Position) )
                {
                    Stop();

                    Context.AddPacket(player.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.WhiteBottomGameWindow, Constants.TargetLost), new StopAttackAndFollowOutgoingPacket(0) );

                    return Promise.Completed;
                }
            
                List<Promise> promises = new List<Promise>();

                if (state == State.Follow || state == State.AttackAndFollow)
                {
                    if (DateTime.UtcNow > walkCooldown)
                    {
                        Tile toTile = walkStrategy.GetNext(Context.Server, null, player, target);

                        if (toTile != null)
                        {
                            walkCooldown = DateTime.UtcNow.AddMilliseconds(1000 * toTile.Ground.Metadata.Speed / player.Speed);

                            promises.Add(Context.AddCommand(new CreatureWalkCommand(player, toTile) ) );
                        }
                    }
                }

                if (state == State.Attack || state == State.AttackAndFollow)
                {
                    if (DateTime.UtcNow > attackCooldown)
                    {
                        Command command = attackStrategy.GetNext(Context.Server, player, target);

                        if (command != null)
                        {
                            attackCooldown = DateTime.UtcNow.Add(cooldown);

                            promises.Add(Context.AddCommand(command) );
                        }
                    }
                }

                return Promise.WhenAll(promises.ToArray() );
            } );
        }

        public void Attack(Creature creature)
        {
            state = State.Attack;

            target = creature;
        }

        public void Follow(Creature creature)
        {
            state = State.Follow;

            target = creature;
        }

        public void AttackAndFollow(Creature creature)
        {
            state = State.AttackAndFollow;

            target = creature;
        }

        public void StartFollow()
        {
            if (state == State.Attack)
            {
                state = State.AttackAndFollow;
            }
        }

        public void StopFollow()
        {
            if (state == State.AttackAndFollow)
            {
                state = State.Attack;
            }
        }

        public void Stop()
        {
            state = State.None;

            target = null;
        }

        public override void Stop(Server server)
        {
            Context.Server.EventHandlers.Unsubscribe<GlobalTickEventArgs>(token);
        }
    }
}