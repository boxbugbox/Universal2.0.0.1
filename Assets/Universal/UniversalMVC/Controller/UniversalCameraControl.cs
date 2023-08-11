using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Text;
using System;

[System.Serializable]
public class MachinePositionT
{
    [LabelWidth(30), LabelText("名字"), HorizontalGroup("Horizontal", marginRight: 30, width: 0.3f)] public string _Name;
    [LabelWidth(30), LabelText("位置"), HorizontalGroup("Horizontal")] public Transform _Position;
}
namespace Universal
{
    public class UniversalCameraControl : MonoBehaviour
    {
        #region 单例
        public static UniversalCameraControl universalControl;
        private void Awake()
        {
            universalControl = this;
        }
        #endregion
        
        [InlineButton("GetMainCamera", "Find")] public Camera mainCamera;
        [SerializeField,ListDrawerSettings(ShowPaging = false)] List<MachinePositionT> machinePositions = new List<MachinePositionT>();

        string cameraLens = "";
        /// <summary>
        /// 设置机位
        /// </summary>
        /// <param name="machineName"></param>
        public void SetCameraMachinePosition(string machineName)
        {
            for (int i = 0; i < machinePositions.Count; i++)
            {
                if (machineName.Equals(machinePositions[i]._Name))
                {
                    Reset(machinePositions[i]._Position);
                }
            }
        }
        /// <summary>
        /// 设置机位
        /// </summary>
        /// <param name="machineName">机位名称</param>
        /// <param name="time">时间</param>
        public void SetCameraMachinePosition(string machineName, float time)
        {
            for (int i = 0; i < machinePositions.Count; i++)
            {
                if (machineName.Equals(machinePositions[i]._Name))
                {
                    mainCamera.transform.SetParent(machinePositions[i]._Position);
                    mainCamera.transform.DOLocalMove(Vector3.zero, time);
                    mainCamera.transform.DOLocalRotateQuaternion(Quaternion.Euler(0, 0, 0), time);
                }
            }
        }
        /// <summary>
        /// 设置机位
        /// </summary>
        /// <param name="machineName">机位名称</param>
        /// <param name="time">时间</param>
        /// <param name="onEndCallback">委托事件</param>
        public void SetCameraMachinePosition(string machineName, float time, TweenCallback onEndCallback)
        {
            for (int i = 0; i < machinePositions.Count; i++)
            {
                if (machineName.Equals(machinePositions[i]._Name))
                {
                    mainCamera.transform.SetParent(this.transform);
                    mainCamera.transform.DOLocalMove(machinePositions[i]._Position.localPosition, time);
                    mainCamera.transform.DOLocalRotateQuaternion(machinePositions[i]._Position.localRotation, time).OnComplete(onEndCallback);
                }
            }
        }
        /// <summary>
        /// 设置相机旋转
        /// </summary>
        /// <param name="start"></param>
        /// <param name="time"></param>
        public void SetCameraMachineRotation(Vector3 start, float time)
        {
            mainCamera.transform.DOLocalRotate(start, time);
        }
        /// <summary>
        /// 设置摄像机移动
        /// </summary>
        /// <param name="isStop"></param>
        public void SetpCamraMove(bool isStop)
        {
            mainCamera.GetComponent<CameraMove>().isControl = isStop;
        }
        /// <summary>
        /// 重置视角
        /// </summary>
        /// <param name="CameraMachinePosition">机位</param>
        private void Reset(Transform CameraMachinePosition)
        {
            mainCamera.fieldOfView = 60f;
            mainCamera.transform.SetParent(CameraMachinePosition);
            mainCamera.transform.localPosition = new Vector3();
            mainCamera.transform.localRotation = new Quaternion();
        }   
        /// <summary>
        /// 获取主摄像机
        /// </summary>
        private void GetMainCamera()
        {
            mainCamera = Camera.main;
        }
        /// <summary>
        /// 获取相机数据
        /// </summary>
        private void GetCameraData()
        {
            var tfname = mainCamera.transform.parent.name;
            if (tfname != this.transform.name)
            {
                mainCamera.transform.SetParent(this.transform);
                var px = mainCamera.transform.localPosition.x;
                var py = mainCamera.transform.localPosition.y;
                var pz = mainCamera.transform.localPosition.z;
                var rx = mainCamera.transform.eulerAngles.x;
                var ry = mainCamera.transform.eulerAngles.y;
                var rz = mainCamera.transform.eulerAngles.z;
                Debug.Log(tfname + "|" + px + "," + py + "," + pz + "|" + rx + "," + ry + "," + rz);
                cameraLens += tfname + "|" + px + "," + py + "," + pz + "|" + rx + "," + ry + "," + rz + "\n";
            }
        }
        /// <summary>
        /// 保存相机数据
        /// </summary>
        private void SaveCameraData()
        {
            var name = "cameralens" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss");
#if UNITY_EDITOR
            Save_txt(name);
#elif UNITY_WEBGL && false
                File file = new File()
                {
                    fileInfo = new FileInfo()
                    {
                        fullName = name + ".txt"
                    },
                    data = System.Text.Encoding.UTF8.GetBytes(cameraLens)
                };
                FrostweepGames.Plugins.WebGLFileBrowser.WebGLFileBrowser.SaveFile(file);
#endif
        }
        /// <summary>
        /// 清除相机数据
        /// </summary>
        private void ClearCameraData()
        {
            cameraLens = "";
        }
        public void Save_txt(string fileName)
        {
            //string path = Application.streamingAssetsPath + "/" + fileName + ".txt";//DateTime.Now.ToString("yyyy-MM-dd-HHmmss")
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/" + fileName + ".txt"; ;
            Debug.Log(path);
            if (System.IO.File.Exists(path))//System.IO.Directory.CreateDirectory(path);//创建文件夹
            {               
                Debug.Log("文件不存在，即将创建。");
            }
            else
            {
                Debug.Log("文件存在，即将覆盖。");
            }
            //System.IO.FileMode.Create 文本不存在创建文本 反之覆盖
            System.IO.FileStream fileStream = new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(fileStream, Encoding.UTF8);
            sw.WriteLine(cameraLens);
            sw.Close();
            fileStream.Close();
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.Refresh();//刷新Unity的资产目录
#endif
        }
        /// <summary>
        /// 设置添加相机数据
        /// </summary>
        private void SetCameraLens()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "/Config.txt";
            Debug.Log(path);
            foreach (string str in System.IO.File.ReadAllLines(path, Encoding.Default))
            {
                var modeData = str.Split('|');
                if (modeData.Length > 0)
                {
                    for (int i = 0; i < machinePositions.Count; i++)
                    {
                        if (modeData[0].Equals(machinePositions[i]._Position.name))
                        {
                            machinePositions[i]._Position.localPosition = UniversalTools.GetVec3ByString(modeData[1]);
                            machinePositions[i]._Position.localRotation = Quaternion.Euler(UniversalTools.GetVec3ByString(modeData[2]));
                            Debug.Log("成功");
                        }
                    }                   
                }
            }
        }
        [Button("Add Camera Machineposition")]
        private void AddCameraMachineposition()
        {
            MachinePositionT mp = new MachinePositionT();
            GameObject go = new GameObject();            
            if (this.transform.childCount < 1)
            {
                go.name = "Default";
            }
            else
            {
                go.name = "Machineposition_" + (this.transform.childCount);
            }
            go.transform.parent = this.transform;
            mp._Name = go.name;
            mp._Position = go.transform;
            machinePositions.Add(mp);
        }
        [OnInspectorGUI, PropertyOrder(1)]
        private void UniversalInspectorGUI()
        {
            Version version = new Version();
            GUILayout.Space(15);
            GUISkin customSkin;
            customSkin = (GUISkin)Resources.Load("Editor\\Control");
            GUILayout.Label("Universal Frame", customSkin.FindStyle("Header"));
            GUILayout.Label(version.VersionNumber, customSkin.FindStyle("Bottom"));
        }
    }  
}

