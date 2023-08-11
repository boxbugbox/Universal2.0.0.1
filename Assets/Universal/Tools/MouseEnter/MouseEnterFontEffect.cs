using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[AddComponentMenu("Universal/MouseEnter/MouseEnterFontEffect")]
public class MouseEnterFontEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("字体样式")] public FontStyleType _fontStyle;
    [Header("增大字号")] public int _font_size;
    [Header("开启字体颜色变化")] public bool isColor;
    [Header("选择要变化的颜色")] public Color _AddColor;

    Color _color;
    int _size;
    private void Start()
    {
        _size = this.GetComponent<Text>().fontSize;
        _color = this.GetComponent<Text>().color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        switch (_fontStyle)
        {
            case FontStyleType.Normal:
                this.GetComponent<Text>().fontStyle = FontStyle.Normal;
                break;
            case FontStyleType.Bold:
                this.GetComponent<Text>().fontStyle = FontStyle.Bold;
                break;
            default:
                break;
        }      
        this.GetComponent<Text>().fontSize = _font_size;
        if (isColor)
        {
            this.GetComponent<Text>().color = _AddColor;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        this.GetComponent<Text>().fontStyle = FontStyle.Normal;
        this.GetComponent<Text>().fontSize = _size;
        if (isColor)
        {
            this.GetComponent<Text>().color = _color;
        }
    }
}
public enum FontStyleType
{
    Normal,
    Bold
}