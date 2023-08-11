using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal;

public class KnowledgeManager : MonoBehaviour
{
    public AudioSource m_audioSource;
    public AudioClip[] audios;
    public Toggle[] m_Toggles;
    private void Start()
    {
       /* string basicknowledge_url = @"file://" + Application.streamingAssetsPath + @"\StandaloneWindows\basicknowledge.universal";
        if (basicknowledge_url != null)
        {
            UniversalOverall.universalOverall.StartDownload("基础知识", basicknowledge_url, delegate (AssetBundle bCallback) {
                audios = bCallback.LoadAllAssets<AudioClip>();
            });
        }

        for (int i = 0; i < m_Toggles.Length; i++)
        {
            int k = i;
            m_Toggles[k].onValueChanged.AddListener((bool isOn) => PlayAudio(isOn, k));
        }*/
    }
    public void PlayAudio(bool isPlay,int index)
    {
        m_audioSource.Stop();
        if (isPlay)
        {
            if (audios != null)
            {
                m_audioSource.clip = audios[index];
                m_audioSource.Play();
            }
        }
    }
}
