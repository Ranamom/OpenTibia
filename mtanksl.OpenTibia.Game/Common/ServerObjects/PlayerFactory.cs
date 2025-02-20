﻿using OpenTibia.Common.Objects;
using OpenTibia.Common.Structures;
using OpenTibia.Game.Components;
using System;

namespace OpenTibia.Game
{
    public class PlayerFactory
    {
        private Server server;

        public PlayerFactory(Server server)
        {
            this.server = server;
        }

        public Player Create(IConnection connection, Data.Models.Player databasePlayer)
        {
            Client client = new Client(server);

            Player player = new Player()
            {
                DatabasePlayerId = databasePlayer.Id,

                Name = databasePlayer.Name,

                Health = (ushort)databasePlayer.Health,

                MaxHealth = (ushort)databasePlayer.MaxHealth,

                Direction = (Direction)databasePlayer.Direction,

                BaseOutfit = databasePlayer.BaseOutfitId == 0 ? new Outfit(databasePlayer.BaseOutfitItemId) : new Outfit(databasePlayer.BaseOutfitId, databasePlayer.BaseOutfitHead, databasePlayer.BaseOutfitBody, databasePlayer.BaseOutfitLegs, databasePlayer.BaseOutfitFeet, (Addon)databasePlayer.BaseOutfitAddon),
               
                Outfit = databasePlayer.OutfitId == 0 ? new Outfit(databasePlayer.OutfitItemId) : new Outfit(databasePlayer.OutfitId, databasePlayer.OutfitHead, databasePlayer.OutfitBody, databasePlayer.OutfitLegs, databasePlayer.OutfitFeet, (Addon)databasePlayer.OutfitAddon),

                BaseSpeed = (ushort)databasePlayer.BaseSpeed,

                Speed = (ushort)databasePlayer.Speed,

                Invisible = databasePlayer.Invisible,

                Skills = {

                    MagicLevel = (byte)databasePlayer.SkillMagicLevel,

                    MagicLevelPercent = (byte)databasePlayer.SkillMagicLevelPercent,

                    Fist = (byte)databasePlayer.SkillFist,

                    FistPercent = (byte)databasePlayer.SkillFistPercent,

                    Club = (byte)databasePlayer.SkillClub,

                    ClubPercent = (byte)databasePlayer.SkillClubPercent,

                    Sword = (byte)databasePlayer.SkillSword,

                    SwordPercent = (byte)databasePlayer.SkillSwordPercent,

                    Axe = (byte)databasePlayer.SkillAxe,

                    AxePercent = (byte)databasePlayer.SkillAxePercent,

                    Distance = (byte)databasePlayer.SkillDistance,

                    DistancePercent = (byte)databasePlayer.SkillDistancePercent,

                    Shield = (byte)databasePlayer.SkillShield,

                    ShieldPercent = (byte)databasePlayer.SkillShieldPercent,

                    Fish = (byte)databasePlayer.SkillFish,

                    FishPercent = (byte)databasePlayer.SkillFishPercent
                },

                Experience = (uint)databasePlayer.Experience,

                Level = (ushort)databasePlayer.Level,

                LevelPercent = (byte)databasePlayer.LevelPercent,

                Mana = (ushort)databasePlayer.Mana,

                MaxMana = (ushort)databasePlayer.MaxMana,

                Soul = (byte)databasePlayer.Soul,

                Capacity = (uint)databasePlayer.Capacity,

                Stamina = (ushort)databasePlayer.Stamina,

                Gender = (Gender)databasePlayer.Gender,

                Vocation = (Vocation)databasePlayer.Vocation
            };
     
            client.Connection = connection;

            player.Client = client;

            server.GameObjects.AddGameObject(player);

            server.GameObjectComponents.AddComponent(player, new PlayerCooldownBehaviour() );

            server.GameObjectComponents.AddComponent(player, new PlayerAttackAndFollowBehaviour() );

            server.GameObjectComponents.AddComponent(player, new PlayerEnvironmentLightBehaviour() );

            server.GameObjectComponents.AddComponent(player, new PlayerPingBehaviour() );

            return player;
        }

        public bool Detach(Player player)
        {
            return server.GameObjects.RemoveGameObject(player);
        }

        public void Destroy(Player player)
        {
            server.GameObjectComponents.ClearComponents(player);

            server.GameObjectEventHandlers.ClearEventHandlers(player);
        }
    }
}