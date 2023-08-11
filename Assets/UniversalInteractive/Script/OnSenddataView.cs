using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Function;

public class OnSenddataView : ClickEvents
{
    protected override void OnPress()
    {
        InteractiveManage interactiveManage = GameObject.FindObjectOfType<InteractiveManage>();
        if (interactiveManage != null)
        {
            interactiveManage.Send();
        }
        else
        {
            Debug.Log("数据发送失败");
        }      
    }
}
