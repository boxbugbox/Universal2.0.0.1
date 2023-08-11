using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSpeedControl : MonoBehaviour
{
   [Header("控制条")] public Slider _speed_control;
   [Header("摄像机控制脚本")] public CameraMove cameraMove;

    private void Start()
    {
        _speed_control.onValueChanged.AddListener((float slider) => OnSliderValue());
    }
    public void OnSliderValue()
    {
        cameraMove = Camera.main.GetComponent<CameraMove>();
        cameraMove.speed = _speed_control.value;
        cameraMove.rotationLerpTime = 1 - ((_speed_control.value / 3f) - 0.1f);
    }
}