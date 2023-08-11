using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicSprite : MonoBehaviour
{
    public Sprite[] sprites;
    private Image m_Image;
    public bool isOverlay;

    public bool IsOverlay { get => isOverlay; set => isOverlay = value; }

    private void Start()
    {
        isOverlay = false;
        m_Image = this.GetComponent<Image>();
    }
    /// <summary>
    /// 覆盖精灵
    /// </summary>
    /// <param name="index"></param>
    public void OverlaySprite(int index)
    {
        isOverlay = true;
        m_Image.overrideSprite = sprites[index];
    }
    private void OnDisable()
    {
        isOverlay = false;
        m_Image.overrideSprite = null;
    }

}
