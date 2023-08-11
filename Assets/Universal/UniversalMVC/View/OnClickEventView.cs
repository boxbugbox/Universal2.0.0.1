using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Universal.Function;

public class OnClickEventView : ClickEvents
{
    /// <summary>
    /// 点击操作类型
    /// </summary>
    public ClickEventType type;
    /// <summary>
    /// 场景名称
    /// </summary>
    [Header("场景名称"),HideIf("type",ClickEventType.Quit)] public string scenesName;
    protected override void OnPress()
    {
        switch (type)
        {
            case ClickEventType.Default:
                Debug.Log(Output.print("默认事件"));
                break;
            case ClickEventType.EnterScenes:
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenesName);
                _press.interactable = false;
                break;
            case ClickEventType.ReturnPrevious:
                int index = SceneManager.GetActiveScene().buildIndex;
                if (index > 1)
                {
                    UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(index - 1);
                }
                Debug.Log(Output.print("当前索引为" + index));
                break;
            case ClickEventType.Quit:
                SceneQuit.SceneEsc();
                break;
            case ClickEventType.GoHome:
                if (Universal.UniversalOverall.universalOverall != null)
                {
                    Universal.UniversalOverall.universalOverall.InSteps();
                    UniversalMenuControl.universalMenuControl.InMenu();
                    Universal.UniversalAudioSourceControl.universalAudioSourceControl.Init();
                }
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenesName);
                break;
            case ClickEventType.LoadingScenes:
                StartCoroutine(AsyncLoading());
                _press.interactable = false;
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <param name="sceneName"></param>
    /// <returns></returns>
    IEnumerator LoadScene(string sceneName)
    {
        //用Slider 展示的数值
        int disableProgress = 0;
        int toProgress = 0;

        //异步场景切换
        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);
        //不允许有场景切换功能
        op.allowSceneActivation = false;
        //op.progress 只能获取到90%，最后10%获取不到，需要自己处理
        while (op.progress < 0.9f)
        {
            //获取真实的加载进度
            toProgress = (int)(op.progress * 100);
            while (disableProgress < toProgress)
            {
                ++disableProgress;
                //Debug.Log(disableProgress / 100.0f); //0.01开始
                yield return new WaitForEndOfFrame();
            }
        }
        //因为op.progress 只能获取到90%，所以后面的值不是实际的场景加载值了
        toProgress = 100;
        while (disableProgress < toProgress)
        {
            ++disableProgress;
            //Debug.Log(disableProgress / 100.0f);
            yield return new WaitForEndOfFrame();
            //Debug.Log("加载资源：" + disableProgress + "%");
        }
        op.allowSceneActivation = true;
        //初始化
    }
    #region 异步加载 WebGL
    AsyncOperation m_operation;
    bool m_loadScene;
    int m_toProgress;
    int m_displayProgress;
    void FixedUpdate()
    {
        if (m_loadScene)
        {
            while (m_operation.progress < 0.9f)
            {
                m_toProgress = (int)(m_operation.progress * 100);
                while (m_displayProgress < m_toProgress)
                {
                    ++m_displayProgress;
                    //Debug.Log(m_displayProgress / 100.0f);
                    //loadingManage.OnLoadingText(m_displayProgress);
                    return;
                }
            }
            m_toProgress = 100;
            while (m_displayProgress < m_toProgress)
            {
                ++m_displayProgress;
                //Debug.Log(m_displayProgress / 100.0f);
                //loadingManage.OnLoadingText(m_displayProgress - 1);
                return;
            }
            m_operation.allowSceneActivation = true;
            //loadingManage.OnLoadingText(99);
        }
    }

    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    IEnumerator AsyncLoading()
    {
        //异步加载关卡
        m_operation = SceneManager.LoadSceneAsync(scenesName);
        //阻止当加载完成自动切换
        m_operation.allowSceneActivation = false;
        m_loadScene = true;
        yield return m_operation;
    }
    #endregion
}
public enum ClickEventType
{
    Default,
    [LabelText("EnterScenes:进入场景")] EnterScenes,
    [LabelText("ReturnPrevious:返回上一级")] ReturnPrevious,
    [LabelText("Quit:退出")] Quit,
    [LabelText("GoHome:返回主页")] GoHome,
    [LabelText("LoadingScenes:加载场景")] LoadingScenes,
}
