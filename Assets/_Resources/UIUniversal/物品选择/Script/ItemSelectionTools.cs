using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelectionTools : MonoBehaviour
{
    public Sprite[] sprites;     
    public GameObject cloneObject;
    public Transform m_parent;

    [SerializeField]List<GameObject> selectionRegionObject = new List<GameObject>();
    [SerializeField]List<GameObject> item = new List<GameObject>();
    /// <summary>
    /// 创建选择区域
    /// </summary>
    [Button("CreateSelectionRegion")]
    private void CreateSelectionRegion()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject go = Instantiate(cloneObject) as GameObject;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.parent = m_parent;
            go.name = sprites[i].name;
            var mouseEnterTip = go.GetComponent<MouseEnterTip>();
            mouseEnterTip.m_Sprite = sprites[i];
        }
    }
    /// <summary>
    /// 创建用物
    /// </summary>
    [Button("CreateItem")]
    private void CreateItem()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            GameObject go = Instantiate(cloneObject) as GameObject;
            go.transform.parent = m_parent;
            go.name = sprites[i].name;
            ItemStyle system = go.GetComponent<ItemStyle>();
            system.m_image.name = sprites[i].name;
            system.m_image.sprite = sprites[i];
            system.m_label.text = sprites[i].name;
        }
    }
    /// <summary>
    /// Add SelectionItem.showObject 添加对应显示物体
    /// </summary>
    [Button("Add SelectionItem.showObject")]
    private void AddSelectionItemShowObject()
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            foreach (var item in selectionRegionObject)
            {
                if (item.name == sprites[i].name)
                {
                    item.GetComponent<SelectionItem>().showObject = this.item[i];
                }
            }
        }
    }
}
public enum CreateItem
{
    Default,
    [LabelText("CreateSelectionRegion:创建选择区域")]CreateSelectionRegion,
    [LabelText("CreateItem:创建用物")]CreateItem,
}
