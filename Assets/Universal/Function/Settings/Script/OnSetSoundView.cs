using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal;
using Universal.Function;

public class OnSetSoundView : ClickButtonToggle
{
    public SoundType soundType;
    public GameObject open;
    public GameObject close;
    private UniversalAudioSourceControl universalAudioSourceControl;
    protected override void Start()
    {
        base.Start();
        universalAudioSourceControl =  UniversalAudioSourceControl.universalAudioSourceControl;
        OnOpen();
    }
    protected override void OnOpen()
    {
        open.SetActive(true);
        close.SetActive(false);
        switch (soundType)
        {
            case SoundType.Default:
                break;
            case SoundType.BackgroundMusic:
                universalAudioSourceControl.OpenSound(soundType);
                break;
            case SoundType.ConversationalSound:
                universalAudioSourceControl.OpenSound(soundType);
                break;
            case SoundType.PromptSound:
                universalAudioSourceControl.OpenSound(soundType);
                break;
            default:
                break;
        }
    }
    protected override void OnClose()
    {
        open.SetActive(false);
        close.SetActive(true);
        switch (soundType)
        {
            case SoundType.Default:
                break;
            case SoundType.BackgroundMusic:
                universalAudioSourceControl.CloseSound(soundType);
                break;
            case SoundType.ConversationalSound:
                universalAudioSourceControl.CloseSound(soundType);
                break;
            case SoundType.PromptSound:
                universalAudioSourceControl.CloseSound(soundType);
                break;
            default:
                break;
        }
    }
}
public enum SoundType
{
    Default,
    [LabelText("BackgroundMusic:背景音乐")] BackgroundMusic,
    [LabelText("ConversationalSound:对话声音")] ConversationalSound,
    [LabelText("PromptSound:提示声音")] PromptSound
}
