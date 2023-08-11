using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
* Data:2021-10-28
* guqing
* 输出类
*/
namespace Universal.Function
{
    public class Output : MonoBehaviour
    {
        public static string DRanColor()
        {
            var r = UnityEngine.Random.Range(0.0001f, 1.000f);
            var g = UnityEngine.Random.Range(0.0001f, 1.0000f);
            var b = UnityEngine.Random.Range(0.0010f, 1.0000f);
            Color c = new Color(r, g, b);
            string h = ColorUtility.ToHtmlStringRGB(c);
            return h;
        }
        public static string print(string content)
        {
            return ("<color=#" + DRanColor() + ">" + content + "</color>");
        }
    }
}

