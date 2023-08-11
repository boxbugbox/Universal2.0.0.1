using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Universal.Function;


public class RotationUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Serializable]
    public class OnValueChanged : UnityEvent<float> { };
    public Canvas m_Canvas;
    public float m_RotateSpeed;
    public GameObject error;
    [PropertySpace(0, 15)] public Button confirm;
    [BoxGroup("Limit"), HorizontalGroup("Limit/Hor_1"), LabelWidth(100f)] public float eulerAnglesMin, eulerAnglesMax;
    [PropertySpace(15)] public OnValueChanged ratationValueChangedEvent;
    public UnityEvent confirmEvent;
    Transform m_Transform;
    private void Start()
    {
        m_Transform = this.transform;
        if (confirm != null) confirm.onClick.AddListener(OnConfirm);
    }
    private void OnConfirm()
    {
        float value = this.transform.localRotation.eulerAngles.z;
        if (value <= eulerAnglesMax && value >= eulerAnglesMin)
        {
            confirmEvent.Invoke();
            Debug.Log(Output.print("正确"));
        }
        else
        {
            error.GetComponent<ErrorManager>().AddErrorEvent(delegate () { InError(); });
            Debug.Log(Output.print("错误：" + value.ToString()));
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        //手指滑动偏移量
        Vector2 mouseXY = eventData.delta;
        mouseXY *= m_RotateSpeed;

        //计算当前物体距离画布左下角位置
        Vector3? curScreenPos = CalculateWorldToScreenPos(transform.position);
        if (curScreenPos == null) return;
        //手指位置偏移量
        Vector2 offset = eventData.position - (Vector2)curScreenPos.Value;

        float value;
        if (Mathf.Abs(mouseXY.x) > Mathf.Abs(mouseXY.y)) // 判断水平滑动还是垂直滑动
        {
            //手指往水平滑动   下面旋转跟随偏移参数  上面与偏移参数相反
            value = mouseXY.x * Mathf.Sign(-offset.y);
        }
        else
        {
            //手指垂直滑动    右边跟随偏移参数    左边与偏移参数相反
            value = mouseXY.y * Mathf.Sign(offset.x);
        }

        transform.Rotate(Vector3.forward, value, Space.Self);

        RotationSlider();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        this.GetComponent<RectTransform>().rotation = new Quaternion(0, 0, 0, 0);
    }
    private Vector3? CalculateWorldToScreenPos(Vector3 worldPos)
    {
        if (m_Canvas.renderMode == RenderMode.ScreenSpaceCamera)
        {
            return m_Canvas.worldCamera.WorldToScreenPoint(worldPos);
        }
        else if (m_Canvas.renderMode == RenderMode.ScreenSpaceOverlay)
        {
            Vector3 screenPos = m_Canvas.transform.InverseTransformPoint(worldPos);
            var rectTrans = m_Canvas.transform as RectTransform;
            screenPos.x += rectTrans.rect.width * 0.5f * rectTrans.localScale.x;
            screenPos.y += rectTrans.rect.height * 0.5f * rectTrans.localScale.y;
            return screenPos;
        }
        return null;
    }
    private void InError()
    {
        float z = UnityEngine.Random.Range(eulerAnglesMin, eulerAnglesMax);
        this.transform.localRotation = Quaternion.Euler(0f, 0f, z);
    }

    /// <summary>
    /// 旋转进度
    /// </summary>
    protected void RotationSlider()
    {
        var z = this.transform.localRotation.eulerAngles.z;
        var difference = 0.0027777777777778;
        var average = 360 - z;
        var differenceValue = average * difference;
        var value = (float)differenceValue;
        ratationValueChangedEvent.Invoke(value);
    }
    
}
