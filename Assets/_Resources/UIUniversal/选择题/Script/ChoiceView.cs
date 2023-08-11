using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceView : OnChoiceView
{
    protected override void ErrorEvent()
    {
        error.GetComponent<ErrorManager>().AddErrorEvent(() => InError());
    }
    private void InError()
    {
        foreach (var item in choices)
        {
            item.Choice.isOn = item.isRight;
        }
    }
}
