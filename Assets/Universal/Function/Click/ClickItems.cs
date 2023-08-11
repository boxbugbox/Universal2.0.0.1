using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Universal;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace Universal.Function
{
    [RequireComponent(typeof(Button))]
    public class ClickItems : MonoBehaviour
    {
        private Button confirm;       
        public ClickItemsType type;
        [HideIf("type", ClickItemsType.提交)] public GameObject _object;
        public Transform _parent;
        [ShowIf("type", ClickItemsType.提交)] public GameObject error;      
        [Space(15)] public UnityEvent confirmEvent;
        [Title("")]
        [ShowIf("type", ClickItemsType.提交)] public int m_count;
        int _Error;
        string _Name;
        protected Transform _ObjectpoolTF;
        private void Start()
        {
            switch (type)
            {
                case ClickItemsType.Default:
                    break;
                case ClickItemsType.选择物品:
                    _ObjectpoolTF = _object.transform.parent;
                    break;
                case ClickItemsType.撤回选择:
                    break;
                case ClickItemsType.提交:
                    break;
                default:
                    break;
            }
            confirm = this.GetComponent<Button>();
            confirm.onClick.AddListener(OnPress);           
            OnStart();
            
        }
        private void OnPress()
        {
            switch (type)
            {
                case ClickItemsType.Default:
                    break;
                case ClickItemsType.选择物品:
                    //if (_parent.childCount >= m_count) return;
                    _object.transform.SetParent(_parent);
                    _object.SetActive(true);
                    this.transform.GetChild(0).gameObject.SetActive(false);
                    break;
                case ClickItemsType.撤回选择:
                    _object.transform.GetChild(0).gameObject.SetActive(true);
                    this.gameObject.SetActive(false);
                    this.transform.SetParent(_parent);
                    break;
                case ClickItemsType.提交:
                    OnSubmit();
                    break;
                default:
                    break;
            }
        }
        protected virtual void OnSubmit()
        {
            if (Verification())
            {
                _Error = 0;
                confirmEvent.Invoke();
                //Debug.Log(Output.print("正确"));
            }
            else
            {
                if (_parent.childCount.Equals(0))
                {
                    error.GetComponent<ErrorManager>().AddErrorEvent(() => ErrorCallback());
                    //Debug.Log(Output.print("请选择物品"));
                }
                else
                {
                    error.GetComponent<ErrorManager>().AddErrorEvent(() => ErrorCallback());
                    //Debug.Log(Output.print("选择错误"));
                }
                _Error++;
                if (_Error >= 3)
                {
                    //Debug.Log(Output.print("错误三次后"));
                }
            }
        }
        private bool Verification()
        {
            if (_parent.childCount == m_count)
            {
                for (int i = 0; i < _parent.childCount; i++)
                {
                    if (_parent.GetChild(i).name.Equals(_Name))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }
        
        protected virtual void ErrorCallback() { }
        protected virtual void OnStart() { }
    }
}

public enum ClickItemsType
{
    Default,
    选择物品,
    撤回选择,
    提交
}
