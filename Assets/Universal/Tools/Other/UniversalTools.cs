using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UniversalTools
{

    public delegate void DelayedCallback();
    /// <summary>
    /// 字符串转Vector3
    /// </summary>
    /// <param name="p_sVec3">需要转换的字符串</param>
    /// <returns></returns>
    public static Vector3 GetVec3ByString(string p_sVec3)
    {
        if (p_sVec3.Length <= 0)
            return Vector3.zero;

        string[] tmp_sValues = p_sVec3.Trim(' ').Split(',');
        if (tmp_sValues != null && tmp_sValues.Length == 3)
        {
            float tmp_fX = float.Parse(tmp_sValues[0]);
            float tmp_fY = float.Parse(tmp_sValues[1]);
            float tmp_fZ = float.Parse(tmp_sValues[2]);

            return new Vector3(tmp_fX, tmp_fY, tmp_fZ);
        }
        return Vector3.zero;
    }
    /// <summary>
    /// 字符串转换Quaternion
    /// </summary>
    /// <param name="p_sVec3">需要转换的字符串</param>
    /// <returns></returns>
    public static Quaternion GetQuaByString(string p_sVec3)
    {
        if (p_sVec3.Length <= 0)
            return Quaternion.identity;

        string[] tmp_sValues = p_sVec3.Trim(' ').Split(',');
        if (tmp_sValues != null && tmp_sValues.Length == 4)
        {
            float tmp_fX = float.Parse(tmp_sValues[0]);
            float tmp_fY = float.Parse(tmp_sValues[1]);
            float tmp_fZ = float.Parse(tmp_sValues[2]);
            float tmp_fH = float.Parse(tmp_sValues[3]);

            return new Quaternion(tmp_fX, tmp_fY, tmp_fZ, tmp_fH);
        }
        return Quaternion.identity;
    }
    /// <summary>
    /// DOTweenTo延时回调
    /// </summary>
    /// <param name="delayedTimer">延时的时间</param>
    /// <param name="loopTimes">循环次数，0:不循环；负数：无限循环；正数：循环多少次</param>
    public static void DOTweenToDelay(float delayedTimer, int loopTimes,DelayedCallback delayedCallback)
    {
        float timer = 0;
        //DOTwwen.To()中参数：前两个参数是固定写法，第三个是到达的最终值，第四个是渐变过程所用的时间
        Tween t = DOTween.To(() => timer, x => timer = x, 1, delayedTimer).OnStepComplete(() =>
        {
            delayedCallback.Invoke();
        }).SetLoops(loopTimes);
    }
}
