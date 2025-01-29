using GorillaLocomotion;
using HarmonyLib;
using PokrukNotifyLib.Notifications;

namespace PokrukNotifyLib.Patches {
    [HarmonyPatch(typeof(Player), "LateUpdate")]
    public class LocomotionPatch {
        public static void Postfix() {
            NotifiLib.Instance.OnUpdate();
        }
    }
}
