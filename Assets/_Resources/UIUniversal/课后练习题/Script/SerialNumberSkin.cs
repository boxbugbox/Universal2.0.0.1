using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SerialNumberSkin : MonoBehaviour
{
    public Toggle m_toggle;
    public Text m_number;
    public GameObject m_icon;

    public void SetNumber(int number)
    {
        m_number.text = number.ToString();
    }
    public void SetIconActive(bool isActive)
    {
        m_icon.SetActive(isActive);
    }
}
