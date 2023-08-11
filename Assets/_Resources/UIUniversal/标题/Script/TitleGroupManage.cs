using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TitleGroupManage : MonoBehaviour
{
    public static TitleGroupManage titleGroupManage;
    private void Awake()
    {
        titleGroupManage = this;
    }
    [LabelText("AssessmentTimes:倒计时")]public AssessmentTimes assessmentTimes;
    [LabelText("ConfirmSubmissionView:确认提交")]public GameObject confirmSubmissionView;
    [LabelText("Submit:提交")] public Button m_submit;
    [LabelText("ReportCard:成绩单")] public GameObject reportCard;
    private void Start()
    {
        m_submit.onClick.AddListener(OnAssessmentSubmit);
        SetAssessmentTimesActive(false);
        SetActive(false);
        Init();
    }
    /// <summary>
    /// 设置标题状态
    /// </summary>
    /// <param name="isActive"></param>
    public void SetActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
    /// <summary>
    /// 设置考核倒计时提交状态
    /// </summary>
    /// <param name="isActive"></param>
    public void SetAssessmentTimesActive(bool isActive)
    {
        m_submit.gameObject.SetActive(isActive);
        assessmentTimes.gameObject.SetActive(isActive);
        assessmentTimes.Pause = isActive;
        if (!isActive)
        {
            assessmentTimes.TimeSpend = 900f;
        }
    }
    /// <summary>
    /// 考核提交
    /// </summary>
    private void OnAssessmentSubmit()
    {
        confirmSubmissionView.SetActive(true);
        confirmSubmissionView.transform.localScale = Vector3.zero;
        confirmSubmissionView.transform.DOScale(1f, 1f);
    }
    private void Init()
    {
        confirmSubmissionView.transform.localScale = Vector3.zero;
        reportCard.SetActive(false);
    }
}
