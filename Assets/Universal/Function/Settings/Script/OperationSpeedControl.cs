using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal;

public class OperationSpeedControl : MonoBehaviour
{
    public Slider operationSpeed;
    private void Start()
    {
        operationSpeed.onValueChanged.AddListener((float slider) => SetOperationSpeed());
    }
    private void SetOperationSpeed()
    {
        if (operationSpeed.value.Equals(0))
        {
            Time.timeScale = 0.5f;
            UniversalAudioSourceControl.universalAudioSourceControl.SetPitch(0.5f);
        }
        if (operationSpeed.value.Equals(1))
        {
            Time.timeScale = 1f;
            UniversalAudioSourceControl.universalAudioSourceControl.SetPitch(1f);
        }
        if (operationSpeed.value.Equals(2))
        {
            Time.timeScale = 2f;
            UniversalAudioSourceControl.universalAudioSourceControl.SetPitch(2f);
        }
        if (operationSpeed.value.Equals(3))
        {
            Time.timeScale = 3f;
            UniversalAudioSourceControl.universalAudioSourceControl.SetPitch(3f);
        }
    }
}
