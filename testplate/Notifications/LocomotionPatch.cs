using GorillaLocomotion;
using HarmonyLib;
using UnityEngine;

namespace PokrukNotifyLib.Notifications {
    [HarmonyPatch(typeof(Player), "LateUpdate")]
    public class LocomotionPatch {
        public static void Postfix() {
            NotifiLib.Instance.OnUpdate();
        }
    }
}
