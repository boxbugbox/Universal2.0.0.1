using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using UnityEngine.Events;
using Universal;

public class DragUI : MonoBehaviour, IDragHandler , IPointerEnterHandler, IPointerExitHandler
{
    [LabelText("当前模式类型"), HorizontalGroup("类型")] public PatternType patternType;
    [LabelWidth(30), LabelText("分数"), HorizontalGroup("类型")] public float m_score;
    [SerializeField, HorizontalGroup("题目"), LabelText("自定义题目")] private bool isTitle;
    [LabelWidth(30), LabelText("题目"), HorizontalGroup("题目", width: 1), ShowIf("isTitle")] public string m_title;
    public Camera m_Camera;
    public RectTransform m_Object;
    public Button m_Confirm;
    [PropertySpace(0, 15)] public GameObject m_Error;
    [BoxGroup("Limit"), HorizontalGroup("Limit/Hor_1"), LabelWidth(40f)] public float xMin, xMax;
    [BoxGroup("Limit"), HorizontalGroup("Limit/Hor_2"), LabelWidth(40f)] public float yMin, yMax;
    [BoxGroup("LimitRegion"), HorizontalGroup("LimitRegion/Hor_1"), LabelWidth(80f)] public float xRegionMin, xRegionMax;
    [BoxGroup("LimitRegion"), HorizontalGroup("LimitRegion/Hor_2"), LabelWidth(80f)] public float yRegionMin, yRegionMax;
    [PropertySpace(15)] public UnityEvent confirmEvent;
    bool isDrag;
    float rangeX;
    float rangeY;
    Vector2 v2;
    private void Awake()
    {
        v2 = m_Object.transform.localPosition;
    }
    private void Start()
    {
        m_Confirm.onClick.AddListener(ConfirmResult);
        OnStart();
    }
    private void Update()
    {
        DragRangeLimit();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_Object, eventData.position, eventData.enterEventCamera, out pos);
        m_Object.position = pos;
        if (isDrag) { }       
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        isDrag = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isDrag = false;
    }
    private void ConfirmResult()
    {
        patternType = UniversalOverall.universalOverall.PatternType;
        var v2 = m_Object.transform.localPosition;
        switch (patternType)
        {
            case PatternType.Default:
                break;
            case PatternType.学习:
                if ((v2.x > xMin && v2.x < xMax) && (v2.y > yMin && v2.y < yMax))
                {
                    OnCorrectCallback();
                }
                else
                {
                    m_Error.GetComponent<ErrorManager>().AddErrorEvent(delegate () { InError(); });
                    Debug.Log("<color=red> X:" + v2.x + "Y:" + v2.y + "</color>");
                }
                break;
            case PatternType.考核:
                Question question = new Question();
                if (isTitle) question.title = m_title;
                else question.title = this.name.Substring(9);
                if ((v2.x > xMin && v2.x < xMax) && (v2.y > yMin && v2.y < yMax))
                {
                    question.score = m_score;
                }
                else
                {
                    question.score = 0f;
                }
                UniversalOverall.universalOverall.Questions.Add(question);
                OnCorrectCallback();
                break;
            default:
                break;
        }
        
    }
    private void OnDisable()
    {
        m_Object.transform.localPosition = v2;
        OnEnd();
    }
    private void InError()
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);
        m_Object.localPosition = new Vector3(x, y, 0);
    }
    /// <summary>
    /// 拖拽范围限制
    /// </summary>
    private void DragRangeLimit()
    {
        //限制水平/垂直拖拽范围在最小/最大值内
        rangeX = Mathf.Clamp(m_Object.localPosition.x, xRegionMin, xRegionMax);
        rangeY = Mathf.Clamp(m_Object.localPosition.y, yRegionMin, yRegionMax);
        //更新位置
        m_Object.localPosition = new Vector3(rangeX, rangeY, 0);
    }
    protected virtual void OnStart() { }
    protected virtual void OnEnd() { }
    protected virtual void OnCorrectCallback()
    {
        confirmEvent.Invoke();
    }
}
