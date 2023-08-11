using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[System.Serializable]
public class PaperT
{
    [LabelWidth(45), LabelText("选择题:"), HorizontalGroup("Hor")] public Toggle Paper;
    [LabelWidth(35), LabelText("答案:"), HorizontalGroup("Hor")] public bool isRight;
}
public class ExaminationPaper : MonoBehaviour
{
    public List<PaperT> papers = new List<PaperT>();
    public Button confirm;
    public SerialNumberSkin serialNumberSkin;
    [PropertySpace(15)] public UnityEvent choiceEvent;
    [Title("")]public ExaminationPaperType m_type;
    public int m_unmber;
    public float m_score;
    public GameObject m_error;
    public GameObject m_correct;
    public Text m_error_label;

    private int times;
    private void Start()
    {
        Init();
        m_unmber = int.Parse(this.name.Substring(5)) + 1;
        if (confirm != null) confirm.onClick.AddListener(this.OnConfirm);
        for (int i = 0; i < papers.Count; i++)
        {
            int k = i;
            papers[k].Paper.onValueChanged.AddListener((bool isOn) => OnPapers(k, isOn));
        }
    }
    private void OnPapers(int j , bool isOn)
    {
        if (isOn)
        {
            serialNumberSkin.SetIconActive(true);
        }
    }
    private void OnConfirm()
    {
        if (ErrorOfJudgment())
        {
            choiceEvent.Invoke();
            Debug.Log("正确");
        }
        else
        {
            if (times >= 1)
            {
                for (int i = 0; i < papers.Count; i++)
                {
                    if (papers[i].isRight == true)
                    {
                        papers[i].Paper.isOn = true;
                    }
                }
                Debug.Log("大于一次初始化");
            }
            Debug.Log("错误");
        }
    }
    private bool ErrorOfJudgment()
    {
        int correctTimes = 0;
        for (int i = 0; i < papers.Count; i++)
        {
            if (papers[i].Paper.isOn.Equals(papers[i].isRight))
            {
                correctTimes++;
                if (correctTimes.Equals(papers.Count))
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

    public int Calculation()
    {
        int correctTimes = 0;
        for (int i = 0; i < papers.Count; i++)
        {
            if (papers[i].Paper.isOn.Equals(papers[i].isRight))
            {
                correctTimes++;
                if (correctTimes.Equals(papers.Count))
                {
                    m_correct.SetActive(true);
                    return m_unmber;
                }
            }
            else if (papers[i].isRight == true)
            {
                m_error.SetActive(true);
                m_error_label.text = "正确答案：";
                m_error_label.text += Answer(i);
            }
        }
        return -1;
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
    private void Init(){ }
}
public enum ExaminationPaperType
{
    Default,
    单选题,
    多选题,
    判断题,
    简答题,
    填空题,
}
