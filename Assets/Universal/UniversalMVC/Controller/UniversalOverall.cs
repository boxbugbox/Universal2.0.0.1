using LitJson;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
namespace Universal
{
    public class UniversalOverall : MonoBehaviour
    {
        #region 单例
        public static UniversalOverall universalOverall;
        private void Awake()
        {
            universalOverall = this;
            DontDestroyOnLoad(this.gameObject);
        }
        #endregion        
        public delegate void DownloadCallback(AssetBundle ab);
        private DownloadCallback onDownloadCallback;
        [ReadOnly ,SerializeField] List<Question> questions = new List<Question>();
        public bool[] guides;
        [ReadOnly, SerializeField,PropertySpace(10)] private PatternType patternType;
        [ReadOnly] public string url;
        [ReadOnly, SerializeField] private float steps;
        public float Steps { get => steps; set => steps = value; }
        public List<Question> Questions { get => questions; set => questions = value; }
        public PatternType PatternType { get => patternType; set => patternType = value; }

        private void Start()
        {
            InSteps();
            InitOverall();
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            string effect_url = GetStreamingURL("StandaloneWindows/soundeffect.universal");
            StartDownload("音效", effect_url, delegate (AssetBundle effectCallback){
                UniversalAudioSourceControl.universalAudioSourceControl.effect = effectCallback.LoadAllAssets<AudioClip>();
            });
#elif UNITY_WEBGL           
            string effect_url = GetStreamingURL("WebGL/soundeffect.universal");
            StartDownload("音效", effect_url, delegate (AssetBundle effectCallback){
                UniversalAudioSourceControl.universalAudioSourceControl.effect = effectCallback.LoadAllAssets<AudioClip>();
            });        
#endif
        }
        /// <summary>
        /// 设置模式
        /// </summary>
        /// <param name="type"></param>
        public void SetPatternType(PatternType type)
        {
            patternType = type;
        }
        /// <summary>
        /// 设置指引状态
        /// </summary>
        public void SetGuide(int index,bool isStatus)
        {
            guides[index] = isStatus;
        }
        /// <summary>
        /// 获取指引状态
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool GetGuide(int index)
        {
            return guides[index];
        }
        /// <summary>
        /// 返回Streaming文件夹路径
        /// </summary>
        /// <param name="localpath"></param>
        /// <returns></returns>
        public string GetStreamingURL(string localpath)
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
        /// <summary>
        /// 销毁数据
        /// </summary>
        public void Delete()
        {
            Destroy(this.gameObject);
        }
        public void InSteps()
        {
            if (GetGuide(0)) steps = 0; else steps = 1;
        }
        /// <summary>
        /// 全局初始化
        /// </summary>
        private void InitOverall()
        {
            QualitySettings.SetQualityLevel(5, true);
            Debug.Log("恢复质量");
        }
        #region Load configuration file
        /// <summary>
        /// Read Config
        /// </summary>
        /// <param name="info"></param>
        private void ReadConfig(string info)
        {
            var list = JsonMapper.ToObject(info);
            if (list != null)
            {
                Debug.Log("Read fileConfig Success");
            }
            else
            {
                Debug.Log("Read fileConfig Error");
            }
        }
        /// <summary>
        /// Download ResourceConfig
        /// </summary>
        /// <returns></returns>
        IEnumerator DownloadResourceConfig()
        {
            using (UnityWebRequest webRequest = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST))
            {
                /*UploadHandler uploader = new UploadHandlerRaw(System.Text.Encoding.Default.GetBytes(_postData));
                webRequest.uploadHandler = uploader;
                webRequest.uploadHandler.contentType = "application/json";  //设置HTTP协议的请求头，默认的请求头HTTP服务器无法识别*/

                //这里需要创建新的对象用于存储请求并响应后返回的消息体，否则报空引用的错误
                DownloadHandler downloadHandler = new DownloadHandlerBuffer();
                webRequest.downloadHandler = downloadHandler;

                //Debug.Log(webRequest.uploadHandler.data.Length);

                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                if (webRequest.isNetworkError)
                {
                    Debug.LogError(webRequest.error + webRequest.isHttpError);
                }
                else
                {
                    //string reciveStr = System.Text.Encoding.UTF8.GetString(webRequest.downloadedBytes);
                    ReadConfig(webRequest.downloadHandler.text);
                    Debug.Log("Form upload complete And receive data :" + webRequest.downloadHandler.text);
                }
            }
        }
        #endregion

        #region Load AssetBundle file
        /// <summary>
        /// Start Download AssetBundle
        /// </summary>
        /// <param name="start_name"></param>
        /// <param name="start_url"></param>
        /// <param name="onDownload"></param>
        public void StartDownload(string start_name,string start_url, DownloadCallback onDownload)
        {
            Debug.Log(start_name + "---<color=#00EC35>开始下载...</color>");
            url = start_url;
            onDownloadCallback = onDownload;
            StartCoroutine(DownloadAssetBundlefile(start_name));
        }
        /// <summary>
        /// Load AssetBundle
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IEnumerator DownloadAssetBundlefile(string name)
        {
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
            request.SendWebRequest();
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log("当前的下载发生错误" + request.error);
                yield break;
            }
            else
            {
                while (!request.isDone)
                {
                    //Debug.Log("当前的下载进度为：" + request.downloadProgress);
                    yield return 0;
                }
                if (request.isDone)
                {
                    Debug.Log(name + "---下载完成：100%");
                    AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
                    if (onDownloadCallback != null) onDownloadCallback(ab);
                }
            }
        }
        #endregion

        #region Editor
        [OnInspectorGUI]
        private void UniversalInspectorGUI()
        {
            Version version = new Version();
            GUILayout.Space(15);
            GUISkin customSkin;
            customSkin = (GUISkin)Resources.Load("Editor\\Control");        
            GUILayout.Label("Universal Frame", customSkin.FindStyle("Header"));
            GUILayout.Label(version.VersionNumber, customSkin.FindStyle("Bottom"));
        }
        [PropertyOrder(-1),OnInspectorGUI]
        private void UniversalLogo()
        {         
            GUISkin customSkin;
            customSkin = (GUISkin)Resources.Load("Editor\\Control");
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Box(new GUIContent(""), customSkin.FindStyle("Logo"));
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
        }
        #endregion
    }
    public enum PatternType
    {
        Default,
        学习,
        考核
    }
}

