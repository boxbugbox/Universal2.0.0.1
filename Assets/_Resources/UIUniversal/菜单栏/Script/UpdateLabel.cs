using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLabel : MonoBehaviour
{
    public Text m_Text;
    public Sprite m_Sprite;
    public string EnterColor, EndColor;
    public FontStyle EnterStyle, EndStyle;
    private Toggle m_Toggle;
    private void Start()
    {
        m_Toggle = GetComponent<Toggle>();
        m_Toggle.onValueChanged.AddListener((bool isOn) => OnToggle(isOn));
    }
    private void OnToggle(bool isOn)
    {
        if (isOn)
        {
            this.GetComponent<Image>().overrideSprite = m_Sprite;
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
    [Button("Update Label"),PropertySpace(15)]
    private void UpdateLabelMethod()
    {
        if (m_Text != null)
        {
            m_Text.text = this.gameObject.name;
        }
    }
    public void Init()
    {
        this.GetComponent<Image>().overrideSprite = null;
    }
}
