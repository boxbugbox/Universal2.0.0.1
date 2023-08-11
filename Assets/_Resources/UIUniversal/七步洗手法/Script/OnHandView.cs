using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHandView : MonoBehaviour
{
    public Toggle[] m_Toggles;
    private void OnEnable()
    {
        StartCoroutine(StartHighlight());
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
    IEnumerator StartHighlight()
    {
        m_Toggles[0].isOn = true;
        yield return new WaitForSeconds(6.125f);
        m_Toggles[1].isOn = true;
        yield return new WaitForSeconds(4.292f);
        m_Toggles[2].isOn = true;
        yield return new WaitForSeconds(3.625f);
        m_Toggles[3].isOn = true;
        yield return new WaitForSeconds(19.417f);
        m_Toggles[4].isOn = true;
        yield return new WaitForSeconds(7.583f);
        m_Toggles[5].isOn = true;
        yield return new WaitForSeconds(8.583f);
        m_Toggles[6].isOn = true;
        //yield return new WaitForSeconds(10.125f);
    }
}
