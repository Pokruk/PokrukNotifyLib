using System;
using GorillaLocomotion;
using HarmonyLib;
using PokrukNotifyLib.Notifications;
using UnityEngine;

namespace PokrukNotifyLib.Patches {
    [HarmonyPatch(typeof(Player), "Start")]
    public class PlayerStart {
        public static void Postfix(Player __instance) {
            try {
                NotifiLib.Instance.Init();
            } catch (Exception e) {
                Debug.LogError(e);
            }
        }
    }
}
