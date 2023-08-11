using UnityEngine;
using UnityEngine.EventSystems;

namespace Universal.Function {
    public class ClickButtonPress : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        /// <summary>
        /// 延迟时间
        /// </summary>
        private float delay = 0.2f;
        /// <summary>
        /// 按钮是否按下状态
        /// </summary>
        private bool isDown = false;
        /// <summary>
        /// 按钮最后一次是被按住状态时候的时间
        /// </summary>
        private float lastIsDownTime;
        /// <summary>
        /// 按下物体的名字
        /// </summary>
        private string objectName;
        private void Update()
        {
            // 如果按钮是被按下状态  
            if (isDown)
            {
                if (this.gameObject.name.Equals(objectName))
                {
                    OnShortPress();
                }
                // 当前时间 -  按钮最后一次被按下的时间 > 延迟时间0.2秒  
                if (Time.time - lastIsDownTime > delay)
                {
                    // 触发长按方法 
                    OnLongPress();
                    // 记录按钮最后一次被按下的时间
                    lastIsDownTime = Time.time;
                }
            }
        }
        public void OnPointerDown(PointerEventData eventData)
        {
            isDown = true;
            lastIsDownTime = Time.time;
        }
        public void OnPointerExit(PointerEventData eventData)
        {
            isDown = false;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            isDown = false;
        }
        /// <summary>
        /// 长按方法
        /// </summary>
        protected virtual void OnLongPress() { }
        /// <summary>
        /// 短按方法
        /// </summary>
        protected virtual void OnShortPress() { }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="thisDelay">延迟时间</param>
        /// <param name="thisObjectName">物体名字</param>
        protected void Init(float thisDelay,string thisObjectName) 
        {
            delay = thisDelay;
            objectName = thisObjectName;
        }
    }
}

