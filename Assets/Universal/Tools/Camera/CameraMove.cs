using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;
/**
 * Data:2021-10-28
 * guqing
 * 摄像机移动
 */
[AddComponentMenu("Universal/Camera/CameraMove")]
public class CameraMove : MonoBehaviour
{

    #region -------------------------CameraState-------------------------
    class CameraState
    {
        public float x, y, z;
        public float pitch; //X 俯仰角
        public float yaw;   //Y 偏航角
        public float roll;  //Z 翻滚角

        /// <summary>
        /// 获取 目标Trans 的数值
        /// </summary>
        /// <param name="t"></param>
        public void SetFromTransform(Transform t)
        {
            pitch = t.eulerAngles.x;
            yaw = t.eulerAngles.y;
            roll = t.eulerAngles.z;

            x = t.position.x;
            y = t.position.y;
            z = t.position.z;
            myCamera = t.GetComponent<Camera>();
        }

        /// <summary>
        /// 计算移动和旋转
        /// </summary>
        /// <param name="target"></param>
        /// <param name="positionLerpPct"></param>
        /// <param name="rotationLerpPct"></param>
        public void LerpTowards(CameraState target, float positionLerpPct, float rotationLerpPct)
        {
            yaw = Mathf.Lerp(yaw, target.yaw, rotationLerpPct);
            pitch = Mathf.Lerp(pitch, target.pitch, rotationLerpPct);
            roll = Mathf.Lerp(roll, target.roll, rotationLerpPct);

            x = Mathf.Lerp(x, target.x, positionLerpPct);
            y = Mathf.Lerp(y, target.y, positionLerpPct);
            z = Mathf.Lerp(z, target.z, positionLerpPct);
        }

        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="translation"></param>
        public void Translate(Vector3 translation)
        {
            //计算一个带有旋转角的位置
            Vector3 rotatedTranslation = Quaternion.Euler(pitch, yaw, roll) * translation;

            x += rotatedTranslation.x;
            y += rotatedTranslation.y;
            z += rotatedTranslation.z;
        }

        /// <summary>
        /// 重置设置
        /// </summary>
        /// <param name="t"></param>
        public void ReSet(Transform t)
        {
            pitch = yaw = roll = 0;
            x = y = z = 0;
            UpdateTransform(t);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="t"></param>
        public void UpdateTransform(Transform t)
        {
            t.eulerAngles = new Vector3(pitch, yaw, roll);
            t.position = new Vector3(x, y, z);
        }

        private Camera myCamera;

        /// <summary>
        /// 变换摄像机视角
        /// </summary>
        /// <param name="temp"></param>
        public void ChangeFieldView(float temp)
        {
            myCamera.fieldOfView = Mathf.Clamp(myCamera.fieldOfView + temp, 20, 100);
        }
    }
    #endregion

    CameraState targer = new CameraState();
    CameraState interpolating = new CameraState();

    /// <summary>
    /// 相机是否可以移动
    /// </summary>
    [Header("停止移动相机")]
    public bool isControl;
    /// <summary>
    /// 速度
    /// </summary>
    //[HideInInspector]
    [Range(0.1f, 5f)]
    [Header("移动速度")]
    public float speed = 1.5f;
    /// <summary>
    /// 旋转插值速度
    /// </summary>
    [Range(0.10f, 1.2f)]
    [Header("旋转插值速度")]
    public float rotationLerpTime = 0.5f; 
    /// <summary>
    /// 鼠标灵敏度
    /// </summary>
    [Header("鼠标灵敏度")]
    public AnimationCurve mouseSensitivityCurve =
        new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));
    /// <summary>
    /// 位置插值速度
    /// </summary> 
    [Header("位置插值速度")]
    public float positionLerpTime = 0.2f;
    /// <summary>
    /// 是否隐藏鼠标指针
    /// </summary>
    [Header("隐藏鼠标指针")]
    public bool isPointer;
    /// <summary>
    /// 射线长度
    /// </summary>
    [Header("射线长度")]
    [Range(0.1f, 2f)]
    public float rayLength = 0.5f;
    /// <summary>
    /// 反转Y轴
    /// </summary>
    private bool invertY = false;
    /// <summary>
    /// 支持碰撞的层
    /// </summary>
    [Header("支持碰撞")]
    public LayerMask layer;
    private RaycastHit _hit;
    /// <summary>
    /// 过去输入旋转方向
    /// </summary>
    /// <returns></returns>
    Vector3 GetInputTranslationDirection()
    {
        Vector3 direction = new Vector3();
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && CanMove(transform.forward))
        {
            direction += Vector3.forward;
        }
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && CanMove(-transform.right))
        {
            direction += Vector3.left;
        }
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && CanMove(-transform.forward))
        {
            direction += Vector3.back;
        }
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && CanMove(transform.right))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey(KeyCode.Q) && CanMove(transform.up))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.E) && CanMove(-transform.up))
        {
            direction += Vector3.down;
        }
        return direction;
    }

    private void Update()
    {
        if (isControl) return;

        #region 指针触碰UI停止
        /*if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }*/
        #endregion

        #region 实时更新位置
        targer.SetFromTransform(transform);
        interpolating.SetFromTransform(transform);
        #endregion

        Vector3 translation = Vector3.zero;

        #region ---------------------隐藏鼠标指针---------------------
        if (isPointer)
        {
            //锁鼠标
            if (Input.GetMouseButtonDown(1))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            // 解鼠标
            if (Input.GetMouseButtonUp(1))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        #endregion

        #region -------------------------旋转-------------------------
        // 处理旋转
        if (Input.GetMouseButton(1))
        {
            var mouseMovement = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y") * (invertY ? 1 : -1));
            var mouseSensitivityFactor = mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

            targer.yaw += mouseMovement.x * mouseSensitivityFactor;
            targer.pitch += mouseMovement.y * mouseSensitivityFactor;
        }
        #endregion

        #region -------------------------移动-------------------------
        //处理移动
        translation = GetInputTranslationDirection() * Time.deltaTime;
        translation *= Mathf.Pow(2.0f, speed);
        targer.Translate(translation);
        #endregion

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            interpolating.ChangeFieldView(-Input.mouseScrollDelta.y);
        }   
        var positionLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / positionLerpTime) * Time.deltaTime);
        var rotationLerpPct = 1f - Mathf.Exp((Mathf.Log(1f - 0.99f) / rotationLerpTime) * Time.deltaTime);
        interpolating.LerpTowards(targer, positionLerpPct, rotationLerpPct);
        interpolating.UpdateTransform(transform);
    }
    /// <summary>
    /// 重置摄像机
    /// </summary>
    public void ResetCamera()
    {
        targer.ReSet(transform);
        interpolating.ReSet(transform);
    }
    /// <summary>
    /// 相机距离检测与设定距离对比并返回是否运动的判断
    /// </summary>
    /// <param name="dir">本物体所朝的方向</param>
    /// <returns></returns>
    bool CanMove(Vector3 dir)
    {
        Ray ray = new Ray(transform.position, dir);
        if (Physics.Raycast(ray, out _hit, rayLength, layer))
        {
            return false;
        }
        return true;
    }
}