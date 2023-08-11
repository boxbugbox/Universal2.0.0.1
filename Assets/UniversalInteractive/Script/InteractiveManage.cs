using LitJson;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Networking;

public class InteractiveManage : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_STANDALONE_WIN

#elif UNITY_WEBGL
        [DllImport("__Internal")]
        private static extern void CreateWord(string str);

        [DllImport("__Internal")]
        private static extern void SendData(string str);

        [DllImport("__Internal")]
        private static extern void GetUrldata();
#endif

    public delegate void ReadCallback(string str);
    [SerializeField,LabelText("步骤分数")]List<Stepsdata> stepsdatas = new List<Stepsdata>();
    [BoxGroup("初始数据")] public string _username, _title;
    [BoxGroup("初始数据")] public int _appid;

    [Title("")] public AssessmentManage assessmentManage;


    private string _ticket, _token, _userAccount, _userName;
    private DateTime _startTime;
    private DateTime _endTime;
    private bool isisFinish;

    public bool IsisFinish { get => isisFinish; set => isisFinish = value; }

    private void Start()
    {
        //----------------------------添加开始时间------------------------
        AddStartTime(DateTime.Now);
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
     
        
#elif UNITY_WEBGL
        
#endif
    }   
    private void OnStart()
    {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
      
#elif UNITY_WEBGL
       
#endif
    }
    /// <summary>
    /// 获取步骤得分
    /// </summary>
    public void GetStepData()
    {
        stepsdatas.Clear();
        for (int i = 0; i < assessmentManage.m_Titles.Length; i++)
        {
            Stepsdata stepsdata = new Stepsdata();
            stepsdata.stepCode = (i + 1);
            stepsdata.stepName = assessmentManage.m_Titles[i].text;
            float sc = float.Parse(assessmentManage.m_Scores[i].text);
            stepsdata.score = (int)sc;
            if (i == 4)
            {
                stepsdata.experctTime = 120;
                stepsdata.maxScore = 8;
            }
            else
            {
                stepsdata.experctTime = 60;
                stepsdata.maxScore = 3;
            }
            stepsdatas.Add(stepsdata);
        }
    }
    /// <summary>
    /// 添加开始时间
    /// </summary>
    /// <param name="dateTime"></param>
    public void AddStartTime(DateTime dateTime)
    {
        _startTime = dateTime;
    }
    /// <summary>
    /// 添加结束时间
    /// </summary>
    /// <param name="dateTime"></param>
    public void AddEndTime(DateTime dateTime)
    {
        _endTime = dateTime;
    }
    /// <summary>
    /// 获取是否完成
    /// </summary>
    /// <returns></returns>
    public int GetIsFinish()
    {
        if (isisFinish) return 1;
        else return 0;
    }
    /// <summary>
    /// 获取分数
    /// </summary>
    /// <returns></returns>
    public int GetScore()
    {
        float ts = float.Parse(assessmentManage._totalScore.text);
        return (int)ts;    
    }
    public string AddData()
    {
        JsonData data = new JsonData();
        //用户名
        data["username"] = _username;
        //实验名称
        data["title"] = _title;
        //实验结果
        data["status"] = GetIsFinish();

        data["score"] = GetScore();

        data["startTime"] = GetTimeStamp(_startTime);

        data["endTime"] = GetTimeStamp(_endTime);

        data["timeUsed"] = GetTimeSpan(_startTime, _endTime);

        data["appid"] = _appid;

        data["originId"] = GetIsFinish();

        data["steps"] = new JsonData();

        data["steps"].SetJsonType(JsonType.Array);

        for (int i = 0; i < stepsdatas.Count; i++)
        {
            JsonData data2 = new JsonData();

            data2["seq"] = stepsdatas[i].stepCode;

            data2["title"] = stepsdatas[i].stepName;

            data2["startTime"] = GetTimeStamp(_startTime);

            data2["endTime"] = GetTimeStamp(_endTime);

            data2["timeUsed"] = GetTimeSpan(_startTime, _endTime);

            data2["experctTime"] = stepsdatas[i].experctTime;

            data2["maxScore"] = stepsdatas[i].maxScore;

            data2["score"] = stepsdatas[i].score;

            data2["repatCount"] = stepsdatas[i].repatCount;

            data2["evaluation"] = stepsdatas[i].evaluation;

            data2["scoringModel"] = stepsdatas[i].scoringModel;

            data["steps"].Add(data2);
        }
        string datas = data.ToJson();

        Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
        datas = reg.Replace(datas, delegate (Match m) { return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString(); });
        return datas;
    }  
    /// <summary>
    /// 获取时间戳
    /// </summary>
    /// <param name="dateTime">时间</param>
    /// <returns></returns>
    private long GetTimeStamp(DateTime dateTime)
    {
        TimeSpan time = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0);
        long t = time.Ticks / 10000;
        return t;
    }
    /// <summary>
    /// 获取时间间隔
    /// </summary>
    /// <param name="end">结束时间</param>
    /// <param name="start">开始时间</param>
    /// <returns></returns>
    private int GetTimeSpan(DateTime end, DateTime start)
    {
        TimeSpan ts = start - end;
        //Debug.Log(ts.Hours + "   " + ts.Minutes + "   " + ts.Seconds);
        return (ts.Hours * 3600 + ts.Minutes * 60 + ts.Seconds);
    }
    /// <summary>
    /// 32位md5加密
    /// </summary>
    /// <param name="s"></param>
    /// <param name="_input_charset">utf-8</param>
    /// <returns></returns>
    private string GetMD5_32(string s, string _input_charset = "utf-8")
    {
        MD5 md5 = MD5.Create();
        byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < t.Length; i++)
        {
            stringBuilder.Append(t[i].ToString("x").PadLeft(2, '0'));
        }
        return stringBuilder.ToString();
    }
    /// <summary>
    /// 返回Streaming文件夹路径
    /// </summary>
    /// <param name="localpath"></param>
    /// <returns></returns>
    private string GetStreamingURL(string localpath)
    {
        string url = "";
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            url = Application.streamingAssetsPath + "/" + localpath;
        }
        else if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            url = "file://" + Application.streamingAssetsPath + "/" + localpath;
        }
        else if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            url = "file://" + Application.streamingAssetsPath + "/" + localpath;
        }
        return url;
    }
    #region-------------------------
    /// <summary>
    /// 发送数据
    /// </summary>
    [Button]
    public void Send()
    {
        //----------------------------添加结束时间------------------------
        AddEndTime(DateTime.Now);
        //----------------------------设置为完成------------------------
        isisFinish = true;
        //----------------------------获取步骤分------------------------
        GetStepData();
        //----------------------------获取数据------------------------
        string jsoninfo = AddData();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        Debug.Log(jsoninfo);
#elif UNITY_WEBGL
        try
        {
            SendData(jsoninfo);
        }
        catch (Exception e)
        {
             print(e.Message);
        }    
#endif
    }
    /// <summary>
    /// 前台获取_ticket&&_token
    /// </summary>
    /// <param name="str"></param>
    public void Getdatass(string str)
    {
        Debug.Log(str);
        var num = str.IndexOf("practiceid=");
        str = str.Substring(num + 1);
        num = str.IndexOf("?");
        str = str.Substring(num + 1);
        var arr = str.Split('&');
        for (int i = 0; i < arr.Length; i++)
        {
            num = arr[i].IndexOf("=");
            if (num > 0)
            {
                if (i == 0)
                {
                    _ticket = arr[0].Substring(num + 1);
                }
                if (i == 1)
                {
                    _token = arr[1].Substring(num + 1);
                }
            }
        }
    }
    IEnumerator UnityHttpsPost(string _url, string jsonData)
    {
        UnityWebRequest uwr = new UnityWebRequest(_url, UnityWebRequest.kHttpVerbPOST);
        DownloadHandler downloadHandler = new DownloadHandlerBuffer();
        uwr.downloadHandler = downloadHandler;

        uwr.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        uwr.SetRequestHeader("Authorization", "Bearer " + _token);

        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
        yield return uwr.SendWebRequest();

        if (uwr.isHttpError || uwr.isNetworkError)
        {
            Debug.LogError("Login Error: " + uwr.error);
        }
        else
        {
            Debug.Log("Form upload complete And receive data :" + uwr.downloadHandler.text);
        }
    }
    IEnumerator UnityHttpsGet(string _url)
    {
        UnityWebRequest request = UnityWebRequest.Get(_url);
        request.SetRequestHeader("Content-Type", "application/json;charset=utf-8");
        request.SetRequestHeader("Authorization", "Bearer " + _token);

        yield return request.SendWebRequest();
        if (request.isHttpError || request.isNetworkError)
        {
            Debug.LogError("Login Error: " + request.error);
        }
        else
        {
            string receiveContent = request.downloadHandler.text;
            var data = JsonMapper.ToObject(receiveContent);
            _userAccount = data["rows"]["account"].ToString();
            _userName = data["rows"]["username"].ToString();
        }
    }
    #endregion
}
[Serializable]
public class Stepsdata
{
    [LabelText("步骤号")]
    public int stepCode;
    [LabelText("步骤名称")]
    public string stepName;
    [ReadOnly, LabelText("开始时间")]
    public long satrtTime;
    [ReadOnly, LabelText("结束时间")]
    public long endTime;
    [ReadOnly, LabelText("操作时长")]
    public int timeUsed;
    [LabelText("合理用时")]
    public int experctTime;
    [LabelText("步骤满分")]
    public int maxScore;
    [ReadOnly, LabelText("步骤得分")]
    public int score;
    [LabelText("操作次数")]
    public int repatCount;
    [LabelText("点评")]
    public string evaluation;
    [LabelText("赋分模型")]
    public string scoringModel;
    [LabelText("备注")]
    public string remarks;
}