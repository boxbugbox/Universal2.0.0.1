using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Video;
using Universal.Function;
/**
* Data:2021-10-28
* guqing
* 视频加载
*/
public class VideoLoading : MonoBehaviour
{
    public string videoname;
    private VideoPlayer vp;
    private void Start()
    {
        vp = this.GetComponent<VideoPlayer>();
        try
        {
            vp.url= Path.Combine(Application.streamingAssetsPath, videoname + ".mp4");
           /* Debug.Log(Output.print(vp.url));*/
        }
        catch (System.Exception e)
        {
            Debug.Log(Output.print("加载失败..." + e.Message));
        }        
    }
}
