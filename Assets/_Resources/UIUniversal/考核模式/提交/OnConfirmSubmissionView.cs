using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal;
using Universal.Medicine;

public class OnConfirmSubmissionView : MonoBehaviour
{
    [LabelText("我再想想")] public Button m_return;
    [LabelText("确认提交")] public Button m_submit;
    public AssessmentManage assessmentManage;
    private void Start()
    {
        m_return.onClick.AddListener(OnReturn);
        m_submit.onClick.AddListener(OnSubmit);
    }
    private void OnReturn()
    {      
        SetActive(false);
    }
    private void OnSubmit()
    {
        if (assessmentManage != null)
        {
            Question question = new Question();
            question.title = "用时";
            question.score = 3f;
            UniversalOverall.universalOverall.Questions.Add(question);
            assessmentManage.gameObject.SetActive(true);
            assessmentManage.OnSubmit();
            SetActive(false);
        }
    }
    private void SetActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}
