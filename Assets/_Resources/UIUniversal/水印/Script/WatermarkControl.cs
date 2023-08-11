using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WatermarkControl : MonoBehaviour
{
    public Sprite[] watermarks;

    Image watermarksImage;
    CanvasGroup watermarksCanvasGroup;
    private void Start()
    {
        Console.sendCommand += Command;
        watermarksImage = this.GetComponent<Image>();
        watermarksCanvasGroup = this.GetComponent<CanvasGroup>();
        DefaultStart();
    }
    private void Update()
    {
        //当回车键按下时
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!CurrentScene(0)) return;
            //Y+K+J
            if (Input.GetKey(KeyCode.Y) && Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.J))
            {                
                watermarksImage.sprite = watermarks[0];
                watermarksCanvasGroup.alpha = 1;
            }
            else if (Input.GetKey(KeyCode.L) && Input.GetKey(KeyCode.H))
            {              
                watermarksImage.sprite = watermarks[1];
                watermarksCanvasGroup.alpha = 1;
            }
            else if ((Input.GetKey(KeyCode.RightControl) || Input.GetKey(KeyCode.LeftControl))&& Input.GetKey(KeyCode.Delete))
            {
                watermarksCanvasGroup.alpha = 0;
            }
        }       
    }
    private void OnDestroy() {
        Console.sendCommand -= Command;
    }
    private void Command(string context) {
        string[] strs = context.Split('_');
        switch (strs[0]){
            case "control":
                switch (strs[1]) {
                    case "ykj":
                        watermarksImage.sprite = watermarks[0];
                        watermarksCanvasGroup.alpha = 1;
                        break;
                    case "lhzn":
                        watermarksImage.sprite = watermarks[1];
                        watermarksCanvasGroup.alpha = 1;
                        break;
                    case "off":
                        watermarksCanvasGroup.alpha = 0;
                        break;
                }
                break;
        }
    }
    /// <summary>
    /// 默认元空间水印
    /// </summary>
    private void DefaultStart()
    {
        watermarksImage.sprite = watermarks[0];
        watermarksCanvasGroup.alpha = 1;
    }
    private bool CurrentScene(int sceneIndex)
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index.Equals(sceneIndex))
        {
            return true;
        }
        return false;
    }
}