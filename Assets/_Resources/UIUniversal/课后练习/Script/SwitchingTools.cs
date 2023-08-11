using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchingTools : MonoBehaviour
{
    public GameObject[] choicesObject;
    public Toggle[] ChoiceSerialnumbers;
    public Button m_Up;
    public Button m_Down;

    int Index;

    private void Start()
    {
        for (int i = 0; i < ChoiceSerialnumbers.Length; i++)
        {
            int k = i;
            ChoiceSerialnumbers[k].onValueChanged.AddListener((bool isOn) => OnToggle(isOn, k));
        }
        m_Up.onClick.AddListener(OnUp);
        m_Down.onClick.AddListener(OnDown);
    }
    private void OnToggle(bool isOn,int j)
    {
        if (isOn)
        {
            Index = j;
        }
        choicesObject[j].SetActive(isOn);
    }
    private void OnUp()
    {
        if (Index > 0)
        {
            Index--;
            ChoiceSerialnumbers[Index].isOn = true;
        }
    }
    private void OnDown()
    {       
        if (Index < ChoiceSerialnumbers.Length - 1)
        {
            Index++;
            ChoiceSerialnumbers[Index].isOn = true;
        }
    }
    public void SetChoiceSerialnumbersIcon(int index)
    {
        ChoiceSerialnumbers[index].GetComponent<ChoiceSerialnumberSkin>().SetIconActive(true);
    }
}
