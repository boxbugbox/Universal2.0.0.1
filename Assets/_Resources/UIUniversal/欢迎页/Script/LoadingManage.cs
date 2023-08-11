using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManage : MonoBehaviour
{
    public GameObject loadingObject;
    public Text loadingText;
    public void OnLoadingText(float progress)
    {
        loadingObject.SetActive(true);
        loadingText.text = progress.ToString() + "%";
    }
}
