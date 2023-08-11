using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[Serializable]
public class FillsT
{
    [LabelWidth(45), LabelText("填空题:"), HorizontalGroup("Hor")] public InputField Fill;
    [LabelWidth(35), LabelText("答案:"), HorizontalGroup("Hor")] public string Answer;
}
public class ExaminationFill : MonoBehaviour
{
    public List<FillsT> fills = new List<FillsT>();
    public SerialNumberSkin serialNumberSkin;
    [HideLabel,TextArea(5,10)]public string m_answer;
    private int times;
    [PropertySpace(15)] public UnityEvent fillEvent;
    private void Start()
    {
        fills[0].Fill.onEndEdit.AddListener((string value)=>OnClickSub(value));
    }
    private  void OnClickSub(string value)
    {
        serialNumberSkin.SetIconActive(true);
    }
    private void OnConfirm()
    {
        if (ErrorOfJudgment())
        {
            fillEvent.Invoke();
        }
        else
        {
            if (times >= 1)
            {

                for (int i = 0; i < fills.Count; i++)
                {
                    
                }
            }
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
    /// <summary>
    /// 判断填空题
    /// </summary>
    /// <returns></returns>
    public int Calculation()
    {
        int correctTimes = 0;
        for (int i = 0; i < fills.Count; i++)
        {
            if (fills[i].Fill.text.Equals(fills[i].Answer))
            {
                correctTimes++;
                if (correctTimes.Equals(fills.Count))
                {
                    return i;
                }
            }
            else
            {
                fills[i].Fill.text = "<color=green>" + fills[i].Answer + "</color>";
            }
        }
        return -1;
    }
    /// <summary>
    /// 简答题
    /// </summary>
    /// <returns></returns>
    public int CalculationBrief()
    {
        int correctTimes = 0;
        string[] keywords = fills[0].Answer.Split('|');//关键词
        for (int i = 0;i < keywords.Length; i++)
        {
            //Debug.Log(fills[0].Fill.text + "=====" + keywords[i]);
            if (fills[0].Fill.text.Contains(keywords[i]))
            {
                correctTimes++;
                if (correctTimes.Equals(keywords.Length))
                {
                    return i;
                }
            }
            else
            {
                fills[0].Fill.text = "<color=green>" + m_answer + "</color>";
            }
        }
        return -1;
    }
    [Button("获取填空输入框")]
    private void GetInputBox()
    {
        fills.Clear();
        InputField[] _inputFields = this.GetComponentsInChildren<InputField>();     
        if (_inputFields.Length > 0)
        {
            for (int i = 0; i < _inputFields.Length; i++)
            {
                FillsT _fill = new FillsT();
                _fill.Fill = _inputFields[i];
                _fill.Answer = "";
                fills.Add(_fill);
            }
        }
    }
}
