using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ScoreT
{
    [LabelWidth(55), LabelText("题目类型:"), HorizontalGroup("Hor")] public string Type;
    [LabelWidth(35), LabelText("题号:"), HorizontalGroup("Hor")] public int Number;
    [LabelWidth(35), LabelText("得分:"), HorizontalGroup("Hor")] public float Score;
}
public class ChoiceScore : MonoBehaviour
{
    #region 单例
    public static ChoiceScore choiceScore;
    private void Awake()
    {
        choiceScore = this;
    }
    #endregion
    public List<ScoreT> scores = new List<ScoreT>();
    public Text[] m_Texts;
    public Text m_Achievement;

    [Title("")]
    [LabelWidth(40), LabelText("单选题"), HorizontalGroup("H"), VerticalGroup("H/V1")] public bool isOne;
    [LabelWidth(40), LabelText("多选题"), HorizontalGroup("H"), VerticalGroup("H/V1")] public bool isMany;
    [LabelWidth(40), LabelText("填空题"), HorizontalGroup("H"), VerticalGroup("H/V1")] public bool isFill;
    [LabelWidth(40), LabelText("简答题"), HorizontalGroup("H"), VerticalGroup("H/V1")] public bool isBrief;
    [Title("")]
    [LabelWidth(80), VerticalGroup("H/V2")] public float m_oneScore;
    [LabelWidth(80), VerticalGroup("H/V2")] public float m_manyScore;
    [LabelWidth(80), VerticalGroup("H/V2")] public float m_fillScore;
    [LabelWidth(80), VerticalGroup("H/V2")] public float m_briefScore;
    public void AddSocre(string type,int number,float score)
    {
        for (int i = 0; i < this.scores.Count; i++)
        {
            if (this.scores[i].Type == type && this.scores[i].Number == number)
            {
                this.scores[i].Score = score;
                return;
            }
        }
        ScoreT scores = new ScoreT();
        scores.Type = type;
        scores.Number = number;
        scores.Score = score;
        this.scores.Add(scores);
    }
    public void NumerationSocre()
    {
        float oneCount = 0;
        float manyCount = 0;
        float fillCount = 0;
        float briefCount = 0;
        float oneSocre = 0;
        float manySocre = 0;
        float fillSocre = 0;
        float briefSocre = 0;

        float totalCount;
        float totalSocre;
        for (int i = 0; i < scores.Count; i++)
        {
            if (scores[i].Type.Equals("单选题") && isOne)
            { 
                //oneCount++;
                oneSocre += scores[i].Score;
                oneCount = oneSocre / m_oneScore;
            }
            if (scores[i].Type.Equals("多选题") && isMany)
            {
                //manyCount++;
                manySocre += scores[i].Score;
                manyCount = manySocre / m_manyScore;
            }
            if (scores[i].Type.Equals("填空题") && isFill)
            {
                //fillCount++;
                fillSocre += scores[i].Score;
                fillCount = fillSocre / m_fillScore;
            }
            if (scores[i].Type.Equals("问答题") && isBrief)
            {
                //briefCount++;
                briefSocre += scores[i].Score;
                briefCount = briefSocre / m_briefScore;
            }
        }
        totalCount = oneCount + manyCount + fillCount + briefCount;
        totalSocre = oneSocre + manySocre + fillSocre + briefSocre;
        if (isOne) m_Texts[0].text = oneCount.ToString();
        if (isMany) m_Texts[1].text = manyCount.ToString();
        if (isFill) m_Texts[2].text = fillCount.ToString();
        if (isBrief) m_Texts[3].text = briefCount.ToString();
        if (isOne) m_Texts[5].text = oneSocre.ToString();
        if (isMany) m_Texts[6].text = manySocre.ToString();
        if (isFill) m_Texts[7].text = fillSocre.ToString();
        if (isBrief) m_Texts[8].text = briefSocre.ToString();
        m_Texts[4].text = totalCount.ToString();
        m_Texts[9].text = totalSocre.ToString();
        m_Achievement.text = totalSocre.ToString();

    }
    public void Clear()
    {
        scores.Clear();
    }
}
