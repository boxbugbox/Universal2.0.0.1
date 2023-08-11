using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
/**
* Data:2021-11-11
* guqing
* 退出
*/
namespace Universal.Function
{
    public class SceneQuit : MonoBehaviour
    {
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    public static extern void Close();
#else
        private static void Close() { }
#endif
        public static void SceneEsc()
        {
            if (Application.platform == RuntimePlatform.WindowsPlayer)
            {
                Application.Quit();
            }
            else if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                Close();
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Debug.Log("Quit");
            }
        }

    }
}
