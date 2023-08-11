using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InheritDragUIOnHint : DragUI
{
    [Title("")] public Image hintImage;
    protected override void OnStart()
    {
        base.OnStart();
        hintImage.transform.localScale = Vector3.zero;
    }
    protected override void OnEnd()
    {
        base.OnEnd();
        hintImage.transform.localScale = Vector3.zero;
    }
    protected override void OnCorrectCallback()
    {
        hintImage.transform.DOScale(1, 1f).OnComplete(() => { confirmEvent?.Invoke(); });
    }
}
