using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UniversalTimer : MonoBehaviour
{
    
    /// <summary>
    /// 小时
    /// </summary>
    private int hour;
    /// <summary>
    /// 分钟
    /// </summary>
    private int minute;
    /// <summary>
    /// 秒
    /// </summary>
    private int second;
    /// <summary>
    /// 毫秒
    /// </summary>
    private int millisecond;
    private float timeSpend = 0;
    private Text recealText;
    private bool pause;

    public bool Pause { get => pause; set => pause = value; }

    private void Start()
    {
        pause = true;
        recealText = this.transform.GetComponent<Text>();
    }
    private void Update()
    {
        if (pause)
        {
            timeSpend += Time.deltaTime;
            hour = (int)timeSpend / 3600;
            minute = ((int)timeSpend - hour * 3600) / 60;
            second = (int)timeSpend - hour * 3600 - minute * 60;
            millisecond = (int)((timeSpend - (int)timeSpend) * 1000);
            //recealText.text = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D3}", hour, minute, second, millisecond);
            recealText.text = string.Format("{0:D2}:{1:D2}", minute, second);
        }
    }
}
