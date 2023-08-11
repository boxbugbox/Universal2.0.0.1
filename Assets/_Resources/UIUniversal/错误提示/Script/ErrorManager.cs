using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorManager : MonoBehaviour
{
    public delegate void ErrorHandler();
    public Button restart;
    public Button show;
    private ErrorHandler onErrorHandler;
    private ErrorHandler onRestartHandler;
    
    private void Start()
    {
        restart.onClick.AddListener(() => { SetErrorActive(false); });
    }
    private void OnShow()
    {
        onErrorHandler?.Invoke();
        SetErrorActive(false);
    }
    private void OnRestart()
    {
        onRestartHandler?.Invoke();
        SetErrorActive(false);
    }
    private void SetErrorActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
    public void AddErrorEvent(ErrorHandler addHandler)
    {
        this.gameObject.SetActive(true);
        onErrorHandler = addHandler;
        show.onClick.AddListener(OnShow);
    }
    public void AddRestart(ErrorHandler addHandler)
    {
        onRestartHandler = addHandler;
        restart.onClick.AddListener(OnRestart);
    }
}
