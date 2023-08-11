using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal;
using Universal.Function;

public class HomeManage : MonoBehaviour
{
    public Button[] m_Button;
    public GameObject loading;
    private UniversalOverall m_DontDestroyOnLoadControl;
    private void Start()
    {
        m_DontDestroyOnLoadControl = UniversalOverall.universalOverall;
        for (int i = 0; i < m_Button.Length; i++)
        {
            int k = i;
            m_Button[k].onClick.AddListener(() => OnButtonClick(k));
        }
        TitleGroupManage.titleGroupManage.SetActive(true);
        UniversalMenuControl.universalMenuControl.SetMenuActive(false);
        loading.SetActive(false);
    }
    private void OnButtonClick(int index)
    {
        if (index == 0)
        {
            Debug.Log("基础知识学习");
        }
        else if (index == 1)
        {
            loading.SetActive(true);
            m_DontDestroyOnLoadControl.PatternType = PatternType.学习;
        }
        else if (index == 2)
        {
            loading.SetActive(true);
            m_DontDestroyOnLoadControl.PatternType = PatternType.考核;
            m_DontDestroyOnLoadControl.Questions.Clear();
        }
        else if (index == 3)
        {
            Debug.Log("课后练习");
        }
        try
        {
            UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(3);
        }
        catch (System.Exception e)
        {
            Debug.Log(Output.print(this.name + ":" + e.Message));
        }       
    }
}
