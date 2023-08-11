using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal;
using Universal.Function;
using Universal.Medicine;

[Serializable]
public class ChoiceViewT
{
    [BoxGroup("选择题"), HorizontalGroup("选择题/Group")] public Toggle Choice;
    [BoxGroup("选择题"), HorizontalGroup("选择题/Group")] public bool isRight;
}
public class OnChoiceView : MonoBehaviour
{
    private int times;
    public List<ChoiceViewT> choices = new List<ChoiceViewT>();
    [LabelText("选择题类型"), HorizontalGroup("类型")] public ChoiceType choiceType;
    [LabelWidth(30), LabelText("分数"), HorizontalGroup("类型")] public float m_score;
    [SerializeField, HorizontalGroup("题目"), LabelText("自定义题目")] private bool isTitle;
    [LabelWidth(30), LabelText("题目"), HorizontalGroup("题目", width:1), ShowIf("isTitle")] public string m_title;
    public Button confirm;
    [InlineButton("FindErrorObject", "Find")] public GameObject error;
    [PropertySpace(15)] public UnityEvent choiceEvent;
    private void Start()
    {
        /*for (int i = 0; i < choiceViews.Count; i++)
        {
            int k = i;
            choiceViews[k].Choice.onValueChanged.AddListener((bool isOn) => OnPlayEffect(isOn, k));
        }*/
        //choiceType = ChoiceType.考核;
        confirm.onClick.AddListener(OnConfirm);
    }
    private void OnPlayEffect(bool isOn, int k)
    {
        UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(2);
    }
    private void OnConfirm()
    {
        choiceType = (ChoiceType)GameObject.FindGameObjectWithTag("ControllerManger").GetComponent<ControllerManger>().patternType;
        Question question = new Question();
        if (isTitle) question.title = m_title;
        else question.title = this.name.Substring(9);
        if (ErrorOfJudgment())
        {
            switch (choiceType)
            {
                case ChoiceType.Default:
                    break;
                case ChoiceType.学习:
                    choiceEvent.Invoke();
                    break;
                case ChoiceType.考核:
                    question.score = m_score;
                    break;
                default:
                    break;
            }
            //Debug.Log(Output.print("正确"));
        }
        else
        {
            switch (choiceType)
            {
                case ChoiceType.Default:
                    break;
                case ChoiceType.学习:
                    ErrorEvent();
                    break;
                case ChoiceType.考核:
                    question.score = 0;
                    break;
                default:
                    break;
            }
            //Debug.Log(Output.print("错误"));
        }
        if (choiceType.Equals(ChoiceType.考核))
        {
            UniversalOverall.universalOverall.Questions.Add(question);
            choiceEvent?.Invoke();
        }
    }
    private bool ErrorOfJudgment()
    {
        int correctTimes = 0;
        for (int i = 0; i < choices.Count; i++)
        {
            if (choices[i].Choice.isOn.Equals(choices[i].isRight))
            {
                correctTimes++;
                if (correctTimes.Equals(choices.Count))
                {
                    switch (choiceType)
                    {
                        case ChoiceType.Default:
                            break;
                        case ChoiceType.学习:
                            Init();
                            break;
                        case ChoiceType.考核:
                            break;
                        default:
                            break;
                    }
                    return true;
                }
            }
            else
            {
                times++;
                return false;
            }
        }
        return false;
    }
    private void Init()
    {
        foreach (var item in choices)
        {
            item.Choice.isOn = false;
        }
        times = 0;
        //Debug.Log(Output.print("选择题初始化"));
    }
    protected virtual void ErrorEvent()
    {
        if (times >= 2)
        {
            foreach (var item in choices)
            {
                item.Choice.isOn = item.isRight;
            }
        }
        UniversalViewControl.universalViewControl.SetHint(error);
    }
#if UNITY_EDITOR
    private void FindErrorObject()
    {
        error = GameObject.Find("UICamera/OverlayCanvas/Error");
    }
#endif

}
public enum ChoiceType
{
    Default,
    学习,
    考核
}
