using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class SpriteManage : MonoBehaviour
{
    public SpiritIconType spiritIconType;
    public GameObject nurseSprite;
    bool isMove;
    Vector3 initial;
    Vector3 _boy, _girl;
    private void Awake()
    {
        initial = new Vector3(1163f, -648f, -10);
        _boy = new Vector3(803.1f, -352.3f, -10f);
        _girl = new Vector3(777.8f, -346.2f, -10f);
        nurseSprite.transform.localPosition = initial;
    }
    /// <summary>
    /// Click
    /// </summary>
    public void OnMove()
    {
        isMove = !isMove;
        if (isMove)
        {
            switch (spiritIconType)
            {
                case SpiritIconType.Default:
                    break;
                case SpiritIconType.Boy:
                    nurseSprite.transform.DOLocalMove(_boy, 1f).SetEase(Ease.OutExpo);
                    break;
                case SpiritIconType.Girl:
                    nurseSprite.transform.DOLocalMove(_girl, 1f).SetEase(Ease.OutExpo);
                    break;
                default:
                    break;
            }           
        }
        else
        {
            nurseSprite.transform.DOLocalMove(initial,1f).SetEase(Ease.OutBack);
        }       
    }
    /// <summary>
    /// 移动位置
    /// </summary>
    /// <param name="isMove"></param>
    public void SetMove(bool isMove)
    {
        if (isMove)
        {
            switch (spiritIconType)
            {
                case SpiritIconType.Default:
                    break;
                case SpiritIconType.Boy:
                    nurseSprite.transform.DOLocalMove(_boy, 1f).SetEase(Ease.OutExpo).SetEase(Ease.OutExpo);
                    break;
                case SpiritIconType.Girl:
                    nurseSprite.transform.DOLocalMove(_girl, 1f).SetEase(Ease.OutExpo).SetEase(Ease.OutExpo);
                    break;
                default:
                    break;
            }
        }
        else
        {
            nurseSprite.transform.DOLocalMove(initial, 1f).SetEase(Ease.OutBack);
        }
    }
    /// <summary>
    /// 移动位置
    /// </summary>
    /// <param name="isMove"></param>
    /// <param name="onEndCallback"></param>
    public void SetMove(bool isMove,TweenCallback onEndCallback)
    {
        if (isMove)
        {
            nurseSprite.transform.DOLocalMove(new Vector3(777.8f, -346.2f, -10f), 1f).SetEase(Ease.OutExpo);
        }
        else
        {
            nurseSprite.transform.DOLocalMove(initial, 1f).SetEase(Ease.OutBack).OnComplete(onEndCallback);
        }
    }
    /// <summary>
    /// 移动位置
    /// </summary>
    /// <param name="isMove"></param>
    /// <param name="onEndCallback"></param>
    /// <param name="onEnterCallback"></param>
    public void SetMove(bool isMove, TweenCallback onEndCallback,TweenCallback onEnterCallback)
    {
        if (isMove)
        {
            nurseSprite.transform.DOLocalMove(new Vector3(777.8f, -346.2f, -10f), 1f).SetEase(Ease.OutExpo).OnComplete(onEnterCallback);
        }
        else
        {
            nurseSprite.transform.DOLocalMove(initial, 1f).SetEase(Ease.OutBack).OnComplete(onEndCallback);
        }
    }
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        nurseSprite.transform.localPosition = initial;
    }
}
public enum SpiritIconType
{
    Default,
    Boy, 
    Girl,
}
