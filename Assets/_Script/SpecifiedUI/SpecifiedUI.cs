using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpecifiedUI : MonoBehaviour, IPointerDownHandler
{
    public Camera m_Camera;
    public Image m_ClickIcon;
    public Button m_Confirm;  
    [PropertySpace(0,15)] public GameObject m_Error;
    [BoxGroup("Limit"),HorizontalGroup("Limit/Hor_1"), LabelWidth(40f)] public float xMin, xMax;
    [BoxGroup("Limit"), HorizontalGroup("Limit/Hor_2"), LabelWidth(40f)] public float yMin, yMax;
    [PropertySpace(15)] public UnityEvent choiceEvent;   
    RectTransform m_RectTransform;
    private void Start()
    {
        m_RectTransform = this.GetComponent<RectTransform>();
        m_Confirm.onClick.AddListener(ConfirmResult);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_ClickIcon.transform.localPosition = GetLocalUI(m_RectTransform);
            Debug.Log("<color=#9C00FF>" + GetLocalUI(m_RectTransform) + "</color>");
            Debug.Log("<color=#9C00FF>" + m_ClickIcon.transform.localPosition + "</color>");
        }
    }
    public void ConfirmResult()
    {
        var v2 = m_ClickIcon.transform.localPosition;
        if ((v2.x > xMin && v2.x < xMax) && (v2.y > yMin && v2.y < yMax))
        {
            choiceEvent.Invoke();
            Debug.Log("正确");
        }
        else
        {
            Debug.Log("<color=red> X:" + v2.x + "Y:" + v2.y + "</color>");
            m_Error.GetComponent<ErrorManager>().AddErrorEvent(() => InError());
        }
    }
    private void InError()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        m_ClickIcon.transform.localPosition = new Vector2(x, y);
    }
    /// <summary>
    /// 获取局部UI在屏幕的位置
    /// </summary>
    /// <param name="rect"></param>
    /// <returns></returns>
    private Vector2 GetLocalUI(RectTransform rect)
    {
        //Vector3 position;
        //RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, Input.mousePosition, m_Camera, out position);
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, m_Camera, out position);
        return position;
    }   
}
