using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using UnityEngine;
using UnityEngine.UI;

namespace PokrukNotifyLib.Notifications {
    public class Notification {
        public string text = "";

        public Notification(string text) {
            this.text = text;
        }
    }

    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class NotifiLib : BaseUnityPlugin {
        public static NotifiLib Instance; // is only necessary for backward compatibility

        public static bool logToConsole = true;

        private static NotifiLib instance;

        public static GameObject HUDObj;

        public static GameObject HUDObj2;

        private static GameObject MainCamera;
        public static Canvas canvas;
        private static Text Testtext;

        private static Text NotifiText;

        private static readonly bool IsEnabled = true;

        private readonly Material AlertText = new Material(Shader.Find("GUI/Text Shader"));

        private readonly List<Notification> notifications = new List<Notification>();

        private void Awake() {
            Instance = this;
        }

        private void LateUpdate() {
            if (instance == null && GameObject.Find("Main Camera") != null) {
                Init();
                instance = this;
            }

            if (MainCamera == null) return;
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
                MainCamera.transform.position.z);
            HUDObj2.transform.rotation = MainCamera.transform.rotation;
        }

        private void Init() {
            MainCamera = GameObject.Find("Main Camera");
            HUDObj = new GameObject();
            HUDObj2 = new GameObject();
            HUDObj2.name = "NOTIFICATIONLIB_HUD_OBJ";
            HUDObj.name = "NOTIFICATIONLIB_HUD_OBJ";
            canvas = HUDObj.AddComponent<Canvas>();
            HUDObj.AddComponent<CanvasScaler>();
            HUDObj.AddComponent<GraphicRaycaster>();
            canvas.enabled = true;
            canvas.renderMode = RenderMode.WorldSpace;
            canvas.worldCamera = MainCamera.GetComponent<Camera>();
            HUDObj.GetComponent<RectTransform>().sizeDelta = new Vector2(5f, 5f);
            HUDObj.GetComponent<RectTransform>().position = new Vector3(MainCamera.transform.position.x,
                MainCamera.transform.position.y, MainCamera.transform.position.z);
            HUDObj2.transform.position = new Vector3(MainCamera.transform.position.x, MainCamera.transform.position.y,
                MainCamera.transform.position.z - 4.6f);
            HUDObj.transform.parent = HUDObj2.transform;
            HUDObj.GetComponent<RectTransform>().localPosition = new Vector3(0f, 0f, 1.6f);
            var eulerAngles = HUDObj.GetComponent<RectTransform>().rotation.eulerAngles;
            eulerAngles.y = -270f;
            HUDObj.transform.localScale = new Vector3(1f, 1f, 1f);
            HUDObj.GetComponent<RectTransform>().rotation = Quaternion.Euler(eulerAngles);
            Testtext = new GameObject {
                transform = {
                    parent = HUDObj.transform
                }
            }.AddComponent<Text>();
            Testtext.text = "";
            Testtext.fontSize = 30;
            Testtext.font = Font.CreateDynamicFontFromOSFont("Agency FB", 24);
            Testtext.rectTransform.sizeDelta = new Vector2(450f, 210f);
            Testtext.alignment = TextAnchor.LowerLeft;
            Testtext.rectTransform.localScale = new Vector3(0.00333333333f, 0.00333333333f, 0.33333333f);
            Testtext.rectTransform.localPosition = new Vector3(-1f, -1f, -0.5f);
            Testtext.material = AlertText;

            NotifiText = Testtext;
            NotifiText.supportRichText = true;

            UpdateText();
        }

        public static void SendNotification(string NotificationText, int clearTime = 1000) { // Just for backward compatibility
            Send(NotificationText, clearTime);
        }
        
        public static void Send(string NotificationText, int clearTime = 1000) {
            if (logToConsole) Debug.Log(NotificationText);
            instance?.SendInternal(NotificationText, clearTime);
        }

        private void SendInternal(string NotificationText, int clearTime = 1000) {
            if (!IsEnabled) return;

            var notification = new Notification(NotificationText);
            notifications.Add(notification);

            UpdateText();
            CoroutinesHelper.Delayed(clearTime, () => { RemoveNotification(notification); });
        }

        public static string ToStringOutput(List<Notification> notifications) {
            var wtf = false;
            notifications.RemoveAll(el => el == null);

            var messages = notifications.Select(notification => {
                if (notification == null) {
                    wtf = true;
                    return "null WTF";
                }

                return notification.text;
            }).ToList();

            var formatedMessages = new List<string>();
            var counter = 1;
            for (var i = 0; i < messages.Count; i++) {
                var message = messages[i];
                var nextMessageSame = messages.Count > i + 1 && messages[i + 1] == message;
                if (nextMessageSame) {
                    counter += 1;
                } else {
                    if (counter > 1)
                        formatedMessages.Add($"{message} ({counter})");
                    else
                        formatedMessages.Add(message);

                    counter = 1;
                }
            }

            return formatedMessages.Join(Environment.NewLine);
        }

        private void UpdateText() {
            try {
                NotifiText.text = ToStringOutput(notifications);
            } catch (Exception err) {
                Debug.LogError(err);
            }
        }

        public void RemoveNotification(Notification notification) {
            notifications.Remove(notification);
            UpdateText();
        }
    }
}