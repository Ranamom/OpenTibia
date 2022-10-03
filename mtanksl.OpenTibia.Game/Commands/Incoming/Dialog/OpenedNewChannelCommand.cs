﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;

namespace OpenTibia.Game.Commands
{
    public class OpenedNewChannelCommand : Command
    {
        public OpenedNewChannelCommand(Player player, ushort channelId)
        {
            Player = player;

            ChannelId = channelId;
        }

        public Player Player { get; set; }

        public ushort ChannelId { get; set; }

        public override void Execute(Context context)
        {
            Channel channel = context.Server.Channels.GetChannel(ChannelId);
            
            if (channel != null)
            {
                PrivateChannel privateChannel = channel as PrivateChannel;

                if (privateChannel != null)
                {
                    if ( !privateChannel.ContainsPlayer(Player) )
                    {
                        if ( !privateChannel.ContainsInvitation(Player) )
                        {
                            return;
                        }

                        privateChannel.RemoveInvitation(Player);

                        privateChannel.AddPlayer(Player);
                    }

                    context.AddPacket(Player.Client.Connection, new OpenChannelOutgoingPacket(privateChannel.Id, privateChannel.Name) );

                    OnComplete(context);
                }
                else
                {
                    GuildChannel guildChannel = channel as GuildChannel;

                    if (guildChannel != null)
                    {
                        if ( !guildChannel.ContainsPlayer(Player) )
                        {
                            guildChannel.AddPlayer(Player);
                        }

                        context.AddPacket(Player.Client.Connection, new OpenChannelOutgoingPacket(guildChannel.Id, guildChannel.Name) );

                        OnComplete(context);
                    }
                    else
                    {
                        PartyChannel partyChannel = channel as PartyChannel;

                        if (partyChannel != null)
                        {
                            if ( !partyChannel.ContainsPlayer(Player) )
                            {
                                partyChannel.AddPlayer(Player);
                            }

                            context.AddPacket(Player.Client.Connection, new OpenChannelOutgoingPacket(partyChannel.Id, partyChannel.Name) );

                            OnComplete(context);
                        }
                        else
                        {
                            if ( !channel.ContainsPlayer(Player) )
                            {
                                channel.AddPlayer(Player);
                            }

                            if (channel.Id == 3)
                            {
                                context.AddPacket(Player.Client.Connection, new OpenRuleViolationsChannelOutgoingPacket(channel.Id) );
                    
                                foreach (var ruleViolation in context.Server.RuleViolations.GetRuleViolations() )
                                {
                                    if (ruleViolation.Assignee == null)
                                    {
                                        context.AddPacket(Player.Client.Connection, new ShowTextOutgoingPacket(0, ruleViolation.Reporter.Name, ruleViolation.Reporter.Level, TalkType.ReportRuleViolationOpen, ruleViolation.Time, ruleViolation.Message) );
                                    }
                                }
                            }
                            else
                            {
                                context.AddPacket(Player.Client.Connection, new OpenChannelOutgoingPacket(channel.Id, channel.Name) );
                            }

                            OnComplete(context);
                        }
                    }                    
                }           
            }
        }
    }
}