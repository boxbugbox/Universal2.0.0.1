using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnConnectDragView : OnDragView
{
    protected override void OverwriteOnTriggerEnter2D(Collider2D collision)
    {
        if (this.name.Equals("静脉输液") && collision.name.Equals("静脉输液"))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Universal.UniversalModelControl.universalModelControl.SetModelActive("静脉输液", true);           
        }
        else if(this.name.Equals("心电监护") && collision.name.Equals("心电监护"))
        {
            this.gameObject.SetActive(false);
            collision.gameObject.SetActive(false);
            Universal.UniversalModelControl.universalModelControl.SetModelActive("心电监护", true);
        }
        if (Verification())
        {
            NextManage.nextManage.SetPrompt(true);
        }
    }
    private bool Verification()
    {
        if (Universal.UniversalModelControl.universalModelControl.GetModelIsActive(new string[] { "静脉输液", "心电监护" }))
        {
            return true;
        }
        return false;
    }
}
