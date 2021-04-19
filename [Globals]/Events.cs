﻿using AltV.Net;
using AltV.Net.Elements.Entities;
using System;
using System.Numerics;
using VnXGlobalSystems.Models;

namespace VnXGlobalSystems.Globals
{
    public class Events
    {
        ////////////////////////// Resource Start/Stop /////////////////////////////////////////////////////////
        public static void OnResourceStart()
        {
            Functions.LoadMainFunctions();
            Database.Main.OnResourceStart();
        }
        public static void OnResourceStop()
        {
            Functions.UnloadMainFunctions();
        }

        ////////////////////////// Player Events /////////////////////////////////////////////////////////
        [ScriptEvent(ScriptEventType.PlayerConnect)]
        public static void OnPlayerConnect(PlayerModel player)
        {
            try
            {
                Main.ConnectedPlayers.Add(player);
                player.Emit("VnXGlobalSystemsClient:GetDiscordID");
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("OnPlayerConnect", ex); }
        }
        [ScriptEvent(ScriptEventType.PlayerDisconnect)]
        public static void OnPlayerDisconnect(PlayerModel player, string reason)
        {
            try
            {
                Main.ConnectedPlayers.Remove(player);
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("OnPlayerDisconnect", ex); }
        }

        [ClientEvent("VnXGlobalSystems:SetDiscordID")]
        public static void UpdateDiscordInfo(PlayerModel player, string DiscordID)
        {
            try
            {
                player.DiscordID = DiscordID;
            }
            catch (Exception ex) { Core.Debug.CatchExceptions("UpdateDiscordInfo", ex); }
        }

        ////////////////////////// Weapon Anticheat /////////////////////////////////////////////////////////
        [ServerEvent("GlobalSystem:GiveWeapon")]
        public static void OnWeaponEventCall(PlayerModel player, uint WeaponHash, byte ammo, bool selectWeapon) => player.GivePlayerWeapon(WeaponHash, ammo, selectWeapon);

        [ServerEvent("GlobalSystem:RemovePlayerWeapon")]
        public static void RemovePlayerWeapon(PlayerModel player, uint WeaponHash) => player.RemovePlayerWeapon(WeaponHash);

        [ServerEvent("GlobalSystem:RemoveAllPlayerWeapons")]
        public static void RemoveAllPlayerWeapon(PlayerModel player) => player.RemoveAllPlayerWeapon();

        ////////////////////////// Player Anticheat /////////////////////////////////////////////////////////
        [ServerEvent("GlobalSystem:PlayerPosition")]
        public static void PlayerPosition(PlayerModel player, Vector3 position) => player.Position(position);
    }
    public static class EventFunctions
    {
        public static void Log(this IPlayer player, string text)
        {
            try { player.Emit(text); }
            catch (Exception ex) { Core.Debug.CatchExceptions("Global-Systems:Log", ex); }
        }
        public static void GivePlayerWeapon(this IPlayer player, uint WeaponHash, byte ammo, bool selectWeapon)
        {
            try { player.GiveWeapon(WeaponHash, ammo, selectWeapon); }
            catch (Exception ex) { Core.Debug.CatchExceptions("GivePlayerWeapon", ex); }
        }

        public static void RemovePlayerWeapon(this IPlayer player, uint WeaponHash)
        {
            try { player.RemoveWeapon(WeaponHash); }
            catch (Exception ex) { Core.Debug.CatchExceptions("RemovePlayerWeapon", ex); }
        }

        public static void RemoveAllPlayerWeapon(this IPlayer player)
        {
            try { player.RemoveAllWeapons(); }
            catch (Exception ex) { Core.Debug.CatchExceptions("RemoveAllPlayerWeapon", ex); }
        }

        public static void Position(this IPlayer player, Vector3 position)
        {
            try { player.Position = position; }
            catch (Exception ex) { Core.Debug.CatchExceptions("Position", ex); }
        }
    }
}
