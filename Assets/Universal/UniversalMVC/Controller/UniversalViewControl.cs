using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal.Function;

namespace Universal
{
    [System.Serializable]
    public class ViewT
    {
        [LabelWidth(30), LabelText("视图"), HorizontalGroup("Horizontal", width: 0.05f), DictionaryDrawerSettings] public bool _isOpen;
        [HideLabel, HorizontalGroup("Horizontal", width: 0.05f), DictionaryDrawerSettings] public string _name;
        [HideLabel, HorizontalGroup("Horizontal")] public GameObject _view;
    }
    public class UniversalViewControl : MonoBehaviour
    {
        #region 单例
        public static UniversalViewControl universalViewControl;
        private void Awake()
        {
            universalViewControl = this;
        }
        #region Editor
        [OnInspectorGUI,PropertyOrder(2)]
        private void UniversalInspectorGUI()
        {
            Version version = new Version();
            GUILayout.Space(15);
            GUISkin customSkin;
            customSkin = (GUISkin)Resources.Load("Editor\\Control");
            GUILayout.Label("Universal Frame", customSkin.FindStyle("Header"));
            GUILayout.Label(version.VersionNumber, customSkin.FindStyle("Bottom"));
        }
        #endregion
        #endregion
        [TableList(ShowIndexLabels = true, ShowPaging = false), SerializeField] List<ViewT> views = new List<ViewT>();
        [FoldoutGroup("Object"), SerializeField] private GameObject startUI, endUI;
        [TitleGroup(""), SerializeField] private GameObject hintObject;
        [TitleGroup(""), SerializeField] private Text hintContent;
        [TitleGroup(""), SerializeField] private ScrollRect hintScrollRect;
        Coroutine stopHintView;

        public GameObject HintObject { get => hintObject; set => hintObject = value; }
        public GameObject StartUI { get => startUI; set => startUI = value; }
        public GameObject EndUI { get => endUI; set => endUI = value; }

