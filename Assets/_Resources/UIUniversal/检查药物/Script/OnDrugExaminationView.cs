using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDrugExaminationView : MonoBehaviour
{
    public Button[] m_click;
    public Toggle[] m_toggle;
    private void Start()
    {
        for (int i = 0; i < m_click.Length; i++)
        {
            int j = i;
            m_click[j].onClick.AddListener(() => OnClickOpen(j));
            m_toggle[j].onValueChanged.AddListener((bool isOn) => OnToggle(j, isOn));
        }
    }
    private void OnClickOpen(int j)
    {
        m_click[j].gameObject.GetComponent<UIEffect>().Init();
        m_toggle[j].isOn = true;
    }
    private void OnToggle(int j, bool isOn)
    {
        if (isOn)
        {
            var index = j + 1;
            if (index < m_click.Length)
            {
                m_click[index].gameObject.SetActive(true);
            }
        }
    }
    private void OnDisable()
    {
        for (int i = 0; i < m_click.Length; i++)
        {
            if (i == 0)
            {
                m_click[i].gameObject.SetActive(true);
            }
            else
            {
                m_click[i].gameObject.SetActive(false);
            }
            m_click[i].gameObject.GetComponent<UIEffect>().UIStart();
            m_toggle[i].isOn = false;
        }
    }
}
