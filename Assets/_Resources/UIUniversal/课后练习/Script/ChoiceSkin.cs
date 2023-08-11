using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceSkin : MonoBehaviour
{
    public Text Title;
    public Text A, B, C, D, E;
    private void Start()
    {
        
    }
    public void SetSkin(string[] data)
    {
        if (data.Length == 3)
        {
            Title.text = data[0];
            A.text = data[1];
            B.text = data[2];
            C.text = "NULL";     
            D.text = "NULL";
            E.text = "NULL";
        }
        else if (data.Length == 4)
        {
            Title.text = data[0];
            A.text = data[1];
            B.text = data[2];
            C.text = data[3];
            D.text = "NULL";
            E.text = "NULL";
        }
        else if (data.Length == 5)
        {
            Title.text = data[0];
            A.text = data[1];
            B.text = data[2];
            C.text = data[3];
            D.text = data[4];
            E.text = "NULL";
        }
        else if (data.Length == 6)
        {
            Title.text = data[0];
            A.text = data[1];
            B.text = data[2];
            C.text = data[3];
            D.text = data[4];
            E.text = data[5];
        }
    }
}