        /// <summary>
        /// 设置视图状态
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isOpen">状态</param>
        public void SetView(int index, bool isOpen)
        {
            views[index]._view.SetActive(isOpen);
        }
        /// <summary>
        /// 设置视图状态
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="isActive"></param>
        public void SetView(string viewName, bool isActive)
        {
            for (int i = 0; i < views.Count; i++)
            {
                if (views[i]._name.Equals(viewName))
                {
                    views[i]._view.SetActive(isActive);
                }
            }
        }
        /// <summary>
        /// 设置试图组状态
        /// </summary>
        /// <param name="indexs"></param>
        /// <param name="isOpen"></param>
        public void SetViews(int[] indexs, bool isOpen)
        {
            for (int i = 0; i < indexs.Length; i++)
            {
                views[indexs[i]]._view.SetActive(isOpen);
            }
        }
        /// <summary>
        /// 设置视图_推进
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isOpen">状态</param>
        /// <param name="speed">时间</param>
        public void SetView(int index, bool isOpen, float speed)
        {
            if (isOpen)
            {
                views[index]._view.SetActive(true);
                universalViewControl.views[index]._view.transform.localScale = Vector3.zero;
                universalViewControl.views[index]._view.transform.DOScale(Vector3.one, speed);
            }
            else
            {
                views[index]._view.SetActive(true);
                universalViewControl.views[index]._view.transform.localScale = Vector3.one;
                universalViewControl.views[index]._view.transform.DOScale(Vector3.zero, speed).OnComplete(delegate {
                    views[index]._view.SetActive(false);
                });
            }
        }
        /// <summary>
        /// 设置视图_推进X轴
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isOpen">状态</param>
        /// <param name="speed">时间</param>
        public void SetView(int index, bool isOpen, Vector3 start, Vector3 end, float speed)
        {
            if (isOpen)
            {
                views[index]._view.SetActive(true);
                universalViewControl.views[index]._view.transform.localScale = start;
                universalViewControl.views[index]._view.transform.DOScale(end, speed);
            }
            else
            {
                views[index]._view.SetActive(true);
                universalViewControl.views[index]._view.transform.localScale = start;
                universalViewControl.views[index]._view.transform.DOScale(end, speed).OnComplete(delegate {
                    views[index]._view.SetActive(false);
                });
            }
        }
        /// <summary>
        /// 设置视图_推进_结束回调
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isOpen">状态</param>
        /// <param name="speed">时间</param>
        /// <param name="onEndCallback">结束时间</param>
        public void SetView(int index, bool isOpen, float speed, TweenCallback onEndCallback)
        {
            if (isOpen)
            {
                views[index]._view.SetActive(true);
                universalViewControl.views[index]._view.transform.localScale = Vector3.zero;
                universalViewControl.views[index]._view.transform.DOScale(Vector3.one, speed).OnComplete(onEndCallback);
            }
            else
            {
                universalViewControl.views[index]._view.transform.localScale = Vector3.one;
                universalViewControl.views[index]._view.transform.DOScale(Vector3.zero, speed).OnComplete(onEndCallback);
            }
        }
        /// <summary>
        /// 设置视图_位移
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isOpen">状态</param>
        /// <param name="speed">时间</param>
        /// <param name="startVector">开始位置</param>
        /// <param name="endVector">结束位置</param>
        public void SetView(int index, bool isOpen, float speed, Vector3 startVector, Vector3 endVector)
        {
            views[index]._view.SetActive(isOpen);
            universalViewControl.views[index]._view.transform.localPosition = startVector;
            universalViewControl.views[index]._view.transform.DOLocalMove(endVector, speed).SetEase(Ease.OutBack);
        }
        /// <summary>
        /// 设置视图_位移_结束回调
        /// </summary>
        /// <param name="index">索引</param>
        /// <param name="isOpen">状态</param>
        /// <param name="speed">时间</param>
        /// <param name="startVector">开始位置</param>
        /// <param name="endVector">结束位置</param>
        /// <param name="onEndCallback">结束事件</param>
        public void SetView(int index, bool isOpen, float speed, Vector3 startVector, Vector3 endVector, TweenCallback onEndCallback)
        {
            views[index]._view.SetActive(isOpen);
            universalViewControl.views[index]._view.transform.localPosition = startVector;
            universalViewControl.views[index]._view.transform.DOLocalMove(endVector, speed).SetEase(Ease.OutBack).OnComplete(onEndCallback);
        }
        /// <summary>
        /// 获取视图组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetViewComponent<T>(int index)
        {
            return views[index]._view.GetComponent<T>();
        }
        /// <summary>
        /// 获取组件_名字
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public T GetViewComponent<T>(string viewName)
        {
            if (viewName != null)
            {
                for (int i = 0; i < views.Count; i++)
                {
                    if (views[i]._name.Equals(viewName))
                    {
                        return views[i]._view.GetComponent<T>();
                    }
                }
            }
            return default;
        }
        /// <summary>
        /// 获取视图状态
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool GetViewActive(int index)
        {
            if (views[index]._view.activeSelf == true)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 开始提示
        /// </summary>
        /// <param name="content">提示内容</param>
        public void SetHintView(string content)
        {           
            hintObject.SetActive(true);
            hintContent.text = content;
        }
        /// <summary>
        /// 开始提示
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="tiems">间隔时间</param>
        public void SetHintView(string content, float tiems)
        {
            hintScrollRect.DOVerticalNormalizedPos(1, 0f);
            hintObject.SetActive(true);
            hintContent.text = content;
            if (content.Length > (17 * 2))
            {
                hintScrollRect.DOVerticalNormalizedPos(1, 5f).OnComplete(() =>
                {
                    hintScrollRect.DOVerticalNormalizedPos(0f, tiems);
                });
            }         
        }
        /// <summary>
        /// 开始提示
        /// </summary>
        /// <param name="audioIndex">音频索引</param>
        /// <param name="content">提示内容</param>
        public void SetHintView(int audioIndex, string content)
        {          
            hintObject.SetActive(true);
            hintContent.text = content;
            if (UniversalAudioSourceControl.universalAudioSourceControl != null)
            {
                UniversalAudioSourceControl.universalAudioSourceControl.PlayHints(audioIndex);
            }
        }
        /// <summary>
        /// 开始提示
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="tiems">间隔时间</param>
        public void SetHintView(int audioIndex, string content, float tiems)
        {
            hintScrollRect.DOVerticalNormalizedPos(1, 0f);
            hintObject.SetActive(true);
            hintContent.text = content;          
            if (content.Length > (17 * 2))
            {
                hintScrollRect.DOVerticalNormalizedPos(1, 5f).OnComplete(() =>
                {
                    hintScrollRect.DOVerticalNormalizedPos(0f, tiems);
                });              
            }
            if (UniversalAudioSourceControl.universalAudioSourceControl != null)
            {
                UniversalAudioSourceControl.universalAudioSourceControl.PlayHints(audioIndex);
            }
        }
        /// <summary>
        /// 添加试图
        /// </summary>
        /// <param name="go"></param>
        public int AddViews(GameObject go)
        {
            ViewT viewgo = new ViewT();
            viewgo._view = go;
            views.Add(viewgo);
            return views.Count - 1;
        }
        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="go">提示物体</param>
        /// <returns></returns>
        public void SetHint(GameObject go)
        {
            StartCoroutine(Hint(go));
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            try
            {
                foreach (var item in views)
                {
                    item._view.SetActive(item._isOpen);
                }
            }
            catch (System.Exception e)
            {
                Debug.Log(Output.print(this.name + ":" + e.Message));
            }
            if (hintObject != null) hintObject.SetActive(false);
            if (startUI != null) startUI.SetActive(false);
            if (endUI != null) endUI.SetActive(false);
            hintScrollRect.verticalScrollbar.value = 1f;
        }      
        IEnumerator Hint(GameObject go)
        {
            go.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            go.SetActive(false);
        }
        IEnumerator HintView(string content, float times)
        {
            yield return new WaitForSeconds(times);
            hintContent.text = content;
        }
        [Button]
        void GetHintObject()
        {
            if (!hintObject)
            {
                var canvas = GameObject.FindObjectOfType<Canvas>();
                GameObject go = (GameObject)Resources.Load("HintBox");
                go = Instantiate(go) as GameObject;
                go.transform.parent = canvas.transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.name = "Hintbox---提示框";
                HintBoxSkin skin = go.GetComponent<HintBoxSkin>();
                hintObject = skin.m_HintBox;
                hintContent = skin.m_Label;
                hintScrollRect = skin.m_Rect;
            }
            else
            {
                Debug.Log("已存在");
            }
        }
    }
}