using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class AssessmentManage : MonoBehaviour
{
    [LabelText("题目组")]public Text[] m_Titles;
    [LabelText("分数组")]public Text[] m_Scores;
    [LabelText("回看题组")] public GameObject[] m_TitleGroup;


    [FoldoutGroup("成绩单"), LabelText("总分")] public Text _totalScore;
    [FoldoutGroup("成绩单"), LabelText("答对数量")] public Text _correctCount;
    [FoldoutGroup("成绩单"), LabelText("考题回看")] public Button _restart;
    [FoldoutGroup("成绩单"), LabelText("退出考核")] public Button _exit;

    
    [FoldoutGroup("回看"), LabelText("上一题")] public Button m_up;
    [FoldoutGroup("回看"), LabelText("下一题")] public Button m_down;
    [FoldoutGroup("回看"), LabelText("回看提示")] public Text m_hit;
    [FoldoutGroup("回看"), LabelText("题目序号提示")] public Text m_Titlehit;
    [FoldoutGroup("回看"), LabelText("返回成绩界面")] public Button m_ReturnPerformanceDetails;
    [FoldoutGroup("回看"), LabelText("退出考核")] public Button _exit2;
    [FoldoutGroup("回看"), LabelText("回看界面")] public GameObject m_DetailedPanel;
    [FoldoutGroup("回看"), LabelText("回看提示UI")] public Image m_hitImage;
    [FoldoutGroup("回看"), LabelText("回看提示精灵")] public Sprite m_Correct, m_Error;

    int Index;
    List<Question> questions = new List<Question>();
    private void Start()
    {
        _restart.onClick.AddListener(() =>
        {
            m_DetailedPanel.SetActive(true);
        });
        _exit.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        _exit2.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false);
        });
        m_ReturnPerformanceDetails.onClick.AddListener(() =>
        {
            m_DetailedPanel.SetActive(false);
        });
        m_up.onClick.AddListener(OnUp);
        m_down.onClick.AddListener(OnDown);
        Inits();
    }
    /// <summary>
    /// 提交成绩
    /// </summary>
    public void OnSubmit()
    {
        TitleGroupManage.titleGroupManage.SetAssessmentTimesActive(false);
        questions.Clear();
        questions.AddRange(Universal.UniversalOverall.universalOverall.Questions);
        if (questions.Count > 0)
        {
            foreach (Question question in questions)
            {
                for (int i = 0; i < m_Titles.Length; i++)
                {
                    if (m_Titles[i].text == question.title)
                    {
                        m_Scores[i].text = question.score.ToString();
                    }                    
                }
            }
        }
        CalculateTscoreCount();
    }
    /// <summary>
    /// 计算总分数量
    /// </summary>
    private void CalculateTscoreCount()
    {
        int tscount = 0;
        float tscore = 0f;
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i].score > 0f)
            {
                tscount++;
                tscore += questions[i].score;
            }
        }
        _correctCount.text = tscount.ToString();
        _totalScore.text = tscore.ToString();

    }
    /// <summary>
    /// 上一题
    /// </summary>
    private void OnUp()
    {
        if (Index > 0)
        {
            InTitleGroup();
            Index--;
            m_Titlehit.text = "第" + (Index + 1) + "题";
            m_TitleGroup[Index].SetActive(true);
            GetRightWrong(Index);
        }
    }
    /// <summary>
    /// 下一题
    /// </summary>
    private void OnDown()
    {
        if (Index < m_TitleGroup.Length - 1)
        {
            InTitleGroup();
            Index++;
            m_Titlehit.text = "第" + (Index + 1) + "题";
            m_TitleGroup[Index].SetActive(true);
            GetRightWrong(Index);
        }
    }
    /// <summary>
    /// 初始化题目
    /// </summary>
    private void InTitleGroup()
    {
        for (int i = 0; i < m_TitleGroup.Length; i++)
        {
            m_TitleGroup[i].SetActive(false);
        }
    }
    /// <summary>
    /// 获取对错
    /// </summary>
    /// <param name="index"></param>
    private void GetRightWrong(int index)
    {
        if (index.Equals(m_TitleGroup.Length - 1))
        {
            if (m_Scores[index].text.Equals("0"))
            {
                m_hitImage.sprite = m_Error;
                m_hit.text = "<color=red>考试时间为15分钟</color>";
            }
            else
            {
                m_hitImage.sprite = m_Correct;
                m_hit.text = "<color=green>在考核时间内完成</color>";
            }
            return;
        }
        if (m_Scores[index].text.Equals("0"))
        {
            m_hitImage.sprite = m_Error;
            m_hit.text = "<color=red>正确答案如图</color>";
        }
        else
        {
            m_hitImage.sprite = m_Correct;
            m_hit.text = " ";
        }
    }
    private void Inits()
    {
        m_DetailedPanel.SetActive(false);
        InTitleGroup();
        m_TitleGroup[0].SetActive(true);
        GetRightWrong(0);
        m_Titlehit.text = "第1题";
    }
    private void OnDisable()
    {
        Inits();
    }
    public string Convert2Chinese(int input)
    {
        string ret = null;
        int input2 = Math.Abs(input);
        string resource = "零一二三四五六七八九";
        string unit = "个十百千万亿兆京垓秭穰沟涧正载极";
        if (input > Math.Pow(10, 4 * (unit.Length - unit.IndexOf('万'))))
        {
            throw new System.Exception("the input is too big,input:" + input);
        }
        Func<int, List<List<int>>> splitNumFunc = (val) => {
            int i = 0;
            int mod;
            int val_ = val;
            List<List<int>> splits = new List<List<int>>();
            List<int> splitNums;
            do
            {
                mod = val_ % 10;
                val_ /= 10;
                if (i % 4 == 0)
                {
                    splitNums = new List<int>();
                    splitNums.Add(mod);
                    if (splits.Count == 0)
                    {
                        splits.Add(splitNums);
                    }
                    else
                    {
                        splits.Insert(0, splitNums);
                    }
                }
                else
                {
                    splitNums = splits[0];
                    splitNums.Insert(0, mod);
                }
                i++;
            } while (val_ > 0);
            return splits;
        };

        Func<List<List<int>>, string> hommizationFunc = (data) => {
            List<StringBuilder> builders = new List<StringBuilder>();
            for (int i = 0; i < data.Count; i++)
            {
                List<int> data2 = data[i];
                StringBuilder newVal = new StringBuilder();
                for (int j = 0; j < data2.Count;)
                {
                    if (data2[j] == 0)
                    {
                        int k = j + 1;
                        for (; k < data2.Count && data2[k] == 0; k++) ;
                        //个位不是0，前面补一个零
                        newVal.Append('零');
                        j = k;
                    }
                    else
                    {
                        newVal.Append(resource[data2[j]]).Append(unit[data2.Count - 1 - j]);
                        j++;
                    }
                }
                if (newVal[newVal.Length - 1] == '零' && newVal.Length > 1)
                {
                    newVal.Remove(newVal.Length - 1, 1);
                }
                else if (newVal[newVal.Length - 1] == '个')
                {
                    newVal.Remove(newVal.Length - 1, 1);
                }

                if (i == 0 && newVal.Length > 1 && newVal[0] == '一' && newVal[1] == '十')
                {//一十 --> 十
                    newVal.Remove(0, 1);
                }
                builders.Add(newVal);
            }
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < builders.Count; i++)
            {//拼接
                if (builders.Count == 1)
                {//个位数
                    sb.Append(builders[i]);
                }
                else
                {
                    if (i == builders.Count - 1)
                    {//万位以下的
                        if (builders[i][builders[i].Length - 1] != '零')
                        {//十位以上的不拼接"零"
                            sb.Append(builders[i]);
                        }
                    }
                    else
                    {//万位以上的
                        if (builders[i][0] != '零')
                        {//零万零亿之类的不拼接
                            sb.Append(builders[i]).Append(unit[unit.IndexOf('千') + builders.Count - 1 - i]);
                        }
                    }
                }
            }
            return sb.ToString();
        };
        List<List<int>> ret_split = splitNumFunc(input2);
        ret = hommizationFunc(ret_split);
        if (input < 0) ret = "-" + ret;
        return ret;
    }
}
