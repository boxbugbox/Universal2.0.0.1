using UnityEngine;
using DG.Tweening;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class CursorLock : MonoBehaviour
{
    #region 单例
    public static CursorLock cursorLock;
    private void Awake()
    {
        cursorLock = this;
        itembarManager = item.GetComponent<ItembarManager>();
    }
    #endregion
    public GameObject[] _target;
    public bool isLocked;
    public GameObject cursor, item;
    private ItembarManager itembarManager;
    NursingType type;
    Vector3 m_Point;

    public NursingType Type { get => type; set => type = value; }

    private void Start()
    {
       
    }
    private void Update()
    {
        if (isLocked)
        {
            switch (type)
            {
                case NursingType.Default:
                    this.transform.position = Camera.main.WorldToScreenPoint(_target[0].transform.position);
                    break;
                default:
                    break;
            }
        }
        else
        {
            this.transform.position = Camera.main.WorldToScreenPoint(m_Point);
        }        
    }
    /// <summary>
    /// Cursor Status
    /// </summary>
    /// <param name="cursorType"></param>
    public void SetCursorStatus(NursingType cursorType)
    {
        cursor.SetActive(true);
        item.SetActive(true);
        type = cursorType;
        itembarManager.SetStatus(true, 97f, 0.8f);
    }
    /// <summary>
    /// Cursor Status
    /// </summary>
    /// <param name="cursorType"></param>
    /// <param name="v3"></param>
    public void SetCursorStatus(NursingType cursorType, Vector3 v3)
    {
        cursor.SetActive(true);
        item.SetActive(true);
        m_Point = v3;
        type = cursorType;
        itembarManager.SetStatus(true, 97f, 0.8f);
    }
    /// <summary>
    /// Cursor Status
    /// </summary>
    /// <param name="cursorType"></param>
    /// <param name="v3"></param>
    public void SetCursorStatus(NursingType cursorType, Vector3 v3, int glint)
    {
        cursor.SetActive(true);
        item.SetActive(true);
        m_Point = v3;
        type = cursorType;
        itembarManager.SetGlint(glint);
        itembarManager.SetStatus(true, 97f, 0.8f);
    }
    /// <summary>
    /// Cursor Status
    /// </summary>
    /// <param name="cursorType"></param>
    /// <param name="v3"></param>
    /// <param name="isTrue"></param>
    public void SetCursorStatus(NursingType cursorType, Vector3 v3 ,bool isTrue)
    {
        cursor.SetActive(true);
        item.SetActive(isTrue);
        m_Point = v3;
        type = cursorType;
        itembarManager.SetStatus(true, 97f, 0.8f);
    }
    /// <summary>
    /// Get Cursor Type
    /// </summary>
    /// <returns></returns>
    public NursingType GetCursorType()
    {
        return type;
    }
    public void InItembar()
    {
        itembarManager.SetStatus(false, -73.7f, 0.8f);
        itembarManager.Init();
    }
    public void InItembar(TweenCallback endCallback)
    {
        itembarManager.SetStatus(false, -73.7f, 0.8f, endCallback);
        itembarManager.Init();       
    }
    public void Init()
    {
        cursor.SetActive(false);
        item.SetActive(false);
        itembarManager.SetStatus(false, -73.7f, 0.8f);
        itembarManager.Init();
    }
}
public enum NursingType
{
    Default,
    垫巾,
    弯盘,
    免洗洗手液,
    无菌手套,
    消毒棉球,
    导尿包,
    孔巾,
    导尿管,
    集尿袋,
    方盘,
    注射器,
    治疗巾,
}