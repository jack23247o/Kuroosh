﻿using System;
using VenoX_Global_Systems._Globals_;
using VenoX_Global_Systems._Models_;

namespace VenoX_Global_Systems._Anticheat_
{
    public class Main
    {
        public static void AntiNoRagdoll(PlayerModel playerClass)
        {
            if (!Functions.AnticheatModel.AntiNoRagdoll) { return; }
            try { playerClass.Emit("VnX_Global_Systems_Client:SetPedCanRagdoll", true); }
            catch (Exception ex) { _Core_.Debug.CatchExceptions("[Anticheat-Error] : NoRagdoll", ex); }
        }
        public static void AntiFly(PlayerModel playerClass)
        {
            if (!Functions.AnticheatModel.AntiFly) { return; }
            try
            {
                if (playerClass.IsInVehicle) { return; }
                else if (playerClass.NextFlyUpdate >= DateTime.Now) { return; }
                else if (playerClass.Position.Z > 150) { return; }
                if (playerClass.EntityIsFlying)
                {
                    if (playerClass.FlyTicks > 5)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        _Core_.Debug.OutputDebugString("[INFO] : " + playerClass.Name + " got kicked! Reason : Fly-Anticheat!");
                        Console.ResetColor();
                        string reason = "[VenoX Global Systems " + Constants.VNX_GLOBAL_SYSTEMS_VERSION + "] : Kicked by Anticheat";
                        playerClass.Log(reason);
                        playerClass.Kick(reason);
                    }
                    playerClass.NextFlyUpdate = DateTime.Now.AddSeconds(3);
                }
            }
            catch (Exception ex) { _Core_.Debug.CatchExceptions("[Anticheat-Error] : AntiFly", ex); }
        }
        public static void CheckTeleport(PlayerModel playerClass)
        {
            if (!Functions.AnticheatModel.CheckTeleport) { return; }
            try
            {
                if (playerClass.IsInVehicle)
                {
                    if (playerClass.Position.Distance(playerClass.LastPosition) > 50)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        _Core_.Debug.OutputDebugString("[INFO] : " + playerClass.Name + " got kicked! Reason : Vehicle-Teleport-Anticheat!");
                        Console.ResetColor();
                        string reason = "[VenoX Global Systems " + Constants.VNX_GLOBAL_SYSTEMS_VERSION + "] : Kicked by Anticheat";
                        playerClass.Log(reason);
                        playerClass.Kick(reason);
                    }
                }
                else
                {
                    if (playerClass.Position.Distance(playerClass.LastPosition) > 20)
                    {
                        if (playerClass.Position.Z < 150)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            _Core_.Debug.OutputDebugString("[INFO] : " + playerClass.Name + " got kicked! Reason : Teleport-Anticheat!");
                            Console.ResetColor();
                            string reason = "[VenoX Global Systems " + Constants.VNX_GLOBAL_SYSTEMS_VERSION + "] : Kicked by Anticheat";
                            playerClass.Log(reason);
                            playerClass.Kick(reason);
                        }
                    }
                }
                playerClass.LastPosition = playerClass.Position;
            }
            catch (Exception ex) { _Core_.Debug.CatchExceptions("[Anticheat-Error] : CheckTeleport", ex); }
        }
        public static void CheckWeapons(PlayerModel playerClass)
        {
            if (!Functions.AnticheatModel.CheckWeapons) { return; }
            try
            {
                if (playerClass.Weapon == (uint)AltV.Net.Enums.WeaponModel.Fist) { return; }

                if (!playerClass.Weapons.Contains((AltV.Net.Enums.WeaponModel)playerClass.Weapon))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    _Core_.Debug.OutputDebugString("[INFO] : " + playerClass.Name + " got kicked! Reason : Weapon-Anticheat!");
                    Console.ResetColor();
                    string reason = "[VenoX Global Systems " + Constants.VNX_GLOBAL_SYSTEMS_VERSION + "] : Kicked by Anticheat";
                    playerClass.RemoveAllWeapons();
                    playerClass.Log(reason);
                    playerClass.Kick(reason);
                }

            }
            catch (Exception ex) { _Core_.Debug.CatchExceptions("[Anticheat-Error] : CheckWeapons", ex); }
        }
    }
}
