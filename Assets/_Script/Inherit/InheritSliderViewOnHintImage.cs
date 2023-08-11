using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InheritSliderViewOnHintImage : OnSliderView
{
    [Title("")] public Image hintImage;
    protected override void Start()
    {
        base.Start();
        hintImage.transform.localScale = Vector3.zero;
    }
    protected override void OnCorrectCallback()
    {
        hintImage.transform.DOScale(1, 1f).OnComplete(() => { choiceEvent?.Invoke(); });
    }
    protected override void OnEnd()
    {
        base.OnEnd();
        hintImage.transform.localScale = Vector3.zero;
    }
}
