using Sirenix.OdinInspector;
using UnityEngine;
[System.Serializable]
public class Question
{
    [LabelWidth(40), LabelText("题目："), HorizontalGroup("Horizontal", marginRight: 30,width:0.35f)] public string title;
    [LabelWidth(40), LabelText("分数："), HorizontalGroup("Horizontal")] public float score;
}
