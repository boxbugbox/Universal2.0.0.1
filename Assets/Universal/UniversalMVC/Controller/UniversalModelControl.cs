using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Reflection;
using System;

[System.Serializable]
public class ModelT
{
    [LabelWidth(30), LabelText("名字"), HorizontalGroup("Horizontal", width: 0.01f)] public bool _isShow;
    [HideLabel, HorizontalGroup("Horizontal", width: 0.2f)] public string _Name;
    [LabelWidth(30), LabelText("模型"), HorizontalGroup("Horizontal")] public GameObject _Model;
}

[System.Serializable]
public class ModelObjectPosition
{
    [LabelWidth(30), LabelText("名字"), HorizontalGroup("Horizontal", width: 0.01f)] public string _Name;
    [LabelWidth(30), LabelText("模型"), HorizontalGroup("Horizontal")] public Transform _Position;
}

[System.Serializable]
public class ModelTransformT
{
    [LabelWidth(30), LabelText("位置"), HorizontalGroup("Horizontal")] public Vector3[] _positions;
    [LabelWidth(30), LabelText("旋转"), HorizontalGroup("Horizontal")] public Vector3[] _rotations;
}
namespace Universal
{
    public class UniversalModelControl : MonoBehaviour
    {
        #region 单例
        public static UniversalModelControl universalModelControl;
        private void Awake()
        {
            universalModelControl = this;
        }
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
        #endregion
        #endregion
        public List<ModelT> models = new List<ModelT>();
        public List<ModelObjectPosition> position = new List<ModelObjectPosition>();
        public List<ModelTransformT> modelTransforms = new List<ModelTransformT>();
        /// <summary>
        /// 设置模型状态
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="isActive"></param>
        public void SetModelActive(string modelName, bool isActive)
        {
            foreach (var item in models)
            {
                if (modelName.Equals(item._Name))
                {
                    item._Model.SetActive(isActive);
                }
            }
        }
        /// <summary>
        /// 设置模型状态
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="isActive"></param>
        public void SetModelActive(string[] modelName, bool isActive)
        {
            for (int i = 0; i < modelName.Length; i++)
            {
                foreach (var item in models)
                {
                    if (modelName[i].Equals(item._Name))
                    {
                        item._Model.SetActive(isActive);
                    }
                }
            }
        }
        /// <summary>
        /// 设置模型位置
        /// </summary>
        /// <param name="modelName">模型名字</param>
        /// <param name="index">位置索引</param>
        public void SetModelPosition(string modelName, int index)
        {
            GameObject go = GetModels(modelName);
            go.transform.localPosition = position[index]._Position.localPosition;
            go.transform.localRotation = position[index]._Position.localRotation;
        }
        /// <summary>
        /// 设置模型位置
        /// </summary>
        /// <param name="modelName">模型名字</param>
        /// <param name="indexName">位置名字</param>
        public void SetModelPosition(string modelName, string indexName)
        {
            GameObject go = GetModels(modelName);
            foreach (var item in position)
            {
                if (indexName.Equals(item._Name))
                {
                    go.transform.localPosition = item._Position.localPosition;
                    go.transform.localRotation = item._Position.localRotation;
                }
            }
        }      
        /// <summary>
        /// 设置模型位置
        /// </summary>
        /// <param name="modelName"></param>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        public void SetModelPosition(string modelName,Vector3 pos,Vector3 rot)
        {
            foreach (var item in models)
            {
                if (modelName.Equals(item._Name))
                {
                    item._Model.transform.localPosition = pos;
                    item._Model.transform.localRotation = Quaternion.Euler(rot);
                }
            }
        }
        /// <summary>
        /// 设置模型位置
        /// </summary>
        /// <param name="modelName">模型名称</param>
        /// <param name="indexArray">父级数组索引</param>
        /// <param name="indexPosition">子级数组位置索引</param>
        /// <param name="indexRotation">子级数组旋转索引 默认-1不使用</param>
        public void SetModeTransforms(string modelName, int indexArray, int indexPosition, int indexRotation = -1)
        {
            GameObject go = GetModels(modelName);
            go.transform.localPosition = modelTransforms[indexArray]._positions[indexPosition];
            if (indexRotation != -1)
            {
                go.transform.localRotation = Quaternion.Euler(modelTransforms[indexArray]._rotations[indexRotation]);
            }
        }
        /// <summary>
        /// 获取模型状态
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public bool GetModelIsActive(string[] modelName)
        {
            int ActiveCount = 0;
            for (int i = 0; i < modelName.Length; i++)
            {
                foreach (var item in models)
                {
                    if (modelName[i].Equals(item._Name))
                    {
                        if (item._Model.activeSelf == true)
                        {
                            ActiveCount++;
                        }
                        if (ActiveCount.Equals(modelName.Length))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// 模型移动
        /// </summary>
        /// <param name="modelName">模型名称</param>
        /// <param name="satrtpos">起点</param>
        /// <param name="endpos">终点</param>
        /// <param name="time">时间</param>
        public void ModelMove(string modelName, Vector3 satrtpos, Vector3 endpos, float time)
        {
            foreach (var item in models)
            {
                if (modelName.Equals(item._Name))
                {
                    item._Model.transform.localPosition = satrtpos;
                    item._Model.transform.DOLocalMove(endpos, time);
                }
            }
        }
        /// <summary>
        /// 获取ModeTransforms数据
        /// </summary>
        /// <param name="indexArray">父级数组索引</param>
        /// <returns></returns>
        public Vector3[] GetModeTransforms(int indexArray)
        {
            return modelTransforms[indexArray]._positions;
        }
        /// <summary>
        /// 获取模型组件
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="modelName">模型名称</param>
        /// <returns></returns>
        public T GetModelComponent<T>(string modelName)
        {
            foreach (var item in models)
            {
                if (modelName.Equals(item._Name))
                {
                    return item._Model.GetComponent<T>();
                }
            }
            return default;
        }
        /// <summary>
        /// 获取子物体组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public T[] GetModelsInChildren<T>(string modelName)
        {
            foreach (var item in models)
            {
                if (modelName.Equals(item._Name))
                {
                    return item._Model.GetComponentsInChildren<T>();
                }
            }
            return default;
        }
        /// <summary>
        /// 获取模型
        /// </summary>
        /// <param name="modelName"></param>
        /// <returns></returns>
        public GameObject GetModels(string modelName)
        {
            for (int i = 0; i < models.Count; i++)
            {
                if (models[i]._Name.Equals(modelName))
                {
                    return models[i]._Model;
                }
            }
            return new GameObject();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            foreach (var item in models)
            {
                item._Model.SetActive(item._isShow);
            }
        }
    }
}