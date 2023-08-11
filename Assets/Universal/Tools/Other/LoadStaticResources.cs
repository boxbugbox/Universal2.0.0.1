using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.UI;

[System.Serializable]
public class DynamicScenes
{
    [LabelWidth(55),LabelText("场景索引"),HorizontalGroup("h", width:0.2f)]public int sceneIndex;
    [LabelWidth(55), LabelText("场景名称"), HorizontalGroup("h")] public string sceneName;
}
public class LoadStaticResources : MonoBehaviour
{
    #region 加载2.0
    public delegate void LoadDelegate();
    private LoadDelegate loadDelegateCallback;
    [LabelWidth(40), HorizontalGroup("Horizontal", marginRight: 55)] public CanvasGroup damp;
    [LabelWidth(75), HorizontalGroup("Horizontal", marginRight: 150)] public bool isActivation;
    public Image _Colorblock;
    public Slider _Progress;
    public Text _Label;
    private void Start()
    {
        _Progress.onValueChanged.AddListener(OnProgressValueChange);
        if (isActivation)
        {
            OffDamp();
        }
    }
    private void OnProgressValueChange(float value)
    {
        _Colorblock.fillAmount = value;
        _Label.text = (value * 100f).ToString("#") + "%";

    }
    /// <summary>
    /// 关闭Damp
    /// </summary>
    private void OffDamp()
    {
        damp.alpha = 0.0f;
        damp.blocksRaycasts = false;
    }
    /// <summary>
    /// 开启Damp
    /// </summary>
    public void SetDamp(LoadDelegate load)
    {
        damp.alpha = 1.0f;
        damp.blocksRaycasts = true;
        float value = Random.Range(0.8f, 0.97f);
        _Progress.DOValue(value, 3f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            load?.Invoke();
        });
    }
    #endregion
    #region 加载1.0
    /*public string resourcesName;
    public delegate void LoadDelegate();
    private LoadDelegate loadDelegateCallback;
    Dictionary<int, string> resourcesScene = new Dictionary<int, string>();
    [LabelWidth(40), HorizontalGroup("Horizontal", marginRight: 55)] public CanvasGroup damp;
    [LabelWidth(75), HorizontalGroup("Horizontal", marginRight: 150)] public bool isActivation;
    [SerializeField, Title("")] List<DynamicScenes> dynamics = new List<DynamicScenes>();
    private void Awake()
    {
        for (int i = 0; i < dynamics.Count; i++)
        {
            resourcesScene.Add(dynamics[i].sceneIndex, dynamics[i].sceneName);
        }
    }
    private void Start()
    {
      
        if (isActivation)
        {
            damp.alpha = 1.0f;
            damp.blocksRaycasts = true;
            if (resourcesName != null && resourcesName != "")
            {
                SceneManager.LoadSceneAsync(resourcesName, LoadSceneMode.Additive);
                SceneManager.sceneLoaded += CallBack;
            }
            else
            {
                OffDamp();
            }
        }      
    }
    private void CallBack(Scene arg0, LoadSceneMode arg1)
    {        
        resourcesName = arg0.name;
        damp.DOFade(0.0f, 1f).SetEase(Ease.InExpo).OnComplete(delegate {
            damp.blocksRaycasts = false;
            loadDelegateCallback?.Invoke();
        });
    }
    /// <summary>
    /// 开启Damp
    /// </summary>
    public void SetDamp()
    {
        damp.alpha = 1.0f;
        damp.blocksRaycasts = true;
    }
    /// <summary>
    /// 关闭Damp
    /// </summary>
    private void OffDamp()
    {
        damp.DOFade(0.0f, 1f).SetEase(Ease.InExpo).OnComplete(delegate {
            damp.blocksRaycasts = false;
        });
    }
    /// <summary>
    /// 淡入
    /// </summary>
    public void OnStart()
    {
        damp.alpha = 1.0f;
        damp.blocksRaycasts = true;
        damp.DOFade(0.0f, 1f).SetEase(Ease.InExpo).OnComplete(delegate {
            damp.blocksRaycasts = false;
        });
    }
    /// <summary>
    /// 淡入
    /// </summary>
    /// <param name="endCallback">事件</param>
    public void OnStart(TweenCallback endCallback)
    {
        damp.alpha = 1.0f;
        damp.blocksRaycasts = true;
        damp.DOFade(0.0f, 1f).SetEase(Ease.InExpo).OnComplete(delegate {
            damp.blocksRaycasts = false;
            endCallback();
        });
    }
    /// <summary>
    /// 切换场景
    /// </summary>
    /// <param name="nextscene"></param>
    public void Switchscene(int nextscene,LoadDelegate loadDelegate)
    {
        damp.alpha = 1.0f;
        damp.blocksRaycasts = true;
        // 卸载当前场景
        SceneManager.UnloadSceneAsync(resourcesName);
        // 加载下一个场景
        SceneManager.LoadScene(resourcesScene[nextscene], LoadSceneMode.Additive);
        //委托
        loadDelegateCallback = loadDelegate;
    }
    /// <summary>
    /// 验证是否切换场景
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public bool ISSwitchscene(int index)
    {
        if (resourcesName.Equals(resourcesScene[index]))
        {
            return true;
        }
        return false;
    }
    public void Init()
    {
        SceneManager.sceneLoaded -= CallBack;
    }*/
    #endregion
}
