using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Universal;
using Universal.Medicine;

public class UniversalMenuControl : MonoBehaviour
{
    #region 单例
    public static UniversalMenuControl universalMenuControl;
    private void Awake()
    {
        universalMenuControl = this;
    }
    #endregion

    [SerializeField]private Toggle[] menus;
    Dictionary<float,float> m_Progressbar = new Dictionary<float,float>();
    [LabelText("收起菜单"), FoldoutGroup("菜单")] public Button packup;
    [LabelText("关闭菜单"), FoldoutGroup("菜单")] public Button m_close;
    [LabelText("收起菜单图标"), FoldoutGroup("菜单")] public GameObject packupIcon;
    [LabelText("收起菜单物体"), FoldoutGroup("菜单")] public CanvasGroup packupObject;

    [LabelText("进度物体"), FoldoutGroup("学习进度")] public GameObject progressObject;
    [LabelText("进度条"), FoldoutGroup("学习进度")] public Slider progress;
    [LabelText("当前进度"), FoldoutGroup("学习进度")] public Text nowProgress;
    [LabelText("当前步骤"), FoldoutGroup("学习进度")] public Text nowStep;
    [LabelText("当前步骤Box"), FoldoutGroup("学习进度")] public Text nowStepBox;

    bool isDamp;
    bool isUp;
    ControllerManger controllerManger;

    public Toggle[] Menus { get => menus; set => menus = value; }
    public Dictionary<float, float> Progressbar { get => m_Progressbar; set => m_Progressbar = value; }

    private void Start()
    {
        isDamp = true;
        packup.onClick.AddListener(OnPackUp);
        m_close.onClick.AddListener(() =>
        {
            packupIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0f);
            packupObject.transform.DOScaleX(0, 0f).OnComplete(delegate () {
                isDamp = true;
                isUp = !isUp;
            });
        });
        for (int i = 0; i < menus.Length; i++)
        {
            int k = i;
            menus[k].onValueChanged.AddListener((bool isOn) => OnMenu(isOn, k));
        }
        Init();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            isUp = !isUp;
            packupIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0f);
            packupObject.transform.DOScaleX(0, 0f).OnComplete(delegate () {
                isDamp = true;
            });
        }
    }
    private void OnMenu(bool isOn, int j)
    {
        controllerManger = GameObject.FindGameObjectWithTag("ControllerManger").GetComponent<ControllerManger>();
        controllerManger.OnMenu(isOn, j);
        if (isOn)
        {
            SetProgressbar(j,0.046f);
            nowStepBox.text = nowStep.text = menus[j].name;
            /* if (menus[j].name.Length < 5)
             {
                 nowStep.fontSize = 22;
                 nowStep.text = menus[j].name;
             }
             else
             {
                 nowStep.fontSize = 18;
                 nowStep.text = menus[j].name;
             }           */
        }
    }
    private void OnPackUp()
    {
        if (isDamp)
        {
            isDamp = false;
            isUp = !isUp;
            if (isUp)
            {
                #region 隐藏显示效果
                packupIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0.6f);
                packupObject.transform.DOScaleX(0, 0.6f).OnComplete(delegate () {
                    isDamp = true;
                });
                #endregion
            }
            else
            {
                #region 隐藏显示效果
                packupIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 0f), 0.6f);
                packupObject.transform.DOScaleX(1, 0.6f).OnComplete(delegate () {
                    isDamp = true;
                });
                #endregion
            }
        }       
    }
    private void Init()
    {
        isUp = !isUp;
        packupIcon.transform.DOLocalRotate(new Vector3(0f, 0f, 180f), 0f);
        packupObject.transform.DOScaleX(0, 0f).OnComplete(delegate () {
            isDamp = true;
        });
        progress.value = 0f;
        SetMenuActive(false);
        SetProgressbar(-1, 0f);
    }
    /// <summary>
    /// 设置菜单状态
    /// </summary>
    /// <param name="isActive"></param>
    public void SetMenuActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }
    /// <summary>
    /// 设置学习物体状态
    /// </summary>
    /// <param name="isActive"></param>
    public void SetProgressActive(bool isActive)
    {
        progressObject.SetActive(isActive);
    }
    /// <summary>
    /// 设置进度
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public void SetProgressbar(float key,float value)
    {
        if (!Progressbar.ContainsKey(key))
        {
            Progressbar.Add(key, value);
            progress.value += value;
            nowProgress.text = "当前实验进度" + (progress.value * 100).ToString("#0") + "%";
        }
    }
    /// <summary>
    /// 初始化精灵
    /// </summary>
    public void InMenu()
    {
        Init();
        Progressbar.Clear();
        SetProgressbar(-1, 0f);
        nowStepBox.text = nowStep.text = "案例导入";
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].isOn = false;
            menus[i].GetComponent<UpdateLabel>().Init();
        }
    }
}
