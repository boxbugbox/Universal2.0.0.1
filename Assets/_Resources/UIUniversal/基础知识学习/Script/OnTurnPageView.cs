using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTurnPageView : MonoBehaviour
{
    public Toggle[] m_Toggles;
    public Button m_Add;
    public Button m_Minus;
    public float m_max, m_min;
    int TurnPageindex;
    private void Start()
    {
        m_Add.onClick.AddListener(OnAdd);
        m_Minus.onClick.AddListener(OnMinus);
    }
    private void OnAdd()
    {
        if(TurnPageindex < m_max)
        {
            TurnPageindex++;
            m_Toggles[TurnPageindex].isOn = true;
        }
    }
    private void OnMinus()
    {
        if (TurnPageindex > m_min)
        {
            TurnPageindex--;
            m_Toggles[TurnPageindex].isOn = true;
        }           
    }
}
