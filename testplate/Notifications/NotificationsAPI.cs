using System.Runtime.CompilerServices;
using BepInEx.Bootstrap;

namespace PokrukNotifyLib.Notifications {
    /// <summary>
    ///     Api for soft dependencies, not recommend for hard dependencies
    ///     Just copy to your project files and add:
    ///     [BepInDependency("org.pragmate.pokruk.notifications", BepInDependency.DependencyFlags.SoftDependency)]
    ///     to the BaseUnityPlugin plugin class
    /// </summary>
    public static class NotificationsAPI {
        private static bool? _enabled;

        public static bool enabled {
            get {
                if (_enabled == null)
                    _enabled = Chainloader.PluginInfos.ContainsKey("org.pragmate.pokruk.notifications");
                return (bool)_enabled;
            }
        }
    
        public static void Send(string text, int clearMilliseconds = 1000) {
            if (enabled) SendInternal(text, clearMilliseconds);
        }
        
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static void SendInternal(string text, int clearMilliseconds = 1000) {
            NotifiLib.SendNotification(text, clearMilliseconds);
        }
    }
}