using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIEffect : MonoBehaviour
{
    public EfectType efectType;
    private bool isStart;
    public float flickerTime;
    public float delayTime;
    private Image m_Image;
    private Button m_Button;
    public delegate void TimerCallback();
    [PropertySpace(15)] public UnityEvent deltaEvent;
    private TimerCallback onCompleteCall;
    bool isDelay;
    float timer;
    Color hintColor;
    Color imageColor;

    public bool IsStart { get => isStart; set => isStart = value; }

    private void Awake()
    {
        m_Image = this.GetComponent<Image>();
        m_Button = this.GetComponent<Button>();
    }
    private void Start()
    {
        timer = 0;
        isStart = true;
        imageColor = m_Image.color;
        hintColor = Color.white;
        if (m_Button != null) m_Button.onClick.AddListener(OnPress);
    }
    private void LateUpdate()
    {
        if (isStart)
        {
            switch (efectType)
            {
                case EfectType.Default:
                    break;
                case EfectType.Fade:
                    hintColor.a = Mathf.PingPong(flickerTime * Time.time, 1f);
                    m_Image.color = hintColor;
                    break;
                default:
                    break;
            }           
        }
        else
        {
            switch (efectType)
            {
                case EfectType.Default:
                    break;
                case EfectType.Fade:
                    m_Image.color = imageColor;
                    break;
                default:
                    break;
            }
        }
        if (isDelay)
        {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                isDelay = false;
                deltaEvent.Invoke();
                onCompleteCall?.Invoke();
            }
        }
    }
    private void OnPress()
    {
               
    }
    /// <summary>
    /// 开始
    /// </summary>
    public void UIStart()
    {
        isStart = true;       
    }
    /// <summary>
    /// 开始
    /// </summary>
    /// <param name="time">时间</param>
    /// <param name="endCallback">结束回调</param>
    public void UIStart(float time, TimerCallback endCallback)
    {
        UIStart();
        isDelay = true;
        delayTime = time;
        onCompleteCall = endCallback;
    }
    public void Init()
    {
        timer = 0;
        isStart = false;
        isDelay = false;
    }
}

public enum EfectType
{
    Default,
    Fade,
}
