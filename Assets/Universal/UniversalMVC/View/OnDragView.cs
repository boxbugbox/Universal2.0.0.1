using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Universal;

public class OnDragView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [PropertySpace(15)] public UnityEvent onDragEvent;

    Vector2 v2;
    Transform parent;
    CircleCollider2D circle;
    RectTransform rectTransform;
    private void Start()
    {
        parent = this.transform.parent;
        v2 = this.transform.localPosition;
        rectTransform = this.GetComponent<RectTransform>();
        OnStart();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parent);
        circle = this.transform.GetComponent<CircleCollider2D>();
        if (circle != null)
            circle.isTrigger = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out pos);
        rectTransform.position = pos;
        this.transform.SetAsLastSibling();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (circle != null)
            circle.isTrigger = false;
        this.transform.localPosition = v2;
    }
    private void OnDisable()
    {
        this.transform.localPosition = v2;
        OnDisables();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OverwriteOnTriggerEnter2D(collision);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        OverwriteOnTriggerExit2D(collision);
    }
    protected virtual void OverwriteOnTriggerEnter2D(Collider2D collision) { }
    protected virtual void OverwriteOnTriggerExit2D(Collider2D collision) { }
    protected virtual void OnStart() { }
    protected virtual void OnDisables() { }
}
