using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Universal.Function;
namespace Universal.Medicine
{
    public class ControllerManger : MonoBehaviour
    {
        private UniversalCameraControl universalCameraControl;
        private UniversalViewControl universalViewControl;
        private UniversalModelControl universalModelControl;
        private UniversalAnimatorControl universalAnimatorControl;
        private UniversalAudioSourceControl universalAudioSourceControl;
        private CursorLock cursorLock;
        private NextManage nextManage;
        public Toggle[] toggleGroup;
        public HighlightingSystem.Highlighter[] highlightHints;
        public SpriteManage spriteManage;
        public SkipManage skipManage;
        [PropertySpace(0, 15)] public Button nextStep;
        [LabelText("步骤："), LabelWidth(40), HorizontalGroup("Horizontal", width: 0.31f, marginRight: 50),
            InlineButton("AddSteps", "+"), InlineButton("SubSteps", "-")] public float steps;
        [LabelText("模式："), LabelWidth(40), HorizontalGroup("Horizontal")] public PatternType patternType;
        [SerializeField, LabelText("全局数据控制器："), LabelWidth(100)] private bool isUNITY_WEBGL;
        private int itemcode;
        private void Start()
        {
            universalCameraControl = UniversalCameraControl.universalControl;
            universalViewControl = UniversalViewControl.universalViewControl;
            universalModelControl = UniversalModelControl.universalModelControl;
            universalAnimatorControl = UniversalAnimatorControl.universalAnimatorControl;
            universalAudioSourceControl = UniversalAudioSourceControl.universalAudioSourceControl;
            cursorLock = CursorLock.cursorLock;
            nextManage = NextManage.nextManage;
            if (nextStep != null) nextStep.onClick.AddListener(OnNextStep);
            for (int i = 0; i < toggleGroup.Length; i++)
            {
                int k = i;
                toggleGroup[k].onValueChanged.AddListener((bool isOn) => OnMenu(isOn, k));
            }
            if (isUNITY_WEBGL)
            {
                steps = UniversalOverall.universalOverall.Steps;
                patternType = UniversalOverall.universalOverall.PatternType;
                switch (patternType)
                {
                    case PatternType.Default:
                        break;
                    case PatternType.学习:
                        UniversalMenuControl.universalMenuControl.SetMenuActive(true);
                        break;
                    case PatternType.考核:
                        if (steps == 0) steps = 1f;
                        TitleGroupManage.titleGroupManage.SetAssessmentTimesActive(true);
                        break;
                    default:
                        break;
                }
            }
            OnStart();
        }
        public void OnMenu(bool isOn, int j)
        {
            if (isOn)
            {
                Init();
                steps = j + 1;
                UniversalOverall.universalOverall.Steps = steps;
                int index = CurrentStepsScene(steps);
                if (CurrentScene(index))
                {
                    StartExecution(steps);
                }
                else
                {
                    this.GetComponent<LoadStaticResources>().SetDamp(() =>
                    {
                        SceneManager.LoadSceneAsync(index);
                    });
                    
                }
            }
        }
        public void OnNextStep()
        {
            switch (patternType)
            {
                case PatternType.Default:
                    break;
                case PatternType.学习:
                    if ((steps % 1) != 0)
                    {
                        StartExecution(steps);
                    }
                    else if (steps >= 40f)
                    {
                        spriteManage.SetMove(false);
                        universalAudioSourceControl.Init();
                        universalViewControl.HintObject.SetActive(false);
                        universalViewControl.EndUI.SetActive(true);
                        if (UniversalMenuControl.universalMenuControl != null)
                        {
                            UniversalMenuControl.universalMenuControl.InMenu();
                        }
                        else
                        {
                            Debug.Log(Output.print("NULL"));
                        }
                    }
                    else
                    {
                        try
                        {
                            UniversalMenuControl.universalMenuControl.Menus[(int)steps].isOn = true;
                        }
                        catch (System.Exception e)
                        {
#if UNITY_EDITOR
                            Init();
                            steps++;
                            StartExecution(steps);
#endif
                            Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
                        }

                    }
                    break;
                case PatternType.考核:
                    if ((steps % 1) != 0)
                    {
                        StartExecution(steps);
                    }
                    else
                    {
                        Init();
                        steps++;
                        UniversalOverall.universalOverall.Steps = steps;
                        int index = CurrentStepsScene((int)steps);
                        if (CurrentScene(index))
                        {
                            StartExecution(steps);
                        }
                        else
                        {
                            this.GetComponent<LoadStaticResources>().SetDamp(() =>
                            {
                                SceneManager.LoadSceneAsync(index);
                            });
                        }
                    }
                    break;
                default:
                    break;
            }         
        }
        /// <summary>
        /// 进入当前场景首次运行
        /// </summary>
        private void OnStart()
        {
            Init();
            StartExecution(steps);
        }
        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="step"></param>
        public void StartExecution(float step)
        {
            StopAllCoroutines();
            switch (patternType)
            {
                case PatternType.Default:
                    break;
                case PatternType.学习:
                    StartCoroutine(Execution(step));
                    break;
                case PatternType.考核:
                    StartCoroutine(ExecutionAssessment(step));
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// ExecutionAssessment 考核执行步骤
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        IEnumerator ExecutionAssessment(float step)
        {
            StepInit(step);
            if (step.Equals(0))
            {             
                yield return new WaitForSeconds(1);
                Debug.Log("考核模式");
            }
        }
        /// <summary>
        /// Execution 执行步骤
        /// </summary>
        /// <param name="step">步骤</param>
        /// <returns></returns>
        IEnumerator Execution(float step)
        {
            StepInit(step);            
            if (step.Equals(0))
            {
                universalCameraControl.SetCameraMachinePosition("Default");
                universalAnimatorControl.PlayAnimator(new string[] { "name", "name", "name" }, "name", 0, 0);
                universalViewControl.StartUI.SetActive(true);
                UniversalOverall.universalOverall.SetGuide(0, false);
                yield return new WaitForSeconds(1);
            }
            else if (step.Equals(1f))//案例导入
            {
                universalCameraControl.SetCameraMachinePosition("Default");
                /*universalAnimatorControl.PlayAnimator(new string[] { "护士A", "病人", "生命体征道具动画" }, "Stand by", 0, 0);*/
                universalViewControl.SetView(0, true, 1f, new Vector3(1350f, 0f, 0f), new Vector3(600f, 0f, 0f));             
            }
            else if (step.Equals(2f))//核对医嘱
            {
                universalCameraControl.SetCameraMachinePosition("Default");
                universalAnimatorControl.PlayAnimator(new string[] { "name", "name", "name" }, "name", 0, 0);
                universalViewControl.SetHintView("核对医嘱与执行单，床号、姓名、住院号、医嘱内容");
                universalViewControl.SetView(1, true);
                universalViewControl.SetView(2, true, 1f, new Vector3(1350f, 0f, 0f), new Vector3(400f, 0f, 0f));
            }
            else if (step.Equals(3f))//术前访视
            {
                universalCameraControl.SetCameraMachinePosition("床头卡");
                universalAnimatorControl.PlayAnimator(new string[] { "name", "name", "name" }, "name", 0, 0);
                universalViewControl.SetHintView("病房内进行术前访视，核对床头卡与病人姓名");
                highlightHints[0].tween = true;
            }
            else if (step.Equals(3.1f))
            {
                universalViewControl.SetView(3, true, 1f, new Vector3(1350f, 0f, 0f), new Vector3(560f, 0f, 0f));
            }
            else if (step.Equals(3.2f))
            {
                universalViewControl.SetView(3, false);
                universalCameraControl.SetCameraMachinePosition("腕带", 1f);
                highlightHints[1].tween = true;
            }
            else if (step.Equals(3.3f))
            {
                universalViewControl.SetView(4, true, 1f);
            }
            else if (step.Equals(4f))//评估患者
            {
                universalCameraControl.SetCameraMachinePosition("评估患者");
                universalAnimatorControl.PlayAnimator(new string[] { "name", "name", "name" }, "name", 0, 0);
                universalViewControl.SetHintView("了解熟悉病人病情，通过谈话取得病人知情同意，向患者和家属解释做动脉穿刺的目的和意义，争取清醒病人配合，告知需要配合的事项");
                yield return new WaitForSeconds(1);
                nextManage.SetPrompt(true);
            }
            else if (step.Equals(5f))//签署知情同意书
            {
                universalCameraControl.SetCameraMachinePosition("Default");
                universalAnimatorControl.PlayAnimator(new string[] { "name", "name", "name" }, "name", 0, 0);
                universalViewControl.SetView(5, true, new Vector3(0f, 1f, 1f), new Vector3(1f, 1f, 1f), 1f);
            }
        }
        /// <summary>
        /// 小步骤初始化
        /// </summary>
        /// <param name="step"></param>
        private void StepInit(float step)
        {
            skipManage.gameObject.SetActive(false);
        }
        /// <summary>
        /// 当前步骤场景
        /// </summary>
        /// <param name="stepsIndex"></param>
        /// <returns></returns>
        private int CurrentStepsScene(float stepsIndex)
        {            
            if (stepsIndex >= 0.0f && stepsIndex <= 50.0f)
            {
                return 3;
            }
            else if (stepsIndex > 50.0f && stepsIndex <= 60.0f)
            {
                return 4;
            }
            return 3;
        }
        /// <summary>
        /// 当前场景是否和步骤场景统一
        /// </summary>
        /// <param name="sceneIndex"></param>
        /// <returns></returns>
        private bool CurrentScene(int sceneIndex)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            if (index.Equals(sceneIndex))
            {
                return true;
            }
            return false;
        }      
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            foreach (var item in highlightHints)
            {
                item.tween = false;
            }
            if (spriteManage != null) spriteManage.Init();
            if (cursorLock != null) cursorLock.Init();
            if (nextManage != null) nextManage.Init();
            universalModelControl.Init();
            universalViewControl.Init();
            universalAnimatorControl.Init();
            universalCameraControl.SetpCamraMove(false);
            DOTween.PauseAll();
            try
            {
                universalAudioSourceControl.Init();
            }
            catch (System.Exception e)
            {
                Debug.Log(Output.print(this.name + "{ ERROR }" + e.Message));
            }
           
        }
        #region Editor
        [OnInspectorGUI]
        private void UniversalInspectorGUI()
        {
            GUILayout.Space(15);
            GUISkin customSkin;
            customSkin = (GUISkin)Resources.Load("Editor\\Control");
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("-"))
            {
                steps = float.Parse((steps -= 0.1f).ToString());
            }
            if (GUILayout.Button("+"))
            {
                steps = float.Parse((steps += 0.1f).ToString());
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("测试"))
            {
#if UNITY_EDITOR
                if (UnityEditor.EditorApplication.isPlaying == false)
                {
                    UnityEditor.EditorApplication.isPlaying = true;
                    return;
                }
                if ((steps % 1) != 0)
                {
                    StartExecution(steps);
                }
                else
                {
                    Init();
                    StartExecution(steps);
                }
#endif
            }
        }
        private void AddSteps()
        {
            steps++;
        }
        private void SubSteps()
        {
            steps--;
        }
        #endregion
    }
}

