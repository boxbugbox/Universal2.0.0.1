using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShortAnswerManager : MonoBehaviour
{
    public Button confirm;
    public Text m_Label;
    public InputField m_InputField;
    public GameObject m_Tab;
    public string m_type;
    public int m_unmber;
    public float m_score;
    private void Start()
    {
        confirm.onClick.AddListener(this.OnConfirm);
    }
    private void OnConfirm()
    {
        m_Tab.SetActive(true);
        if (m_InputField.text==null || m_InputField.text == "")
        {
            ChoiceScore.choiceScore.AddSocre(m_type, m_unmber, 0f);
        }
        if (m_InputField.text.Contains("电解质") || m_InputField.text.Contains("维持酸碱平衡"))
        {
            ChoiceScore.choiceScore.AddSocre(m_type, m_unmber, m_score);
        }
        else
        {
            ChoiceScore.choiceScore.AddSocre(m_type, m_unmber, 0f);
        }
    }
}
