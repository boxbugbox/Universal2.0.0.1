using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal;

public class OnTogglesView : MonoBehaviour
{
    public Toggle[] m_Toggles;
    [LabelText("当前模式类型"), HorizontalGroup("类型")] public PatternType patternType;
    [LabelWidth(30), LabelText("分数"), HorizontalGroup("类型")] public float m_score;
    [SerializeField, HorizontalGroup("题目"), LabelText("自定义题目")] private bool isTitle;
    [LabelWidth(30), LabelText("题目"), HorizontalGroup("题目", width: 1), ShowIf("isTitle")] public string m_title;
    public Button confirm;
    public GameObject error;    
    [PropertySpace(15)] public UnityEvent confirmEvent;
    private void Start()
    {
        confirm.onClick.AddListener(OnConfirm);
    }
    private void OnConfirm()
    {
        patternType = UniversalOverall.universalOverall.PatternType;
        switch (patternType)
        {
            case PatternType.Default:
                break;
            case PatternType.学习:
                if (Verification())
                {
                    confirmEvent?.Invoke();
                }
                else
                {
                    error.GetComponent<ErrorManager>().AddErrorEvent(delegate () { ErrorInit(); });
                }
                break;
            case PatternType.考核:
                Question question = new Question();
                if (isTitle) question.title = m_title;
                else question.title = this.name.Substring(9);
                if (Verification())
                {
                   question.score = m_score;
                }
                else
                {
                    question.score = 0f; 
                }
                confirmEvent?.Invoke();
                break;
            default:
                break;
        }    
    }
    private bool Verification()
    {
        for (int i = 0; i < m_Toggles.Length; i++)
        {
            if (!m_Toggles[i].isOn)
            {
                return false;
            }
        }
        return true;
    }
    private void ErrorInit()
    {
        foreach (var item in m_Toggles)
        {
            item.isOn = true;
        }
    }
    private void OnDisable()
    {
        foreach (var item in m_Toggles)
        {
            item.isOn = false;
        }
    }
}
