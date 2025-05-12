using System;
using GorillaLocomotion;
using HarmonyLib;
using PokrukNotifyLib.Notifications;
using UnityEngine;

namespace PokrukNotifyLib.Patches {
    [HarmonyPatch(typeof(GTPlayer), "Start")]
    public class PlayerStart {
        public static void Postfix(GTPlayer __instance) {
            try {
                NotifiLib.Instance.Init();
            } catch (Exception e) {
                Debug.LogError(e);
            }
        }
    }
}
