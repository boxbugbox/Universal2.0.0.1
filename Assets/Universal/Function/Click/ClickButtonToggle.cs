using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Universal.Function
{
    public class ClickButtonToggle : MonoBehaviour
    {
        private Button button;
        bool isPress;
        protected virtual void Start()
        {
            button = this.gameObject.GetComponent<Button>();
            button.onClick.AddListener(OnPress);
        }
        private void OnPress()
        {
            if (isPress)
            {
                OnOpen();
            }
            else
            {
                OnClose();
            }
            isPress = !isPress;
        }
        protected virtual void OnOpen() { }
        protected virtual void OnClose() { }
    }
}

