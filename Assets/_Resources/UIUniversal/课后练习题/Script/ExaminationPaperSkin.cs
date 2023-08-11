using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExaminationPaperSkin : MonoBehaviour
{
    public Text m_Title;
    public Text A, B, C, D, E;
    public void SetSkin(string[] data)
    {
        if (data.Length == 3)
        {
            m_Title.text = data[0];
            A.text = "A、"+data[1];
            B.text = "B、" + data[2];
            C.transform.parent.gameObject.SetActive(false);
            D.transform.parent.gameObject.SetActive(false);
            E.transform.parent.gameObject.SetActive(false);
        }
        else if (data.Length == 4)
        {
            m_Title.text = data[0];
            A.text = "A、" + data[1];
            B.text = "B、" + data[2];
            C.text = "C、" + data[3];
            D.transform.parent.gameObject.SetActive(false);
            E.transform.parent.gameObject.SetActive(false);
        }
        else if (data.Length == 5)
        {
            m_Title.text = data[0];
            A.text = "A、" + data[1];
            B.text = "B、" + data[2];
            C.text = "C、" + data[3];
            D.text = "D、" + data[4];
            E.transform.parent.gameObject.SetActive(false);
        }
        else if (data.Length == 6)
        {
            m_Title.text = data[0];
            A.text = "A、" + data[1];
            B.text = "B、" + data[2];
            C.text = "C、" + data[3];
            D.text = "D、" + data[4];
            E.text = "E、" + data[5];
        }
    }
}
