using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Universal.Function;

namespace Universal
{
    public class UniversalAudioSourceControl : MonoBehaviour
    {
        #region 单例
        public static UniversalAudioSourceControl universalAudioSourceControl;
        private void Awake()
        {
            universalAudioSourceControl = this;
        }
        #endregion
        [ListDrawerSettings(ShowIndexLabels = true), LabelWidth(-155)] public AudioClip[] dialogues;
        [ListDrawerSettings(ShowIndexLabels = true), LabelWidth(-155)] public AudioClip[] hints;
        [ListDrawerSettings(ShowIndexLabels = true), LabelWidth(-155)] public AudioClip[] effect;
        [Header("对话音播放器")] public AudioSource dialogueSource;
        [Header("提示音播放器")] public AudioSource hintSource;
        [Header("背景音播放器")] public AudioSource BGMSource;
        [Header("背景音播放器")] public AudioSource effectSource;
        /// <summary>
        /// 播放配音
        /// </summary>
        /// <param name="Index">配音索引</param>
        public void PlayClips(int Index)
        {
            try
            {
                dialogueSource.clip = dialogues[Index];
                dialogueSource.Play();
            }
            catch (System.Exception e)
            {
                Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
            }
            
        }
        /// <summary>
        /// 播放提示音
        /// </summary>
        /// <param name="Index">提示音索引</param>
        public void PlayHints(int Index)
        {
            try
            {
                hintSource.clip = hints[Index];
                hintSource.Play();
            }
            catch (System.Exception e)
            {

                Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
            }
            
        }
        /// <summary>
        /// 播放音效
        /// </summary>
        /// <param name="Index"></param>
        public void PlayEffect(int Index)
        {
            try
            {
                effectSource.clip = effect[Index];
                effectSource.Play();
            }
            catch (System.Exception e)
            {
                Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
            }
            
        }
        /// <summary>
        /// 关闭声音
        /// </summary>
        /// <param name="soundType">声音类型</param>
        public void CloseSound(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Default:
                    break;
                case SoundType.BackgroundMusic:
                    BGMSource.volume = 0;
                    break;
                case SoundType.ConversationalSound:
                    dialogueSource.volume = 0;
                    break;
                case SoundType.PromptSound:
                    hintSource.volume = 0;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 开启声音
        /// </summary>
        /// <param name="soundType">声音类型</param>
        public void OpenSound(SoundType soundType)
        {
            switch (soundType)
            {
                case SoundType.Default:
                    break;
                case SoundType.BackgroundMusic:
                    BGMSource.volume = 1;
                    break;
                case SoundType.ConversationalSound:
                    dialogueSource.volume = 1;
                    break;
                case SoundType.PromptSound:
                    hintSource.volume = 1;
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 设置播放速度
        /// </summary>
        /// <param name="value"></param>
        public void SetPitch(float value)
        {
            dialogueSource.pitch = value;
            hintSource.pitch = value;
            BGMSource.pitch = value;
        }
        /// <summary>
        /// 获取声音长度
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="soundType"></param>
        /// <returns></returns>
        public float GetAudioLength(int Index, SoundType soundType)
        {
            var length = 0f;
            switch (soundType)
            {
                case SoundType.Default:
                    break;
                case SoundType.BackgroundMusic:
                    break;
                case SoundType.ConversationalSound:
                    length = dialogues[Index].length;
                    break;
                case SoundType.PromptSound:
                    length = hints[Index].length;
                    break;
                default:
                    break;
            }
            return length;           
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public void Init()
        {
            dialogueSource.Stop();
            hintSource.Stop();
        }
    }
}

