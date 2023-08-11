using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ItembarManager : MonoBehaviour
{
    public GameObject[] glint;
    public GameObject m_Bar;
    public GameObject unfold, folding;
    public Button m_Button;

    public ScrollRect m_ScrollRect;
    bool isUF;
    private void Start()
    {
        m_Button.onClick.AddListener(OnUnfoldFolding);
    }
    private void OnUnfoldFolding()
    {
        if (isUF)
        {
            SetStatus(true, 97f, 0.8f);
        }
        else
        {
            SetStatus(false, -73.7f, 0.8f);
        }
       /* isUF = !isUF;*/
    }
    /// <summary>
    /// 设置用物栏状态
    /// </summary>
    /// <param name="isStatus"></param>
    /// <param name="x"></param>
    /// <param name="time"></param>
    public void SetStatus(bool isStatus,float x,float time)
    {
        if (isStatus)
        {
            isUF = false;
            unfold.SetActive(false);
            folding.SetActive(true);
            m_Bar.GetComponent<RectTransform>().DOAnchorPosY(x, time);
        }
        else
        {
            isUF = true;
            folding.SetActive(false);
            unfold.SetActive(true);            
            m_Bar.GetComponent<RectTransform>().DOAnchorPosY(x, time);
        }
    }
    public void SetStatus(bool isStatus, float x, float time,TweenCallback tweenEndCallback)
    {
        if (isStatus)
        {
            isUF = false;
            unfold.SetActive(false);
            folding.SetActive(true);
            m_Bar.GetComponent<RectTransform>().DOAnchorPosY(x, time);
        }
        else
        {
            isUF = true;
            folding.SetActive(false);
            unfold.SetActive(true);
            m_Bar.GetComponent<RectTransform>().DOAnchorPosY(x, time).OnComplete(tweenEndCallback);
        }
    }
    /// <summary>
    /// 设置闪烁提示
    /// </summary>
    /// <param name="index"></param>
    public void SetGlint(int index)
    {
        glint[index].SetActive(true);
        if(index >= 8)
        {
            m_ScrollRect.horizontalScrollbar.value = 1;
        }
        else
        {
            m_ScrollRect.horizontalScrollbar.value = 0;
        }
    }
    public void Init()
    {
        foreach (var item in glint)
        {
            item.SetActive(false);
        }
    }
}
