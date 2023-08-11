using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Universal.Function;
using DG.Tweening;
using Universal;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class ClickModelEvent : MonoBehaviour
{
    public ClickModelType clickModelType;
    [LabelText("当前模式类型"), HorizontalGroup("类型")] public PatternType patternType;  
    [LabelWidth(30), LabelText("题目"), HorizontalGroup("类型")] public string m_title;
    [LabelWidth(30), LabelText("分数"), HorizontalGroup("类型")] public float m_score;
    [PropertySpace(15)] public UnityEvent choiceEvent;
    private bool isAssessmentClick;
    public bool IsAssessmentClick { get => isAssessmentClick; set => isAssessmentClick = value; }
    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        patternType = UniversalOverall.universalOverall.PatternType;
        switch (patternType)
        {
            case PatternType.Default:
                break;
            case PatternType.学习:
                if (this.gameObject.GetComponent<HighlightingSystem.Highlighter>().tween)
                {
                    switch (clickModelType)
                    {
                        case ClickModelType.Default:
                            break;
                        case ClickModelType.床头卡:
                            break;
                        case ClickModelType.腕带:
                            break;
                        default:
                            break;
                    }
                    choiceEvent.Invoke();
                    this.gameObject.GetComponent<HighlightingSystem.Highlighter>().tween = false;
                }
                break;
            case PatternType.考核:
                if (isAssessmentClick)
                {
                    Question question = new Question();
                    question.title = m_title;
                    question.score = m_score;
                    UniversalOverall.universalOverall.Questions.Add(question);
                    isAssessmentClick = false;
                    choiceEvent.Invoke();
                }
                break;
            default:
                break;
        }
    }
}
public enum ClickModelType
{
    Default,
    床头卡,
    腕带,
}
