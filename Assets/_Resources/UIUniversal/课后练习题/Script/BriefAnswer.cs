using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BriefAnswer : MonoBehaviour
{
    public string[] keywords;
    public InputField m_inputField;
    public int m_unmber;
    public GameObject m_error;
    public GameObject m_correct;
    public Text m_error_label;
    private void Start()
    {
        m_unmber = int.Parse(this.name.Substring(5)) + 1;
    }
    private int Verification()
    {
        int correctTimes = 0;
        for (int i = 0; i < keywords.Length; i++)
        {
            if (m_inputField.text.Contains(keywords[i]))
            {
                correctTimes++;
                if (correctTimes.Equals(keywords.Length))
                {
                    m_correct.SetActive(true);
                    return m_unmber;
                }
            }
            else
            {
                m_error.SetActive(true);
                m_error_label.text = "回答错误";
            }
        }
        return -1;
    }

}
