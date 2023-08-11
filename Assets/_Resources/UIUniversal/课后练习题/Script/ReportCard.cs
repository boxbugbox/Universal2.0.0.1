using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReportCard : MonoBehaviour
{
    private Button m_submit;
    private GameObject go;
    List<int> m_Single_Correct = new List<int>();
    List<int> m_Multiple_Correct = new List<int>();
    List<int> m_Judge_Correct = new List<int>();
    List<int> m_Fill_Correct = new List<int>();
    List<int> m_Brief_Correct = new List<int>();
    List<int> m_Other_Correct = new List<int>();
    [ShowIf("isSingle")] public ExaminationPaper[] m_Single_papers;
    [ShowIf("isMultiple")] public ExaminationPaper[] m_Multiple_papers;
    [ShowIf("isJudge")] public ExaminationPaper[] m_Judge_papers;
    [ShowIf("isFill")] public ExaminationFill[] m_in_fills;
    [ShowIf("isBrief")] public ExaminationFill[] m_brief_fills;
    [ShowIf("isOther")] public ExaminationFill[] m_other_fills;
    [HorizontalGroup("O"), VerticalGroup("O/E"), BoxGroup("O/E/答对"), LabelWidth(80)] public Text m_SingleAnswerCount, m_MultipleAnswerCount, m_JudgeAnswerCount, m_BriefAnswerCount, m_FillAnswerCount;
    [HorizontalGroup("O"), VerticalGroup("O/R"), BoxGroup("O/R/得分"), LabelWidth(80)] public Text m_SingleAnswerScore, m_MultipleAnswerScore, m_JudgeAnswerScore, m_BriefAnswerScore, m_FillAnswerScore;
    [HorizontalGroup("O"), VerticalGroup("O/E"), BoxGroup("O/E/答对"), LabelWidth(80), ShowIf("isOther")] public Text m_OtherAnswerCount;
    [HorizontalGroup("O"), VerticalGroup("O/R"), BoxGroup("O/R/得分"), LabelWidth(80), ShowIf("isOther")] public Text m_OtherAnswerScore;

    [Title("")]
    [LabelWidth(40), LabelText("单选题"), HorizontalGroup("H", 0.15f), VerticalGroup("H/V1")] public bool isSingle;
    [LabelWidth(40), LabelText("多选题"), HorizontalGroup("H", 0.15f), VerticalGroup("H/V1")] public bool isMultiple;
    [LabelWidth(40), LabelText("判断题"), HorizontalGroup("H", 0.15f), VerticalGroup("H/V1")] public bool isJudge;    
    [LabelWidth(40), LabelText("填空题"), HorizontalGroup("H", 0.15f), VerticalGroup("H/V1")] public bool isFill;
    [LabelWidth(40), LabelText("简答题"), HorizontalGroup("H", 0.15f), VerticalGroup("H/V1")] public bool isBrief;
    [LabelWidth(40), LabelText("其他题"), HorizontalGroup("H", 0.15f), VerticalGroup("H/V1")] public bool isOther;
    [Title("")]
    [LabelWidth(30), LabelText("分数"), VerticalGroup("H/V2")] public float m_singleScore;
    [LabelWidth(30), LabelText("分数"), VerticalGroup("H/V2")] public float m_multipleScore;
    [LabelWidth(30), LabelText("分数"), VerticalGroup("H/V2")] public float m_judgeScore;    
    [LabelWidth(30), LabelText("分数"), VerticalGroup("H/V2")] public float m_fillScore;
    [LabelWidth(30), LabelText("分数"), VerticalGroup("H/V2")] public float m_briefScore;
    [LabelWidth(30), LabelText("分数"), VerticalGroup("H/V2")] public float m_otherScore;

    [Title("成绩单"), PropertySpace(10)]
    public GameObject reportCardObject;
    public Text TotalScore;
    public Button checkButton;
    [Title("查看试题"), PropertySpace(10)]
    public GameObject checkObject;
    public GameObject[] cloneObject;
    public Button returnReportCardObject;
    public ScrollRect m_scrollRect;
    public Transform m_parent;
    [Title("时间"), PropertySpace(10)]
    public Text m_CardTime;
    public Text reportCardTime;
    [Title("提交面板"), PropertySpace(10)]
    public GameObject submitObject;
    public Button confirmsub;
    public Button goahead;

    private void Start()
    {
        m_submit = this.GetComponent<Button>();
        confirmsub.onClick.AddListener(OnSubmit);
        checkButton.onClick.AddListener(OnCheck);
        reportCardObject.transform.DOScale(0, 0f);
        checkObject.transform.DOScale(0, 0f);
        returnReportCardObject.onClick.AddListener(() => { checkObject.transform.DOScale(0, 0f); });
        m_submit.onClick.AddListener(() => { submitObject.SetActive(true); });
        goahead.onClick.AddListener(() => { submitObject.SetActive(false); });
    }
    private void OnSubmit()
    {
        float _singleScore = 0;
        float _multipleScore = 0;
        float _judgeScore = 0;
        float _fillScore = 0;
        float _briefScore = 0;
        float _otherScore = 0;

        if (isSingle)
        {
            SingleCalculation();
            m_SingleAnswerScore.text = (m_Single_Correct.Count * m_singleScore).ToString();
            m_SingleAnswerCount.text = m_Single_Correct.Count.ToString();
            _singleScore = (m_Single_Correct.Count * m_singleScore);
        }
        if (isMultiple)
        {
            MultipleCalculation();
            m_MultipleAnswerScore.text = (m_Multiple_Correct.Count * m_multipleScore).ToString();
            m_MultipleAnswerCount.text = m_Multiple_Correct.Count.ToString();
            _multipleScore = (m_Multiple_Correct.Count * m_multipleScore);
        }
        if (isJudge)
        {
            JudgeCalculation();
            m_JudgeAnswerScore.text = (m_Judge_Correct.Count * m_judgeScore).ToString();
            m_JudgeAnswerCount.text = m_Judge_Correct.Count.ToString();
            _judgeScore = (m_Judge_Correct.Count * m_judgeScore);
        }
        if (isFill)
        {
            FillCalculation();
            m_FillAnswerScore.text = (m_Fill_Correct.Count * m_fillScore).ToString();
            m_FillAnswerCount.text = m_Fill_Correct.Count.ToString();
            _fillScore = (m_Fill_Correct.Count * m_fillScore);
        }
        if (isBrief)
        {
            BriefCalculation();
            m_BriefAnswerScore.text = (m_Brief_Correct.Count * m_briefScore).ToString();
            m_BriefAnswerCount.text = m_Brief_Correct.Count.ToString();
            _briefScore = (m_Brief_Correct.Count * m_briefScore);
        }
        if (isOther)
        {
            OtherCalculation();
            m_OtherAnswerCount.text = (m_Other_Correct.Count * m_otherScore).ToString();
            m_OtherAnswerScore.text = m_Other_Correct.Count.ToString();
            _otherScore = (m_Other_Correct.Count * m_otherScore);
        }
        m_CardTime.GetComponent<UniversalTimer>().Pause = false;
        reportCardTime.text = m_CardTime.text;
        TotalScore.text = (_singleScore + _multipleScore + _judgeScore + _fillScore + _briefScore + _otherScore).ToString();
        reportCardObject.transform.DOScale(1, 1f);
    }
    private void OnCheck()
    {
        ComeBacklater();
        checkObject.transform.DOScale(1, 1f);
    }
    /// <summary>
    /// 单选计算得分
    /// </summary>
    private void SingleCalculation()
    {
        for (int i = 0; i < m_Single_papers.Length; i++)
        {
            var value = m_Single_papers[i].Calculation();
            if (value > -1)
            {
                m_Single_Correct.Add(value);
            }
        }
    }
    /// <summary>
    /// 多选计算得分
    /// </summary>
    private void MultipleCalculation()
    {
        for (int i = 0; i < m_Multiple_papers.Length; i++)
        {
            var value = m_Multiple_papers[i].Calculation();
            if (value > -1)
            {
                m_Multiple_Correct.Add(value);
            }
        }
    }
    /// <summary>
    /// 判断计算得分
    /// </summary>
    private void JudgeCalculation()
    {
        for (int i = 0; i < m_Judge_papers.Length; i++)
        {
            var value = m_Judge_papers[i].Calculation();
            if (value > -1)
            {
                m_Judge_Correct.Add(value);
            }
        }
    }
    /// <summary>
    /// 填空得分
    /// </summary>
    private void FillCalculation()
    {
        for (int i = 0; i < m_in_fills.Length; i++)
        {
            var value = m_in_fills[i].Calculation();
            if (value > -1)
            {
                m_Fill_Correct.Add(value);
            }
        }
    }
    /// <summary>
    /// 简答题得分
    /// </summary>
    private void BriefCalculation()
    {
        for (int i = 0; i < m_brief_fills.Length; i++)
        {
            var value = m_brief_fills[i].CalculationBrief();
            if (value > -1)
            {
                m_Brief_Correct.Add(value);
            }
        }
    }
    /// <summary>
    /// 其他得分_名词解析
    /// </summary>
    private void OtherCalculation()
    {
        for (int i = 0; i < m_other_fills.Length; i++)
        {
            var value = m_other_fills[i].CalculationBrief();
            if (value > -1)
            {
                m_Other_Correct.Add(value);
            }
        }
    }
    /// <summary>
    /// 回看成绩
    /// </summary>
    private void ComeBacklater()
    {
        ClearChilds(m_parent);
        GameObject titleobj = Resources.Load("ReturnLookTitle") as GameObject;
        for (int i = 0; i < cloneObject.Length; i++)
        {
            titleobj = Instantiate(titleobj) as GameObject;
            titleobj.transform.SetParent(m_parent, false);
            titleobj.GetComponent<ReturnLookTitleSkin>().m_title.text = cloneObject[i].name.Substring(10);
            
            go = Instantiate(cloneObject[i]) as GameObject;
            go.transform.SetParent(m_parent, false);                    
        }
        //m_scrollRect.content = m_parent.GetComponent<RectTransform>();
    }
    /// <summary>
    /// 清除父物体下面的所有子物体
    /// </summary>
    /// <param name="parent"></param>
    private void ClearChilds(Transform parent)
    {
        if (parent.childCount > 0)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }
    }
}
