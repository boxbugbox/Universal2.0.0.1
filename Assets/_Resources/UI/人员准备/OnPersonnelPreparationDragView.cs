using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPersonnelPreparationDragView : OnDragView
{
    protected override void OverwriteOnTriggerEnter2D(Collider2D collision)
    {
        /*if (this.name.Equals("口罩") && collision.name.Equals("口罩"))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Universal.UniversalModelControl.universalModelControl.SetModelActive("口罩", true);
        }
        else if (this.name.Equals("头套") && collision.name.Equals("头套"))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Universal.UniversalModelControl.universalModelControl.SetModelActive(new string[] { "手套2", "头套" }, true);
        }
        else*/ if (this.name.Equals("无菌手术衣") && collision.name.Equals("无菌手术衣"))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Universal.UniversalModelControl.universalModelControl.SetModelActive(new string[] { "无菌手术衣", "无菌服" }, true);
        }
        else if (this.name.Equals("手套") && collision.name.Equals("手套"))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Universal.UniversalModelControl.universalModelControl.SetModelActive(new string[] { "手套" }, true);
        }
        if (Verification())
        {
            onDragEvent.Invoke();
        }
    }
    private bool Verification()
    {
        if (Universal.UniversalModelControl.universalModelControl.GetModelIsActive(new string[] { "无菌手术衣", "手套", "无菌服"}))
        {
            return true;
        }
        return false;
    }
}
