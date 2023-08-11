using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnInformationView : MonoBehaviour
{
    public PositionType positionType;
    public bool isRun;
    public Vector3 m_Position;
    public Transform m_Transform;
    Camera uiCamera;
    protected virtual void Start()
    {
        uiCamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }
    private void Update()
    {
        if (isRun)
        {
            switch (positionType)
            {
                case PositionType.Default:
                    break;
                case PositionType.Position:
                    Vector2 pos = Camera.main.WorldToScreenPoint(m_Position);
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(), pos, uiCamera, out Vector3 v2);
                    this.transform.position = v2;
                    break;
                case PositionType.Transform:
                    Vector2 tf = Camera.main.WorldToScreenPoint(m_Transform.position);
                    RectTransformUtility.ScreenPointToWorldPointInRectangle(this.GetComponent<RectTransform>(), tf, uiCamera, out Vector3 tfv2);
                    this.transform.position = tfv2;
                    break;
                default:
                    break;
            }
        }
    }
}
public enum PositionType
{
    Default,
    Position,
    Transform,
}
