using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;
using Universal;

[AddComponentMenu("Universal/MouseEnter/MouseEnterPlayAudio")]
public class MouseEnterPlayAudio : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [Header("播放模式")]
    [PropertySpace(10f, 15f)]
    public AudioPlayType playType;
    [BoxGroup("GROUP", centerLabel: true)]
    [HorizontalGroup("GROUP/Item", width: 0)]
    [LabelText("播放索引")]
    public int playIndex;
    [BoxGroup("GROUP")]
    [HorizontalGroup("GROUP/Item")]
    [LabelText("打开")]
    public bool isOpen;

    private UniversalAudioSourceControl universalAudioSourceControl;
    private void Start()
    {
        universalAudioSourceControl = UniversalAudioSourceControl.universalAudioSourceControl;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (isOpen)
        {
            switch (playType)
            {
                case AudioPlayType.Default:
                    break;
                case AudioPlayType.Down:
                    try
                    {
                        universalAudioSourceControl.PlayEffect(playIndex);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(this.name + e.Message);
                    }
                    
                    break;
                case AudioPlayType.Enter:
                    break;
                default:
                    break;
            }
        }
               
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isOpen)
        {
            switch (playType)
            {
                case AudioPlayType.Default:
                    break;
                case AudioPlayType.Down:
                    break;
                case AudioPlayType.Enter:
                    try
                    {
                        universalAudioSourceControl.PlayEffect(playIndex);
                    }
                    catch (System.Exception e)
                    {
                        Debug.Log(this.name + e.Message);
                    }                    
                    break;
                default:
                    break;
            }
        }      
    }
    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
public enum AudioPlayType 
{
    [LabelText("Default:默认")] Default,
    [LabelText("Down:按下播放")] Down,
    [LabelText("Enter:移入播放")] Enter
}

