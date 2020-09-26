﻿using OpenTibia.Common.Objects;

namespace OpenTibia.Game.Scripts
{
    public interface IPlayerLogoutScript : IScript
    {
        bool OnPlayerLogout(Player player, Tile fromTile, Context context);
    }
}