using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("Universal/MouseEnter/MouseEnterImageEffect")]
public class MouseEnterImageEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("鼠标移入显示的物体")] public GameObject _m_game;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _m_game.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _m_game.SetActive(false);
    }
}