using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Universal.Function
{
    public class ClickEvents : MonoBehaviour
    {
        protected Button _press;
        private void Awake()
        {
            _press = this.GetComponent<Button>();
            _press.onClick.AddListener(OnPress);
        }
        protected virtual void OnPress() { }
    }
}