using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ChoiceTools : MonoBehaviour
{
    [FilePath] public string m_path;
    [ShowInInspector] public List<string[]> m_list = new List<string[]>();
    public Transform m_transform;
    private void GetFileData()
    {
        foreach (string str in System.IO.File.ReadAllLines(m_path, Encoding.Default))
        {
            Debug.Log(str);
            var modeData = str.Split('|');
            m_list.Add(modeData);
        }
    }
    private void AddChoice()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            var data = m_list[i];
            GameObject go = Resources.Load("Choice") as GameObject;
            go = Instantiate(go);
            go.transform.SetParent(transform, false);
            go.name = "Choice" + i;
            var skin = go.GetComponent<ChoiceSkin>();
            skin.SetSkin(data);
        }
    }
    private void AddChoiceSerialnumber()
    {
        for (int i = 0; i < m_list.Count; i++)
        {
            GameObject go = Resources.Load("ChoiceSerialnumber") as GameObject;
            go = Instantiate(go);
            go.transform.SetParent(m_transform, false);
            go.name = "ChoiceSerialnumber" + i;
            var skin = go.GetComponent<ChoiceSerialnumberSkin>();
            skin.SetSkin(i + 1);
        }
    }
    private void OnGUI()
    {
        if (GUILayout.Button("Get File Data", GUILayout.Width(150), GUILayout.Height(30)))
        {
            GetFileData();
        }
        if (GUILayout.Button("Add Choice", GUILayout.Width(150), GUILayout.Height(30)))
        {
            AddChoice();
        }
        if (GUILayout.Button("Add Choice Serialnumber", GUILayout.Width(150), GUILayout.Height(30)))
        {
            AddChoiceSerialnumber();
        }
    }
}
