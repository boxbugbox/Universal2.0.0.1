using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class ChoiceT
{
    [LabelWidth(45), LabelText("选择题:"), HorizontalGroup("Hor")] public Toggle Choice;
    [LabelWidth(35), LabelText("错误:"), HorizontalGroup("Hor")] public GameObject Error;
    [LabelWidth(35), LabelText("答案:"), HorizontalGroup("Hor")] public bool isRight;
}
public class ChoiceManager : MonoBehaviour
{
    public List<ChoiceT> choices = new List<ChoiceT>();   
    public Button confirm;
    public Text m_Label;
    public string m_type;
    public int m_unmber;
    public float m_score;
    private int times;
    private SwitchingTools switchingTools;
    [PropertySpace(15)] public UnityEvent choiceEvent;
    private void Start()
    {
        Init();
        m_unmber = int.Parse(this.name.Substring(6)) + 1;
        confirm.onClick.AddListener(this.OnConfirm);
        switchingTools = this.transform.parent.GetComponent<SwitchingTools>();
    }
    private void OnConfirm()
    {
        switchingTools.SetChoiceSerialnumbersIcon(int.Parse(this.name.Substring(6)));
        m_Label.text = "回答错误！正确答案：";
        if (ErrorOfJudgment())
        {
            m_Label.text = "回答正确！";
            ChoiceScore.choiceScore.AddSocre(m_type,m_unmber,m_score);
            choiceEvent.Invoke();
        }
        else
        {
            if (times >= 1)
            {
                for (int i = 0; i < choices.Count; i++)
                {
                    if (choices[i].Choice.isOn == true && choices[i].isRight != true)
                    {
                        choices[i].Error.SetActive(true);
                    }
                }
                for (int i = 0; i < choices.Count; i++)
                {                   
                    if (choices[i].isRight == true)
                    {
                        choices[i].Choice.isOn = true;
                    }
                }
            }
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i].isRight.Equals(true))
                {
                    m_Label.text += Answer(i);
                }
            }
            ChoiceScore.choiceScore.AddSocre(m_type, m_unmber, 0f);
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
    private string Answer(int option)
    {
        if (option.Equals(0)) return "A";
        if (option.Equals(1)) return "B";
        if (option.Equals(2)) return "C";
        if (option.Equals(3)) return "D";
        if (option.Equals(4)) return "E";
        if (option.Equals(5)) return "F";
        if (option.Equals(6)) return "G";
        return "NULL";
    }

    private void Init()
    {
        foreach (var item in choices)
        {
            item.Error.SetActive(false);
        }
        m_Label.text = "";
    }
}
