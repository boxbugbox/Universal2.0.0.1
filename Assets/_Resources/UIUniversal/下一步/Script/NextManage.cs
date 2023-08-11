using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Universal;

public class NextManage : MonoBehaviour
{
    #region 单例
    public static NextManage nextManage;
    private void Awake()
    {
        nextManage = this;
        _nextStep = this.GetComponent<Button>();
        _animator = this.GetComponent<Animator>();
        _image = this.GetComponent<Image>();
        _canvasGroup = this.GetComponent<CanvasGroup>();    
    }
    #endregion
    public PromptType promptType;
    [Header("闪烁频率"), Range(0.0f, 5.0f)] public float flickerTime;
    
    private Image _image;   
    private Button _nextStep;
    private Animator _animator;
    private CanvasGroup _canvasGroup;

    Color hintColor;
    Color imageColor;
    bool isPrompt;
    private void Start()
    {
        imageColor = _image.color;
        hintColor = Color.white;
        _nextStep.onClick.AddListener(OnNext);
        Init();
    }
    private void Update()
    {
        if (isPrompt)
        {
            switch (promptType)
            {
                case PromptType.Default:
                    break;
                case PromptType.Move:
                    break;
                case PromptType.Fade:
                    hintColor.a = Mathf.PingPong(flickerTime * Time.time, 1f);//5*Time.time是闪烁频率，大家可以自己改，1F就是颜色的a的最大的值，意思就是从完全透明到完全不透明
                    _image.color = hintColor;//获取UI的image组件的颜色并把上面变化的hintcolor赋值给他
                    break;
                default:
                    break;
            }
            _nextStep.interactable = true;
            _canvasGroup.DOFade(1f, 0f);
        }
        else
        {
            switch (promptType)
            {
                case PromptType.Default:
                    break;
                case PromptType.Move:
                    break;
                case PromptType.Fade:
                    _image.color = imageColor;
                    break;
                default:
                    break;
            }
            _nextStep.interactable = false;
            _canvasGroup.DOFade(0f, 0f);
        }
    }
    /// <summary>
    /// 设置效果
    /// </summary>
    /// <param name="isStart"></param>
    public void SetPrompt(bool isStart)
    {
        isPrompt = isStart;
        switch (promptType)
        {
            case PromptType.Default:
                break;
            case PromptType.Move:
                if (isStart)
                {
                    _animator.SetBool("isPrompt", isPrompt);
                }
                else
                {
                    _animator.SetBool("isPrompt", isPrompt);
                }
                break;
            case PromptType.Fade:
                break;
            default:
                break;
        }        
    }
    private void OnNext()
    {
        //UniversalAudioSourceControl.universalAudioSourceControl.PlayEffect(7);
        //Debug.Log(Universal.Function.Output.print("触发"));
    }
    public void Init()
    {
        SetPrompt(false);
    }
}
public enum PromptType
{
    Default,
    Move,
    Fade
}
