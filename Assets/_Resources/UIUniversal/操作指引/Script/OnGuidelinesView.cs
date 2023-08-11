using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Function;

public class OnGuidelinesView : ClickEvents
{
    public GameObject _nextObject;
    protected override void OnPress()
    {
        this.transform.parent.gameObject.SetActive(false);
        _nextObject.SetActive(true);
        Universal.UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(6);
    }
}
