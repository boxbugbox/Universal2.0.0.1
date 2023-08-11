using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExaminationPaperManage : MonoBehaviour
{
    #region 单例
    public static ExaminationPaperManage examinationPaperManage;
    private void Awake()
    {
        examinationPaperManage = this;
    }
    #endregion

    Dictionary<string, string> selector = new Dictionary<string, string>();

    public Dictionary<string, string> Selector { get => selector; set => selector = value; }

    private void Start()
    {
        if (TitleGroupManage.titleGroupManage != null)
            TitleGroupManage.titleGroupManage.SetActive(false);
    }
    /// <summary>
    /// 单选题答题记录
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void Setselector(string key,string value)
    {
        if (selector.ContainsKey(key))
        {
            selector[key] = value;
        }
        else
        {
            selector.Add(key, value);
        }        
    }
}
