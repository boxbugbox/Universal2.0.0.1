using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal.Function;

public class QualityControl : MonoBehaviour
{
    #region 1.0
    /*/// <summary>
    /// 底
    /// </summary>
    [Header("低")] public Toggle _bottomLevel;
    /// <summary>
    /// 中
    /// </summary>
    [Header("中")] public Toggle _middle;
    /// <summary>
    /// 高
    /// </summary>
    [Header("高")] public Toggle _advanced;

    private void Start()
    {
        _bottomLevel.onValueChanged.AddListener((bool isOn) => { OnSetBottomLevel(_bottomLevel, isOn); });
        _middle.onValueChanged.AddListener((bool isOn) => { OnSetMiddle(_middle, isOn); });
        _advanced.onValueChanged.AddListener((bool isOn) => { OnSetAdvanced(_advanced, isOn); });
        QualitySettings.SetQualityLevel(5, true);
    }
    /// <summary>
    /// 设置高级画质
    /// </summary>
    /// <param name="advanced"></param>
    /// <param name="isOn"></param>
    private void OnSetAdvanced(Toggle advanced, bool isOn)
    {
        if (isOn)
        {
            QualitySettings.SetQualityLevel(5, true);
        }
        else
        {
            Debug.Log("Init");
        }
    }
    /// <summary>
    /// 设置中级画质
    /// </summary>
    /// <param name="middle"></param>
    /// <param name="isOn"></param>
    private void OnSetMiddle(Toggle middle, bool isOn)
    {
        if (isOn)
        {
            QualitySettings.SetQualityLevel(3, true);
        }
        else
        {
            Debug.Log("Init");
        }
    }
    /// <summary>
    /// 设置低级画质
    /// </summary>
    /// <param name="bottom_level"></param>
    /// <param name="isOn"></param>
    private void OnSetBottomLevel(Toggle bottom_level, bool isOn)
    {
        if (isOn)
        {
            QualitySettings.SetQualityLevel(0, true);
        }
        else
        {
            Debug.Log("Init");
        }
    }*/
    #endregion

    public Slider qualitySlider;

    private void Start()
    {
        qualitySlider.onValueChanged.AddListener((float slider) => SetQuality());
        QualitySettings.SetQualityLevel(5, true);
    }
    private void SetQuality()
    {
        if (qualitySlider.value.Equals(0))
        {
            QualitySettings.SetQualityLevel(3, true);
            Debug.Log(Output.print("设置中级质量"));
        }
        if (qualitySlider.value.Equals(1))
        {
            QualitySettings.SetQualityLevel(5, true);
            Debug.Log(Output.print("设置高级质量"));
        }
        if (qualitySlider.value.Equals(2))
        {
            QualitySettings.SetQualityLevel(0, true);
            Debug.Log(Output.print("设置低级质量"));
        }
    }
}
