﻿using OpenTibia.Common.Structures;
using OpenTibia.Game.Commands;
using System;

namespace OpenTibia.Game.CommandHandlers
{
    public class InvisibleHandler : CommandHandler<PlayerSayCommand>
    {
        public override Promise Handle(Func<Promise> next, PlayerSayCommand command)
        {
            if (command.Message.StartsWith("/ghost") && command.Player.Vocation == Vocation.Gamemaster)
            {
                if ( !command.Player.Invisible)
                {
                    return Context.AddCommand(new ShowMagicEffectCommand(command.Player.Tile.Position, MagicEffectType.Puff) ).Then( () =>
                    {
                        return Context.AddCommand(new CreatureUpdateInvisibleCommand(command.Player, true) );

                    } ).Then( () =>
                    {
                        return Context.AddCommand(new CreatureUpdateOutfitCommand(command.Player, command.Player.BaseOutfit, Outfit.Invisible) );
                    } );
                }
                else
                {
                    return Context.AddCommand(new ShowMagicEffectCommand(command.Player.Tile.Position, MagicEffectType.Teleport) ).Then( () =>
                    {
                        return Context.AddCommand(new CreatureUpdateInvisibleCommand(command.Player, false) );

                    } ).Then( () =>
                    {
                        return Context.AddCommand(new CreatureUpdateOutfitCommand(command.Player, command.Player.BaseOutfit, command.Player.BaseOutfit) );
                    } );
                }
            }

            return next();
        }
    }
}