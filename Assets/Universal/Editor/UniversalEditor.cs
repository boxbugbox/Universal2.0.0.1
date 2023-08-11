using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Universal
{
    public class UniversalEditor : EditorWindow
    {
        private static Transform GetParent()
        {
            GameObject selectObj = Selection.activeGameObject;
            if (selectObj == null)
            {
                var canvas = GameObject.FindObjectOfType<Canvas>();
                if (canvas != null) return canvas.transform;
                else
                {
                    var canvasObject = new GameObject();
                    canvasObject.name = "Canvas";
                    canvas = canvasObject.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    canvasObject.AddComponent<CanvasScaler>();
                    canvasObject.AddComponent<GraphicRaycaster>();
                    var eventSystem = new GameObject();
                    eventSystem.name = "EventSystem";
                    eventSystem.AddComponent<EventSystem>();
                    eventSystem.AddComponent<StandaloneInputModule>();
                    return canvas.transform;
                }
            }
            else
            {
                return selectObj.transform;
            }
        }

        private static void AddUniversal(string name)
        {
            GameObject hp_bar = (GameObject)Resources.Load(name);
            hp_bar = Instantiate(hp_bar) as GameObject;
            hp_bar.transform.parent = GetParent();
            hp_bar.transform.localPosition = Vector3.zero;
            hp_bar.transform.localScale = new Vector3(1, 1, 1);
            hp_bar.name = name;
        }
        private static void AddUniversalObject(string name)
        {
            GameObject hp_bar = (GameObject)Resources.Load(name);
            hp_bar = Instantiate(hp_bar) as GameObject;
            hp_bar.transform.localPosition = Vector3.zero;
            hp_bar.transform.localScale = new Vector3(1, 1, 1);
            hp_bar.name = name;
        }
        [MenuItem("GameObject/Universal/Universal Label", priority = 43)]
        public static void AddLineChart()
        {
            AddUniversal("Label");
        }       
        [MenuItem("GameObject/Universal/Universal Setings", priority = 43)]
        public static void AddSetingsChart()
        {
            AddUniversal("Setings");
        }
        [MenuItem("GameObject/Universal/Universal UICamera", priority = 43)]
        public static void AddUICameraChart()
        {
            AddUniversal("UICamera");
        }
        [MenuItem("GameObject/Universal/Universal Confirm", priority = 43)]
        public static void AddConfirmChart()
        {
            AddUniversal("Confirm");
        }
        [MenuItem("GameObject/Universal/Sreen Space Label", priority = 44)]
        public static void AddSreenSpaceLabelChart()
        {
            AddUniversal("Sreen Space Label");
        }
        [MenuItem("GameObject/Universal/Timer Manage", priority = 44)]
        public static void AddTimerManageChart()
        {
            AddUniversalObject("TimerManage");
        }
        [MenuItem("GameObject/Universal/Controller Manger", priority = 44)]
        public static void AddControllerMangerChart()
        {
            AddUniversalObject("ControllerManger");
        }
        [MenuItem("GameObject/Universal/Audio Source", priority = 44)]
        public static void AddAudioSourceChart()
        {
            AddUniversalObject("Audio Source");
        }
        [MenuItem("GameObject/Universal/Camera Manage", priority = 44)]
        public static void AddCameraManageChart()
        {
            AddUniversalObject("CameraManage");
        }
        [MenuItem("GameObject/Universal/Universal Overall", priority = 43)]
        public static void AddUniversalOverallChart()
        {
            AddUniversalObject("UniversalOverall");
        }
        [MenuItem("GameObject/Add to ViewControl", priority = 0)]
        public static void AddToViewControl()
        {
            GameObject active = Selection.activeGameObject;
            if (active == null)
                return;
            UniversalViewControl universalv = FindObjectOfType<Universal.UniversalViewControl>();
            var index = universalv.AddViews(active);
            Debug.Log(active.name + "：Added Successfully " + index);
        }
    }
}