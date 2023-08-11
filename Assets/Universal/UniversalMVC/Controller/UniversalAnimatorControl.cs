using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AnimatorT
{
    [LabelWidth(30), LabelText("名字"), HorizontalGroup("Horizontal",marginRight: 30,width:0.3f)] public string _Name;
    [LabelWidth(60), LabelText("动画组件"), HorizontalGroup("Horizontal")] public Animator _Animator;
}
namespace Universal
{
    public class UniversalAnimatorControl : MonoBehaviour
    {
        #region 单例
        public static UniversalAnimatorControl universalAnimatorControl;
        private void Awake()
        {
            universalAnimatorControl = this;
        }
        #region Editor
        [OnInspectorGUI]
        private void UniversalInspectorGUI()
        {
            Version version = new Version();
            GUILayout.Space(15);
            GUISkin customSkin;
            customSkin = (GUISkin)Resources.Load("Editor\\Control");
            GUILayout.Label("Universal Frame", customSkin.FindStyle("Header"));
            GUILayout.Label(version.VersionNumber, customSkin.FindStyle("Bottom"));
        }
        #endregion
        #endregion
        public List<AnimatorT> animators = new List<AnimatorT>();

        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="animatorName"></param>
        /// <param name="clipName">动画名称</param>
        /// <param name="layer">The layer index. If layer is -1, it plays the first state with the given state name or hash.</param>
        /// <param name="normalizedTime">0和1之间的时间偏移量</param>
        public void PlayAnimator(string animatorName, string clipName, int layer, float normalizedTime)
        {
            foreach (var item in animators)
            {
                if (animatorName.Equals(item._Name))
                {
                    item._Animator.Play(clipName, layer, normalizedTime);
                }
            }
        }
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="animatorName"></param>
        /// <param name="clipName">动画名称</param>
        /// <param name="layer">The layer index. If layer is -1, it plays the first state with the given state name or hash.</param>
        /// <param name="normalizedTime">0和1之间的时间偏移量</param>
        public void PlayAnimator(string[] animatorName, string clipName, int layer, float normalizedTime)
        {
            for (int i = 0; i < animatorName.Length; i++)
            {
                foreach (var item in animators)
                {
                    if (animatorName[i].Equals(item._Name))
                    {
                        item._Animator.Play(clipName, layer, normalizedTime);
                    }
                }
            }
        }
        /// <summary>
        /// 播放动画带速度
        /// </summary>
        /// <param name="animatorName">动画组</param>
        /// <param name="clipName">动画名称</param>
        /// <param name="layer"></param>
        /// <param name="normalizedTime"></param>
        /// <param name="speed">速度</param>
        public void PlayAnimator(string[] animatorName, string clipName, int layer, float normalizedTime, int speed)
        {
            for (int i = 0; i < animatorName.Length; i++)
            {
                foreach (var item in animators)
                {
                    if (animatorName[i].Equals(item._Name))
                    {
                        item._Animator.speed = speed;
                        item._Animator.Play(clipName, layer, normalizedTime);
                    }
                }
            }
        }
        /// <summary>
        /// 播放动画
        /// </summary>
        /// <param name="animatorName">动画名称</param>
        /// <param name="floatName">float名称</param>
        /// <param name="normalizedTime">时间偏移量</param>
        public void PlayAnimator(string[] animatorName, string floatName, float normalizedTime)
        {
            for (int i = 0; i < animatorName.Length; i++)
            {
                foreach (var item in animators)
                {
                    if (animatorName[i].Equals(item._Name))
                    {
                        item._Animator.SetFloat(floatName, normalizedTime);
                    }
                }
            }
        }       
        /// <summary>
        /// 暂停动画
        /// </summary>
        public void Pause()
        {
            foreach (var item in animators)
            {
                item._Animator.speed = 0;              
            }
        }
        /// <summary>
        /// 播放动画
        /// </summary>
        public void Play()
        {
            foreach (var item in animators)
            {
                item._Animator.speed = 1;
            }
        }
        /// <summary>
        /// 添加动画事件
        /// </summary>
        /// <param name="_animator"></param>
        /// <param name="_clipName">动画名称</param>
        /// <param name="_eventFunctionName">事件方法名称</param>
        /// <param name="_time">添加事件时间。单位：秒</param>
        private void AddAnimationEvent(Animator _animator, string _clipName, string _eventFunctionName, float _time)
        {
            AnimationClip[] _clips = _animator.runtimeAnimatorController.animationClips;
            for (int i = 0; i < _clips.Length; i++)
            {
                if (_clips[i].name == _clipName)
                {
                    AnimationEvent _event = new AnimationEvent();
                    _event.functionName = _eventFunctionName;
                    _event.time = _time;
                    _clips[i].AddEvent(_event);
                    break;
                }
            }
            _animator.Rebind();
        }
        /// <summary>
        /// 清除所有事件
        /// </summary>
        private void CleanAllEvent(AnimationClip[] clips)
        {
            for (int i = 0; i < clips.Length; i++)
            {
                clips[i].events = default(AnimationEvent[]);
            }
            Debug.Log("清除所有事件");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            foreach (var item in animators)
            {
                item._Animator.speed = 1;
            }
        }
    }
}

