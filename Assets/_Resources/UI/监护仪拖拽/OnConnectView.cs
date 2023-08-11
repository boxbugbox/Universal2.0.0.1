using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnConnectView : MonoBehaviour
{
    public GameObject[] uiSpirit;

    private void OnDisable()
    {
        foreach (GameObject go in uiSpirit)
        {
            go.SetActive(true);
        }
    }
}
