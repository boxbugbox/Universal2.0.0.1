using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldDialogue : MonoBehaviour
{
    public WorldDialogueType worldDialogueType;
    public Text m_WorldDialogue, m_Text;
    public Transform m_WorldDialogueTransform;
    Vector3 m_Position;
    private void Start()
    {
        //SetStatus(false);
    }
    private void Update()
    {
        switch (worldDialogueType)
        {
            case WorldDialogueType.Default:
                break;
            case WorldDialogueType.Position:
                this.transform.position = Camera.main.WorldToScreenPoint(m_Position);
                break;
            case WorldDialogueType.Transform:
                this.transform.position = Camera.main.WorldToScreenPoint(m_WorldDialogueTransform.position);
                break;
            default:
                break;
        }        
    }
    public void SetContent(string conten ,Vector3 v3)
    {
        this.gameObject.SetActive(true);
        m_WorldDialogue.text = m_Text.text = conten;
        m_Position = v3;       
    }
    public void SetContent(string conten)
    {
        this.gameObject.SetActive(true);
        m_WorldDialogue.text = m_Text.text = conten;
    }
    public void SetStatus(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
    public void SetPoints(Vector3 v3)
    {
        this.gameObject.SetActive(true);
        m_Position = v3;
    }
}
public enum WorldDialogueType
{
    Default,
    Position,
    Transform,
}

