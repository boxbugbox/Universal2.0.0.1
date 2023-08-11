using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDialogView : MonoBehaviour
{
    public Text m_WorldDialogue, m_Text;
    public void SetContent(string conten,int index)
    {
        this.gameObject.SetActive(true);
        m_WorldDialogue.text = m_Text.text = conten;
        try
        {
            Universal.UniversalAudioSourceControl.universalAudioSourceControl.PlayClips(index);
        }
        catch (System.Exception e)
        {
            Debug.Log("<color=red> NULL </color>" + e.Message);
        }
       
    }
    public void SetStatus(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
}
