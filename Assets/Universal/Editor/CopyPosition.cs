using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CopyPosition : Editor
{
    [UnityEditor.MenuItem("Tools/Universal Tools/Copy Position %Q")]
    static void CopyXYZ()
    {
        GameObject obj = UnityEditor.Selection.activeGameObject;
        if (obj != null)
        {
            string ret = obj.transform.localPosition.x + "f,"
            + obj.transform.localPosition.y + "f,"
            + obj.transform.localPosition.z + "f";
            GUIUtility.systemCopyBuffer = ret;
        }
    }

    [MenuItem("Tools/Universal Tools/Show and Hide _F1")]
    static void EditorCustorkKeys1()
    {
        Transform active = Selection.activeTransform;
        if (active == null)
            return;
        bool isactive = active.gameObject.activeSelf;
        active.gameObject.SetActive(!isactive);
    }
    [MenuItem("Tools/Universal Tools/Universal Screenshot %&Q")]
    static void UniversalScreenshot()
    {
        string path = Application.streamingAssetsPath + "/" + DateTime.Now.ToString("yyyy-MM-dd-HHmmss") + ".jpg";
        ScreenCapture.CaptureScreenshot(path, 0);
    }
}
