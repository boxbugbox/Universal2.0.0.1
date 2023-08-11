using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/**
 * Data:2021-10-28
 * guqing
 *  摄像机控制类
 */
[AddComponentMenu("Universal/Camera/CameraControl")]
public class CameraControl : MonoBehaviour
{
    #region Private Variable

    /// <summary>
    /// 世界坐标X轴与本地坐标X轴之间的夹角
    /// </summary>
    float angleOfWorldToLocalX;

    /// <summary>
    /// 世界坐标系Y轴与本地坐标系Y轴之间的夹角
    /// </summary>
    float angleOfWorldToLocalY;

    /// <summary>
    /// 摄像机距离观察点的目标距离
    /// </summary>
    float targetDistance;

    /// <summary>
    /// 摄像机距离观察点的当前距离
    /// </summary>
    float currentDistance;

    /// <summary>
    /// 摄像机的目标朝向
    /// </summary>
    Quaternion targetRotation;

    /// <summary>
    /// 摄像机
    /// </summary>
    Camera m_Camera;
    #endregion

    #region Private Method

    private void Start()
    {
        m_Camera = this.GetComponent<Camera>();
        //计算世界坐标系与本地坐标系轴向的夹角
        angleOfWorldToLocalX = Vector3.Angle(Vector3.right, transform.right);
        angleOfWorldToLocalY = Vector3.Angle(Vector3.up, transform.up);
        //计算观察目标与摄像机的距离
        currentDistance = Vector3.Distance(lookTarget.position, transform.position);
        targetDistance = currentDistance;
    }

    //在Update方法之后，在每一帧被调用
    private void LateUpdate()
    {
        if (Input.GetMouseButton(1) && true)
        {
            /*//在鼠标移动与夹角变化之间建立映射关系
            angleOfWorldToLocalX += Input.GetAxis("Mouse X") * sensitivityOfX;
            angleOfWorldToLocalY -= Input.GetAxis("Mouse Y") * sensitivityOfY;
            //处理摄像机旋转角度的限位
            angleOfWorldToLocalX = Mathf.Clamp(angleOfWorldToLocalX,
                minLimitAngleX, maxLimitAngleX);
            angleOfWorldToLocalY = Mathf.Clamp(angleOfWorldToLocalY,
                minLimitAngleY, maxLimitAngleY);
            //计算目标朝向，将欧拉角转换为四元数
            targetRotation = Quaternion.Euler(angleOfWorldToLocalY, angleOfWorldToLocalX, 0);
            //将摄像机向目标朝向进行移动
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);*/

            //RotateAround表示绕着第一个参数进行旋转，其中第一个参数表示围绕player进行旋转，第二个参数表示旋转围绕的轴，在这里表示垂直主角的轴，第三个表示旋转速度
            transform.RotateAround(lookTarget.position, lookTarget.up, rotateSpeed * Input.GetAxis("Mouse X"));
            transform.RotateAround(lookTarget.position, transform.right, -rotateSpeed * Input.GetAxis("Mouse Y"));
        }
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            //a = lookTarget.position
            //b`= transform.forward * currentDistance
            //c = a - b`
            //  = lookTarget.position - transform.forward * currentDistance
            switch (zoommodeType)
            {
                case ZoommodeType.Default:
                    break;
                case ZoommodeType.Distance:
                    //建立滑轮移动与摄像机移动之间的映射关系
                    targetDistance -= Input.GetAxis("Mouse ScrollWheel")
                        * sensitivityOfScrollWheel;
                    //处理摄像机移动距离的限位
                    targetDistance = Mathf.Clamp(targetDistance,
                        minLimitDistance, maxLimitDistance);
                    //计算摄像机与观察点的当前距离
                    currentDistance = Mathf.Lerp(currentDistance,
                        targetDistance, translateSpeed * Time.deltaTime);

                    transform.position =
                 lookTarget.position - transform.forward * currentDistance;
                    break;
                case ZoommodeType.FieldOfView:
                    m_Camera.fieldOfView = Mathf.Clamp(m_Camera.fieldOfView - Input.GetAxis("Mouse ScrollWheel")
                * sensitivityOfScrollWheel,
                minLimitDistance, maxLimitDistance);
                    /*Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView - Input.GetAxis("Mouse ScrollWheel")
                * sensitivityOfScrollWheel,
                minLimitDistance, maxLimitDistance);*/
                    break;
                default:
                    break;
            }
        }
        //按下滚轮拖拽,视角平移
        if (Input.GetMouseButton(2))
        {
            float x = Input.GetAxis("Mouse X");
            float y = Input.GetAxis("Mouse Y");
            var tv = new Vector3(-x, -y) * moveSpeed * Time.deltaTime;
            this.transform.Translate(tv);
        }
        else
        {
            /* //更新摄像机的位置
             transform.position =
                 lookTarget.position - transform.forward * currentDistance;*/
        }

    }

    #endregion

    #region Public Variable
    /// <summary>
    /// 伸缩视角方式
    /// </summary>
    [Header("伸缩视角方式")]
    public ZoommodeType zoommodeType;
    /// <summary>
    /// 摄像机观察的目标
    /// </summary>
    [Header("摄像机观察的目标")]
    public Transform lookTarget;

    /// <summary>
    /// 鼠标在X轴方向移动的灵敏度
    /// </summary>
    private float sensitivityOfX = 5f;
    /// <summary>
    /// 鼠标在Y轴方向移动的灵敏度
    /// </summary>
    private float sensitivityOfY = 5f;

    /// <summary>
    /// 滑轮转动的灵敏度
    /// </summary>
    
    [BoxGroup("速度控制",centerLabel:true)]
    [LabelText("滑轮转动的灵敏度:")]
    [Range(0.0f,10.0f)]
    public float sensitivityOfScrollWheel = 5f;

    /// <summary>
    /// 摄像机的旋转速度
    /// </summary>
    [BoxGroup("速度控制", centerLabel: true)]
    [LabelText("摄像机的旋转速度:")]
    [Range(0.0f, 10.0f)]
    public float rotateSpeed = 1f;

    [BoxGroup("速度控制", centerLabel: true)]
    [LabelText("摄像机平移速度:")]
    [Range(0.0f, 10.0f)]
    public float translateSpeed = 1f;

    /// <summary>
    /// 模型拖拽速度
    /// </summary>
    [BoxGroup("速度控制", centerLabel: true)]
    [LabelText("模型拖拽速度:")]
    [Range(0.0f, 10.0f)]
    public float moveSpeed = 1f;

    /// <summary>
    /// X轴最小限位角度
    /// </summary>
    public float minLimitAngleX = -360f;

    /// <summary>
    /// X轴最大限位角度
    /// </summary>
    public float maxLimitAngleX = 360f;

    /// <summary>
    /// Y轴最小限位角度
    /// </summary>
    public float minLimitAngleY = -360f;

    /// <summary>
    /// Y轴最大限位角度
    /// </summary>
    public float maxLimitAngleY = 360f;

    /// <summary>
    /// 最小观察距离
    /// </summary>
    public float minLimitDistance = 2f;

    /// <summary>
    /// 最大观察距离
    /// </summary>
    public float maxLimitDistance = 80f;

    #endregion
}

public enum ZoommodeType
{
    Default,
    Distance,
    FieldOfView
}