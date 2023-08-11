using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal;
using Universal.Function;
using Universal.Medicine;

public class OnSliderView : MonoBehaviour
{   
    [LabelText("当前模式类型"), HorizontalGroup("类型")] public PatternType patternType;
    [LabelWidth(30), LabelText("分数"), HorizontalGroup("类型")] public float m_score;
    [SerializeField, HorizontalGroup("题目"), LabelText("自定义题目")] private bool isTitle;
    [LabelWidth(30), LabelText("题目"), HorizontalGroup("题目", width: 1), ShowIf("isTitle")] public string m_title;
    public Slider m_Slider;
    public Button confirm;
    [PropertySpace(0f,15f)] public GameObject error;
    [BoxGroup("Box",false)] public float m_max, m_min;
    
    [PropertySpace(15)] public UnityEvent choiceEvent;
    protected virtual void Start()
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
                if (m_Slider.value <= m_max && m_Slider.value >= m_min)
                {
                    OnCorrectCallback();
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
                if (m_Slider.value <= m_max && m_Slider.value >= m_min)
                {
                    question.score = m_score; 
                }
                else
                {
                    question.score = 0f;
                }
                UniversalOverall.universalOverall.Questions.Add(question);
                OnCorrectCallback();
                break;
            default:
                break;
        }
       
    }
    protected virtual void ErrorInit()
    {
        m_Slider.value = Random.Range(m_max, m_min);
    }
    private void OnDisable()
    {
        m_Slider.value = m_Slider.minValue;
        OnEnd();
    }
    protected virtual void OnEnd() { }
    protected virtual void OnCorrectCallback()
    {
        choiceEvent?.Invoke();
    }
}
