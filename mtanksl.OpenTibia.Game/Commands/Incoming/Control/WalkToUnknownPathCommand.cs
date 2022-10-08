﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Network.Packets.Outgoing;
using System;

namespace OpenTibia.Game.Commands
{
    public class WalkToUnknownPathCommand : Command
    {
        public WalkToUnknownPathCommand(Player player, Tile tile)
        {
            Player = player;

            Tile = tile;
        }

        public Player Player { get; set; }

        public Tile Tile { get; set; }

        public override Promise Execute(Context context)
        {
            return Promise.Run(resolve =>
            {
                MoveDirection[] moveDirections = context.Server.Pathfinding.GetMoveDirections(Player.Tile.Position, Tile.Position);

                if (moveDirections.Length == 0)
                {
                    context.AddPacket(Player.Client.Connection, new ShowWindowTextOutgoingPacket(TextColor.WhiteBottomGameWindow, Constants.ThereIsNoWay) );
                }
                else
                {
                    context.AddCommand(new WalkToKnownPathCommand(Player, moveDirections) ).Then(ctx =>
                    {
                        resolve(ctx);
                    } );
                }
            } );
        }
    }
}