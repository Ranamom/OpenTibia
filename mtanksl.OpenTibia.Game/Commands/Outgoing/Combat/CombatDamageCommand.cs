﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class CombatDamageCommand : Command
    {
        public CombatDamageCommand(Creature attacker, Creature target, int health)
        {
            Attacker = attacker;

            Target = target;

            Health = health;
        }

        public Creature Attacker { get; set; }

        public Creature Target { get; set; }

        public int Health { get; set; }

        public override Promise Execute(Context context)
        {
            return Promise.Run(resolve =>
            {
                if (Health < 0)
                {
                    if (Target is Player player)
                    {
                        context.AddPacket(player.Client.Connection, new SetFrameColorOutgoingPacket(Attacker.Id, FrameColor.Black) );

                        context.AddPacket(player.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.WhiteBottomGameWindowAndServerLog, "You lose " + (-Health) + " hitpoints due to an attack by " + Attacker.Name) );

                        context.AddCommand(new ShowAnimatedTextCommand(Target.Tile.Position, AnimatedTextColor.Red, (-Health).ToString() ) );

                        if (Target.Health + Health > 0)
                        {
                            context.AddCommand(new CreatureUpdateHealthCommand(Target, (ushort)(Target.Health + Health), Target.MaxHealth) );
                        }
                        else
                        {
                            context.AddCommand(new PlayerDestroyCommand(player) );

                            context.Disconnect(player.Client.Connection);
                        }
                    }
                    else if (Target is Monster monster)
                    {
                        context.AddCommand(new ShowAnimatedTextCommand(Target.Tile.Position, AnimatedTextColor.Red, (-Health).ToString() ) );

                        if (Target.Health + Health > 0)
                        {
                            context.AddCommand(new CreatureUpdateHealthCommand(Target, (ushort)(Target.Health + Health), Target.MaxHealth) );
                        }
                        else
                        {
                            context.AddCommand(new MonsterDestroyCommand(monster) );
                        }
                    }
                }
                else
                {
                    if (Target.Health + Health < Target.MaxHealth)
                    {
                        context.AddCommand(new CreatureUpdateHealthCommand(Target, (ushort)(Target.Health + Health), Target.MaxHealth) );
                    }
                    else
                    {
                        context.AddCommand(new CreatureUpdateHealthCommand(Target, Target.MaxHealth, Target.MaxHealth) );
                    }
                }

                resolve(context);
            } );
        }
    }
}