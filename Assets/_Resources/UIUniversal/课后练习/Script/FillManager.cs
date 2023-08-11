using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class FillT
{
    [LabelWidth(45), LabelText("填空题:"), HorizontalGroup("Hor")] public InputField Fill;
    [LabelWidth(35), LabelText("答案:"), HorizontalGroup("Hor")] public string Answer;
}
public class FillManager : MonoBehaviour
{
    public List<FillT> fills = new List<FillT>();
    public Button confirm;
    public Text m_Label;
    public GameObject m_Tab;
    public string m_type;
    public int m_unmber;
    public float m_score;
    private int times;
    [PropertySpace(15)] public UnityEvent fillEvent;
    private void Start()
    {
        Init();
        confirm.onClick.AddListener(this.OnConfirm);
    }
    private void OnConfirm()
    {
        m_Tab.SetActive(true);
        m_Label.text = "正确答案：";
        if (ErrorOfJudgment())
        {
            m_Label.text = "回答正确";
            ChoiceScore.choiceScore.AddSocre(m_type, m_unmber, m_score);
            fillEvent.Invoke();
        }
        else
        {
            if (times >= 1)
            {

                for (int i = 0; i < fills.Count; i++)
                {
                    if (i == fills.Count - 1)
                    {
                        m_Label.text += fills[i].Answer;
                    }
                    else
                    {
                        m_Label.text += fills[i].Answer + "、";
                    }
                    
                }
            }
            ChoiceScore.choiceScore.AddSocre(m_type, m_unmber, 0f);
        }
    }
    private bool ErrorOfJudgment()
    {
        int correctTimes = 0;
        for (int i = 0; i < fills.Count; i++)
        {
            if (fills[i].Fill.text.Equals(fills[i].Answer))
            {
                correctTimes++;
                if (correctTimes.Equals(fills.Count))
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
    private void Init()
    {
        m_Label.text = "";
    }
    [OnInspectorGUI]
    private void AddFillInput()
    {
        if (GUILayout.Button("Add"))
        {
            
            fills.Clear();
            var count = this.gameObject.GetComponentsInChildren<InputField>();
            for (int i = 0; i < count.Length; i++)
            {
                FillT fillts = new FillT();
                fillts.Fill = count[i];
                fills.Add(fillts);
            }
        }
    }
}
