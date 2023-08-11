using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatData : MonoBehaviour
{
    [TextArea(5, 10)] public string m_data;
    public Text self_adaptation, content;
    public OnChatInputView inputView;
    [LabelText("医生")]public int dioIndex;
    [LabelText("病人")] public int patients;
    Button m_Button;
    private void Start()
    {
        self_adaptation.text = content.text = m_data;
        SetHeight();
        m_Button = this.GetComponent<Button>();
        if (m_Button != null)
        {
            m_Button.onClick.AddListener(OnClickSend);
        }        
    }
    float chatItemTextMaxWidth = 311f;
    private void SetHeight()
    {
        RectTransform rect = self_adaptation.GetComponent<RectTransform>();
        float curTextWidth = self_adaptation.preferredWidth;
        if (curTextWidth >= chatItemTextMaxWidth)
        {
            rect.sizeDelta = new Vector2(311f, rect.rect.height);
            self_adaptation.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            self_adaptation.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(404.7f, self_adaptation.preferredHeight + 52f);
        }
    }
    private void OnClickSend()
    {
        StopAllCoroutines();
        inputView.SetMessage("Doctor", m_data);
        try
        {
            Universal.UniversalAudioSourceControl.universalAudioSourceControl.PlayClips(dioIndex);
            StartCoroutine(DelayPlay());
        }
        catch (System.Exception e)
        {

            Debug.Log("<color=red>NULL Reference Exception：</color>" + e.ToString());
        }        
        //inputView.OnOpenChatRecord(); //点击消息关闭      
    }
    IEnumerator DelayPlay()
    {       
        if (patients > -1)
        {
            float times = Universal.UniversalAudioSourceControl.universalAudioSourceControl.GetAudioLength(dioIndex, SoundType.ConversationalSound);
            Debug.Log(times);
            yield return new WaitForSeconds(times);
            Universal.UniversalAudioSourceControl.universalAudioSourceControl.PlayClips(patients);
        }       
    }
}
