using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsageTime : MonoBehaviour
{
    public Text m_text;

    private void OnEnable()
    {
        m_text.text = TitleGroupManage.titleGroupManage.assessmentTimes.recealText.text;
    }

}
