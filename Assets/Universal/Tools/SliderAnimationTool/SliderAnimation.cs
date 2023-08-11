using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal;

public class SliderAnimation : MonoBehaviour
{
    public string[] m_name;
    public string aname;
    public Slider _slider;
    private void Start()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChange);
    }
    public void OnSliderValueChange(float value)
    {
        UniversalAnimatorControl.universalAnimatorControl.PlayAnimator(m_name, aname, 0, value);
        Debug.Log("进度：" + value);
    }
}
