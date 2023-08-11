using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnToggleStyleView : MonoBehaviour
{
    private Toggle m_Toggle;
    public Text m_Text;
    public string EnterColor, EndColor;
    public GameObject Icon;
    public FontStyle EnterStyle, EndStyle;
    public bool PlayOnAwake;
    private void Start()
    {
        m_Toggle = this.GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener((bool isOn) => OnToggle(isOn));
        Init();
    }
    private void OnToggle(bool isOn)
    {
        IconSetActive(isOn);
        if (isOn)
        {
            ColorUtility.TryParseHtmlString("#" + EnterColor, out Color nowColor);
            m_Text.color = nowColor;
            m_Text.fontStyle = EnterStyle;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#" + EndColor, out Color nowColor);
            m_Text.color = nowColor;
            m_Text.fontStyle = EndStyle;
        }
    }
    private void IconSetActive(bool isActive)
    {
        if (Icon != null)
            Icon.SetActive(isActive);
    }
    protected virtual void Init()
    {
        OnToggle(PlayOnAwake);
    }

}
