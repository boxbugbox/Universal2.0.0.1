using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnConfirmEventView : MonoBehaviour
{
    [SerializeField]private ConfirmEventType confirmEventType;
    private Button confirm;
    public ConfirmEventType ConfirmEventType { get => confirmEventType; set => confirmEventType = value; }

    private void Start()
    {
        confirm = GetComponent<Button>();
        confirm.onClick.AddListener(OnConfirm);
    }
    private void OnConfirm()
    {
        switch (confirmEventType)
        {
            case ConfirmEventType.Default:
                break;
            default:
                break;
        }
    }
}
public enum ConfirmEventType
{
    Default,
}
