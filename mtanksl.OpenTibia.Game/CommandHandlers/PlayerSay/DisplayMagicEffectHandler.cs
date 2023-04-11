﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class DisplayMagicEffectHandler : CommandHandler<PlayerSayCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerSayCommand command)
        {
            int id;

            if (command.Message.StartsWith("/me") && command.Message.Contains(" ") && int.TryParse(command.Message.Split(' ')[1], out id) && id >= 1 && id <= 70)
            {
                Tile fromTile = command.Player.Tile;
                              
                return Context.AddCommand(new ShowMagicEffectCommand(fromTile.Position, (MagicEffectType)id) );
            }

            return next();
        }
    }
}