﻿using OpenTibia.Common.Objects;

namespace OpenTibia.Game.Commands
{
    public class PlayerDestroyCommand : Command
    {
        public PlayerDestroyCommand(Player player)
        {
            Player = player;
        }

        public Player Player { get; set; }

        public override Promise Execute(Context context)
        {
            return context.AddCommand(new TileRemoveCreatureCommand(Player.Tile, Player) ).Then( (ctx, index) =>
            {
                foreach (var child in Player.Inventory.GetItems() )
                {
                    ctx.AddCommand(new ItemDestroyCommand(child) );
                }

                foreach (var pair in Player.Client.ContainerCollection.GetIndexedContainers() )
                {
                    Player.Client.ContainerCollection.CloseContainer(pair.Key);
                }

                foreach (var pair in Player.Client.WindowCollection.GetIndexedWindows() )
                {
                    Player.Client.WindowCollection.CloseWindow(pair.Key);
                }

                foreach (var channel in ctx.Server.Channels.GetChannels() )
                {
                    if (channel.ContainsPlayer(Player) )
                    {
                        channel.RemovePlayer(Player);
                    }

                    if (channel is PrivateChannel privateChannel)
                    {
                        if (privateChannel.Owner == Player)
                        {
                            privateChannel.Owner = null;
                        }

                        if (privateChannel.ContainsInvitation(Player) )
                        {
                            privateChannel.RemoveInvitation(Player);
                        }
                    }    
                    
                    //TODO: Remove channel
                }

                foreach (var ruleViolation in ctx.Server.RuleViolations.GetRuleViolations() )
                {
                    if (ruleViolation.Reporter == Player)
                    {
                        ruleViolation.Reporter = null;
                    }

                    if (ruleViolation.Assignee == Player)
                    {
                        ruleViolation.Assignee = null;
                    }

                    //TODO: Remove rule violation
                }

                ctx.Server.PlayerFactory.Destroy(Player);
            } );
        }
    }
}