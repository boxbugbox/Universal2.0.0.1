using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseEnterTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("提示框")] public GameObject tips;
    public Sprite m_Sprite;
    public RectTransform m_canvas;

    Camera UICamera;   
    Image m_Image;
    private void Start()
    {
        m_Image = tips.GetComponent<Image>();
        UICamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        OverlayStart();
    }
    private void Update()
    {
        Vector2 position = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(m_canvas, Input.mousePosition, UICamera, out position);
        tips.transform.localPosition = position;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        m_Image.sprite = m_Sprite;
        m_Image.SetNativeSize();
        tips.SetActive(true);
        OverlayMouseEnter();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        tips.SetActive(false);
        OverlayMouseExit();
    }
    protected virtual void OverlayStart() { }
    protected virtual void OverlayMouseEnter(){}
    protected virtual void OverlayMouseExit(){}
}
