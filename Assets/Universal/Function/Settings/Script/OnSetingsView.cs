using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OnSetingsView : MonoBehaviour
{
    public GameObject setingsWindow;
    private Button setings;
    bool isPress;
    private void Start()
    {
        setings = this.gameObject.GetComponent<Button>();
        setings.onClick.AddListener(OnPress);
        setingsWindow.SetActive(false);
    }
    private void Update()
    {
        if ((Input.GetMouseButtonUp(0)) && setingsWindow.activeSelf.Equals(true) && !EventSystem.current.IsPointerOverGameObject())
        {
            setingsWindow.SetActive(false);
            isPress = false;
        }
    }
    private void OnPress()
    {
        isPress = !isPress;
        if (isPress)
        {
            setingsWindow.SetActive(isPress);
        }
        else
        {
            setingsWindow.SetActive(isPress);
        }       
    }
}
