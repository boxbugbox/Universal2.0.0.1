using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using Universal.Function;
using UnityEngine.Events;
using Universal;

public class SelectionItem : MonoBehaviour
{
    private Button m_Button;
    [LabelText("提示物体")] public CanvasGroup[] m_CanvasGroup;
    [LabelText("需要显示物品"),HideIf("selectionType", SelectionType.Confirm)] public GameObject showObject;
    [LabelText("父级")] public Transform _parent;
    [LabelText("错误面板"),ShowIf("selectionType",SelectionType.Confirm)] public GameObject errorObject;
    [PropertySpace(15)] public UnityEvent confirmEvent;
    [Title("")]public SelectionType selectionType;
    [SerializeField] int _Count;
    int _Error;
    Transform _transform;
    private void Start()
    {
        if (showObject != null) _transform = showObject.transform.parent;
        m_Button = this.GetComponent<Button>();
        m_Button.onClick.AddListener(OnAfterClick);    
    }
    private void OnAfterClick()
    {
        switch (selectionType)
        {
            case SelectionType.Default:
                break;
            case SelectionType.Selection:
                m_CanvasGroup[0].DOFade(0, 0f);
                m_CanvasGroup[0].transform.DOLocalMoveY(100f, 0f);
                m_CanvasGroup[0].transform.DOLocalMoveY(30f, 0.8f).SetEase(Ease.OutBack);
                m_CanvasGroup[0].DOFade(1, 0.8f).OnComplete(delegate {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    m_CanvasGroup[0].DOFade(0, 0.3f);
                });
                showObject.transform.SetParent(_parent);
                showObject.transform.SetAsFirstSibling();
                try
                {
                    UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(5);
                }
                catch (System.Exception e)
                {
                    Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
                }               
                break;
            case SelectionType.Error:
                m_CanvasGroup[1].DOFade(0, 0f);
                m_CanvasGroup[1].transform.DOLocalMoveY(100f, 0f);
                m_CanvasGroup[1].transform.DOLocalMoveY(30f, 0.8f).SetEase(Ease.OutBack);
                m_CanvasGroup[1].DOFade(1, 0.8f).OnComplete(delegate {
                    m_CanvasGroup[1].DOFade(0, 0.3f);
                });
                try
                {
                    UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(11);
                }
                catch (System.Exception e)
                {
                    Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
                }
                break;
            case SelectionType.Confirm:
                if (OnSubmit())
                {
                    _Error = 0;
                    confirmEvent.Invoke();
                    //Debug.Log(Output.print("正确"));
                }
                else
                {
                    if (_parent.childCount.Equals(0))
                    {
                        errorObject.GetComponent<ErrorManager>().AddErrorEvent(delegate () { ErrorInit(); });
                        //Debug.Log(Output.print("请选择物品"));
                    }
                    else
                    {
                        errorObject.GetComponent<ErrorManager>().AddErrorEvent(delegate () { ErrorInit(); });
                        //Debug.Log(Output.print("选择错误"));
                    }
                    _Error++;
                    if (_Error >= 3)
                    {
                        //Debug.Log(Output.print("错误三次以上"));
                    }
                }
                break;
            default:
                break;
        }       
    }
    private bool OnSubmit()
    {
        if (_parent.childCount >= _Count)
        {
            for (int i = 0; i < _parent.childCount; i++)
            {
                if (_parent.GetChild(i).name.Equals("saline") || _parent.GetChild(i).name.Equals("saline"))
                {
                    return false;
                }
            }
            return true;
        }
        return false;
    }
    private void OnDisable()
    {
        switch (selectionType)
        {
            case SelectionType.Default:
                break;
            case SelectionType.Selection:
                Init();
                this.transform.GetChild(0).gameObject.SetActive(false);
                break;
            case SelectionType.Error:
                break;
            case SelectionType.Confirm:
                Init();
                this.m_CanvasGroup[0].gameObject.SetActive(false);
                break;
            default:
                break;
        }        
    }
    private void Init()
    {
        if (showObject != null)
            showObject.transform.SetParent(_transform);
    }
    private void ErrorInit()
    {
        this.m_CanvasGroup[0].gameObject.SetActive(true);
    }
}
public enum SelectionType
{
    Default,
    [LabelText("Selection:选择物品")] Selection,
    [LabelText("Error:错误物品")] Error,
    [LabelText("Confirm:确认")] Confirm,
}
