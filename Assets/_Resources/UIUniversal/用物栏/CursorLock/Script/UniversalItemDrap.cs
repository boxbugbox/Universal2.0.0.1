using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Universal;
using Universal.Function;
using Universal.Medicine;

public class UniversalItemDrap : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform dragParent;
    [PropertySpace(15)] public UnityEvent onDragEvent;
    PatternType patternType;
    Vector2 v2;
    Transform parent;
    CircleCollider2D circle;
    RectTransform rectTransform;
    private void Start()
    {
        //dragParent = GameObject.FindGameObjectWithTag("Drag").transform;
        //mousePainter = GameObject.FindGameObjectWithTag("UICamera").GetComponent<MousePainter>();
        parent = this.transform.parent;
        v2 = this.transform.localPosition;
        rectTransform = this.GetComponent<RectTransform>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        circle = this.transform.GetComponent<CircleCollider2D>();
        if (circle != null)
            circle.isTrigger = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;
        this.transform.SetParent(dragParent);
        this.transform.SetAsLastSibling();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.transform.SetParent(parent);
        this.transform.localPosition = v2;
        if (circle != null)
            circle.isTrigger = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var cm = GameObject.FindGameObjectWithTag("ControllerManger").GetComponent<ControllerManger>();
        patternType = cm.patternType;
        Question question = new Question();
        question.title = "选择" + CursorLock.cursorLock.Type.ToString();
        if (Verification())
        {
            switch (patternType)
            {
                case PatternType.Default:
                    break;
                case PatternType.学习:
                    break;
                case PatternType.考核:
                    question.score = 2.5f;
                    break;
                default:
                    break;
            }
            collision.gameObject.SetActive(false);
        }
        else if (false)
        {
            //collision.gameObject.SetActive(false);
        }
        else
        {
            switch (patternType)
            {
                case PatternType.Default:
                    break;
                case PatternType.学习:
                    break;
                case PatternType.考核:
                    collision.gameObject.SetActive(false);
                    question.score = 0f;
                    UniversalOverall.universalOverall.Questions.Add(question);
                    cm.OnNextStep();
                    break;
                default:
                    break;
            }
            Debug.Log(Output.print("错误"));
            return;
        }
        switch (patternType)
        {
            case PatternType.Default:
                break;
            case PatternType.学习:
                onDragEvent?.Invoke();
                break;
            case PatternType.考核:
                UniversalOverall.universalOverall.Questions.Add(question);
                onDragEvent?.Invoke();
                break;
            default:
                break;
        }
    }
    private bool Verification()
    {
        foreach (var type in Enum.GetValues(typeof(NursingType)))
        {
            if (gameObject.name.Equals(type.ToString()) && CursorLock.cursorLock.GetCursorType().Equals(type))
            {               
                return true;
            }
        }
        return false;
    }
}
