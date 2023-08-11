using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSerialnumberSkin : MonoBehaviour
{
    public Text m_Serialnumber;
    public GameObject m_Icon;
    private void Start()
    {
        
    }
    public void SetSkin(int number)
    {
        m_Serialnumber.text = number.ToString();
    }
    public void SetIconActive(bool isActive)
    {
        m_Icon.SetActive(isActive);
    }
}
