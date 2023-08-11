using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ExaminationPaperTools : MonoBehaviour
{
    [ShowInInspector] public List<string[]> m_list = new List<string[]>();
    [ShowInInspector] public List<ExaminationPaper> m_Papers = new List<ExaminationPaper>();
    [ShowInInspector] public List<SerialNumberSkin> m_Serialnumbers = new List<SerialNumberSkin>();
    [FilePath,Title("")] public string m_path;
    public string m_CloneName;
    public Transform m_transform;
    /// <summary>
    /// 获取文件数据
    /// </summary>
    private void GetFileData()
    {
        foreach (string str in System.IO.File.ReadAllLines(m_path, Encoding.Default))
        {
            Debug.Log(str);
            var modeData = str.Split('|');
            m_list.Add(modeData);
        }
    }
    /// <summary>
    /// 生成试题
    /// </summary>
    private void AddPapers()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            var data = m_list[i];
            GameObject go = Resources.Load(m_CloneName) as GameObject;
            go = Instantiate(go);
            go.transform.SetParent(m_transform, false);
            go.name = "Paper" + i;
            var skin = go.GetComponent<ExaminationPaperSkin>();
            skin.SetSkin(data);
        }
    }
    /// <summary>
    /// 生成答题卡序号
    /// </summary>
    private void AddSerialnumber()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            GameObject go = Resources.Load("SerialNumber") as GameObject;
            go = Instantiate(go);
            go.transform.SetParent(m_transform, false);
            go.name = "SerialNumber" + i;
            var skin = go.GetComponent<SerialNumberSkin>();
            skin.SetNumber(i + 1);
        }
    }
    /// <summary>
    /// 试题和答题卡关联
    /// </summary>
    [Button]
    private void PaperAssociationSerialnumber()
    {
        for (int i = 0; i < m_Papers.Count; i++)
        {
            m_Papers[i].serialNumberSkin = m_Serialnumbers[i];
        }
    }
#if UNITY_EDITOR
    private void OnGUI()
    {
        if (GUILayout.Button("Get File Data", GUILayout.Width(150), GUILayout.Height(30)))
        {
            GetFileData();
        }
        if (GUILayout.Button("Add Choice", GUILayout.Width(150), GUILayout.Height(30)))
        {
            AddPapers();
        }
        if (GUILayout.Button("Add Choice Serialnumber", GUILayout.Width(150), GUILayout.Height(30)))
        {
            AddSerialnumber();
        }
    }
#endif
}
