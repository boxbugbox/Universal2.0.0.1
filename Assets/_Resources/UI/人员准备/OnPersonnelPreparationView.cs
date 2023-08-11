using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPersonnelPreparationView : MonoBehaviour
{
    public GameObject[] icon;
    public GameObject[] target;
    private void Start()
    {
        
    }
    private void OnDisable()
    {
        foreach (var item in target)
        {
            item.gameObject.SetActive(true);
        }
        foreach (var item in icon)
        {
            item.gameObject.SetActive(true);
        }
    }
}
