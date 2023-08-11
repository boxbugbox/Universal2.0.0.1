using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Console : MonoBehaviour
{
    public delegate void SendCommand(string context);
    public static event SendCommand sendCommand;
    public static void Command(string context) {
        sendCommand(context);
    }

    InputField inputfield;
    private void Awake() {
        inputfield = GetComponent<InputField>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKey(KeyCode.Return)) {
            inputfield.ActivateInputField();
        }
        if (Input.GetKeyDown(KeyCode.Return)) {
            if(inputfield.text != "") {
                Command(inputfield.text);
            }
            inputfield.text = "";
        }
    }
}
