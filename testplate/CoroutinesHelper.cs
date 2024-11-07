using System;
using System.Collections;
using BepInEx;
using UnityEngine;

namespace PokrukNotifyLib {
    [BepInPlugin("org.pragmate.pokruk.coroutineshelper", "CoroutinesHelper", "1.0.0")]
    internal class CoroutinesHelper : BaseUnityPlugin {
        public static CoroutinesHelper Instance { get; private set; }

        private void Awake() {
            Instance = this;
            Logger.LogInfo("Plugin GTNotifications is loaded!");
        }

        private static IEnumerator DelayCoroutine(float seconds, Action action) {
            yield return new WaitForSecondsRealtime(seconds);
            action();
        }

        private static IEnumerator DelayCoroutine(int milliseconds, Action action) {
            yield return DelayCoroutine(milliseconds / 1000f, action);
        }

        public static void Delayed(int milliseconds, Action action) {
            Instance.delayed(milliseconds, action);
        }

        private void delayed(int milliseconds, Action action) {
            StartCoroutine(DelayCoroutine(milliseconds, action));
        }
    }
}