using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItembarTools : MonoBehaviour
{
    public GameObject _Glint;
    public GameObject _Icon;
    public Text _Lable;

    [Button]
    public void UpdateInfor()
    {
        _Glint.name = this.name + "_Glint";
        _Icon.name = this.name;
        _Lable.text = this.name;
    }
}
