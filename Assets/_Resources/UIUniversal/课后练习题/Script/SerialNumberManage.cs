using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialNumberManage : MonoBehaviour
{
    public Toggle[] m_SerialNumber;
    public ScrollRect m_ScrollRect;
    private void Start()
    {
        for (int i = 0; i < m_SerialNumber.Length; i++)
        {
            int k = i;
            m_SerialNumber[k].onValueChanged.AddListener((bool isOn) => OnSerialNumber(k, isOn));
        }
    }
    private void OnSerialNumber(int k,bool isOn)
    {
        if (isOn)
        {
            var average = 1f / m_SerialNumber.Length;
            var value = average * k;
            m_ScrollRect.verticalScrollbar.value = (1f - value);
        }
    }
}
