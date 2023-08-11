using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Universal.Medicine;

public class OnSliderValueChangedView : MonoBehaviour
{
    public Slider m_slider;
    public Button confirm;
    [PropertySpace(0f, 15f)] public GameObject error;
    [BoxGroup("Box", false)] public float m_max, m_min;
    [PropertySpace(15)] public UnityEvent choiceEvent;
    private void Start()
    {
        m_slider.value = m_slider.minValue;
        confirm.onClick.AddListener(OnConfirm);
        m_slider.onValueChanged.AddListener((float value) => OnSlider(value));
        BaseOnStart();
    }
    private void OnConfirm()
    {
        if (m_slider.value <= m_max && m_slider.value >= m_min)
        {
            choiceEvent?.Invoke();
        }
        else
        {
            error.GetComponent<ErrorManager>().AddErrorEvent(delegate () { InError(); });
        }
    }
    private void OnSlider(float value)
    {
        SliderValueChanged(value);
    }
    protected virtual void InError()
    {
        m_slider.value = Random.Range(m_max, m_min);
    }
    private void OnDisable(){ BaseOnDisable();}
    protected virtual void SliderValueChanged(float value){}
    protected virtual void BaseOnStart() { }
    protected virtual void BaseOnDisable() { m_slider.value = m_slider.minValue; }
}
