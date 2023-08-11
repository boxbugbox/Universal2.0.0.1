using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal.Function;
using Universal.Medicine;
using DG.Tweening;
using Universal;

public class OnConfirmView : ClickEvents
{
    public ConfirmType confirmType;
    [SerializeField,InlineButton("SubSteps", ">"), InlineButton("SubStepsfloat", "-"), 
        InlineButton("AddStepsfloat", "+"),InlineButton("AddSteps", "<"),ShowIf("confirmType",ConfirmType.StartExecution)] private float startSteps;
    [ShowIf("confirmType", ConfirmType.Hint)] public Image m_hintImage;
    [PropertySpace(15),ShowIf("confirmType", ConfirmType.Hint)] public UnityEvent hintEvent;
    private ControllerManger controllerManger;
    private void Start()
    {
        controllerManger = GameObject.FindGameObjectWithTag("ControllerManger").GetComponent<ControllerManger>();
        switch (confirmType)
        {
            case ConfirmType.Default:
                break;
            case ConfirmType.Next:
                break;
            case ConfirmType.Hint:
                m_hintImage.transform.localScale = Vector3.zero;
                break;
            case ConfirmType.StartExecution:
                break;
            default:
                break;
        }
    }
    protected override void OnPress()
    {
        if (UniversalAudioSourceControl.universalAudioSourceControl != null)
        {
            UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(0);
        }
        switch (confirmType)
        {
            case ConfirmType.Default:
                break;
            case ConfirmType.Next:
                controllerManger.OnNextStep();
                break;
            case ConfirmType.Hint:
                m_hintImage.transform.DOScale(1f, 1f).OnComplete(() => hintEvent?.Invoke());
                break;
            case ConfirmType.StartExecution:
                controllerManger.StartExecution(startSteps);
                break;
            default:
                break;
        }
    }
    private void OnDisable()
    {
        switch (confirmType)
        {
            case ConfirmType.Default:
                break;
            case ConfirmType.Next:
                break;
            case ConfirmType.Hint:
                m_hintImage.transform.localScale = Vector3.zero;
                break;
            case ConfirmType.StartExecution:
                break;
            default:
                break;
        }
    }
    private void AddSteps()
    {
        startSteps++;
    }
    private void AddStepsfloat()
    {
        
        startSteps  = float.Parse((startSteps += 0.1f).ToString());
    }
    private void SubSteps()
    {
        startSteps--;
    }
    private void SubStepsfloat()
    {
        startSteps = float.Parse((startSteps -= 0.1f).ToString());
    }
}
public enum ConfirmType
{
    Default,
    Next,
    Hint,
    StartExecution,
}
