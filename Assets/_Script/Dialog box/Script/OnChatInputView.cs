using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Universal.Function;
using Universal;
using Universal.Medicine;

public class OnChatInputView : MonoBehaviour
{
    public InputField inputBox;
    public Transform _parent;
    public Button viewAll;
    public GameObject chatRecord, m_lock;

    ControllerManger controllerManger;
    protected Timer timer;
    protected bool isOpen;
    protected float steps;

    public delegate void OnChatInputViewDelegate();
    OnChatInputViewDelegate chatInputCallback;
    private void Start()
    {
        timer = GameObject.FindGameObjectWithTag("ControllerManger").GetComponent<Timer>();     
        viewAll.onClick.AddListener(OnOpenChatRecord);
        inputBox.onEndEdit.AddListener(delegate { SendMessage(); });
        inputBox.ActivateInputField();
        m_lock.SetActive(false);
        steps = 1f;
    }
    private void SendMessage()
    {
        if ((Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter)) && !inputBox.text.Equals(""))
        {
            GameObject doctor = (GameObject)Resources.Load("Doctor");
            doctor = Instantiate(doctor);
            doctor.transform.SetParent(_parent, false);
            doctor.GetComponent<Message>().UpdateMessage(inputBox.text);
            VerifyKeywords(inputBox.text.Trim(), controllerManger.steps);//Trim()消除空格
            inputBox.text = "";
            inputBox.ActivateInputField();
        }
        else
        {
            inputBox.DeactivateInputField();
            //Debug.Log(Output.print("内容为空"));
        }
    }
    private void VerifyKeywords(string keywords,float step)
    {
        #region 示例
        /*if (step.Equals(100))
        {
            if ((keywords.Contains("听见") || keywords.Contains("说话")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("嗯，能，，，");
                    steps = 3f;
                });
            }
            else if (keywords.Contains("腕带") && steps.Equals(2f))
            {
                GameObject patients = (GameObject)Resources.Load("Patients");
                patients = Instantiate(patients);
                patients.transform.SetParent(_parent, false);
                patients.GetComponent<Message>().UpdateMessage("<color=#0000FF>[腕带]</color>");
                patients.AddComponent<Button>().onClick.AddListener(delegate
                {
                    patients.GetComponent<Message>().UpdateMessage("<color=#FFFFFF>[腕带]</color>");
                    patients.GetComponent<Button>().interactable = false;
                    Universal.UniversalAnimatorControl.universalAnimatorControl.PlayAnimator(new string[] { "护士A", "患者", "病房道具" }, "Check the wristband", 0, 0);
                    timer.start(2f, delegate
                    {
                        steps = 3f;
                        timer.reset();
                    });
                });
            }
            else if (keywords.Contains("了解") && steps.Equals(3f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("好的");
                    steps = 4f;
                });
            }
            else if (keywords.Contains("名字") && steps.Equals(4f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("你好，我叫刘远。");
                    steps = 5f;
                });
            }
            else if (keywords.Contains("哪里不舒服") && steps.Equals(5f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我现在胸疼，感觉特别不舒服，我今天早晨跑步就感觉胸部那块疼的不行，出了很多汗，就赶紧打电话让女儿送我来医院了。");
                    steps = 6f;
                });
            }
            else if (keywords.Contains("多久") && steps.Equals(6f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("得有两个钟头了，很疼。");
                    steps = 7f;
                });
            }
            else if (keywords.Contains("缓解") && steps.Equals(7f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("没有缓解，而且也服用了硝酸甘油但是没有什么好转，还是很疼。");
                    steps = 8f;
                });
            }
            else if (keywords.Contains("不舒服") && steps.Equals(8f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("有感觉到恶心呕吐，全身大汗，呼吸困难。");
                    steps = 9f;
                });
            }
            else if (keywords.Contains("变质")&& steps.Equals(9f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("没有吃过，早饭都是正常的饭菜没有什么特别的。");
                    steps = 10f;
                });
            }
            else if (keywords.Contains("肝炎") && steps.Equals(10f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("也没有。");
                    steps = 11f;
                });
            }
            else if (keywords.Contains("病史") && steps.Equals(11f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我有高血压6年了，也经常心绞痛，医院开的药也一直随身带着。");
                    steps = 12f;
                });
            }
            else if (keywords.Contains("手术") && steps.Equals(12f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("这个没有。");
                    steps = 13f;
                });
            }
            else if (keywords.Contains("食物过敏") && steps.Equals(13f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("没有的。");
                    steps = 14f;
                });
            }
            else if (keywords.Contains("比较疼痛") && steps.Equals(14f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("左侧胸口的位置。");
                    steps = 15f;
                });
            }
            else if (keywords.Contains("疼痛的程度") && steps.Equals(15f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("像是心脏被大石头压着，非常疼痛，有点快死亡的感觉。");
                    steps = 16f;
                });
            }
            else if (keywords.Contains("疼痛减轻") && steps.Equals(16f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("吃药不能缓解，还是非常疼痛。");
                    steps = 17f;
                });
            }
            else if (keywords.Contains("您的姓名") && steps.Equals(17f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我叫刘远。");
                    steps = 18f;
                });
            }
            else if (keywords.Contains("体格检查") && steps.Equals(18f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("好的护士。");
                    steps = 19f;
                    NextManage.nextManage.SetPrompt(true);
                });
            }
            else
            {
                GameObject patients = (GameObject)Resources.Load("Patients");
                patients = Instantiate(patients);
                patients.transform.SetParent(_parent, false);
                patients.GetComponent<Message>().UpdateMessage("<color=red>提示：请按顺序提问！</color>");
            }
        }*/
        #endregion
        VerifyKeywordsContent(keywords, step);
    }
    private void OnEnable()
    {
        steps = 1f;
        isOpen = false;
        chatRecord.transform.DOScaleX(0f, 0f);
        chatRecord.SetActive(false);
        controllerManger = GameObject.FindGameObjectWithTag("ControllerManger").GetComponent<ControllerManger>();
       /* if (controllerManger.steps == 2 || controllerManger.steps == 2.1 || controllerManger.steps == 6)
        {
            m_lock.SetActive(true);
        }
        else
        {
            m_lock.SetActive(false);
        }*/
    }
    private void OnOpenChatRecord()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            chatRecord.SetActive(true);
            chatRecord.transform.DOScaleX(1f, 0.5f);
        }
        else
        {
            chatRecord.transform.DOScaleX(0f, 0f);
            chatRecord.SetActive(false);
        }
    }
    public void SetMessage(string name,string content)
    {
        if (name.Equals("Doctor"))
        {
            GameObject doctor = (GameObject)Resources.Load("Doctor");
            doctor = Instantiate(doctor);
            doctor.transform.SetParent(_parent, false);
            doctor.GetComponent<Message>().UpdateMessage(content);
            VerifyKeywords(content, controllerManger.steps);
        }
        if (name.Equals("Patients"))
        {
            GameObject doctor = (GameObject)Resources.Load("Patients");
            doctor = Instantiate(doctor);
            doctor.transform.SetParent(_parent, false);
            doctor.GetComponent<Message>().UpdateMessage(content);
            VerifyKeywords(content, controllerManger.steps);
        }
    }
    public void AddCallback(OnChatInputViewDelegate onChatInputViewDelegate)
    {
        chatInputCallback = onChatInputViewDelegate;
    }
    protected virtual void VerifyKeywordsContent(string keywords, float step)
    {
        if (step.Equals(4f))
        {
            if ((keywords.Contains("责任护士") || keywords.Contains("名字")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("你好护士我叫王云");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("看") || keywords.Contains("腕带")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("好的");
                    /*patients.GetComponent<Message>().UpdateMessage("<color=#0000FF>[腕带]</color>");
                    patients.AddComponent<Button>().onClick.AddListener(delegate
                    {
                        patients.GetComponent<Message>().UpdateMessage("<color=#FFFFFF>[腕带]</color>");
                        patients.GetComponent<Button>().interactable = false;
                    });*/
                    steps = 3f;
                });
            }
            else if ((keywords.Contains("手术") || keywords.Contains("尿管")) && steps.Equals(3f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("好的护士");
                    steps = 4f;
                });
            }
            else if ((keywords.Contains("过程") || keywords.Contains("小心")) && steps.Equals(4f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("好的");
                    steps = 5f;

                });
            }
            else if ((keywords.Contains("阴部") || keywords.Contains("掀开")) && steps.Equals(5f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("不冷，温度正好");
                    steps = 6f;
                });
            }
            else if ((keywords.Contains("皮肤完好") || keywords.Contains("清洗")) && steps.Equals(6f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我自己可以");
                    steps = 7f;
                });
            }
            else if ((keywords.Contains("检查") || keywords.Contains("膀胱")) && steps.Equals(7f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject doctor = (GameObject)Resources.Load("Doctor");
                    doctor = Instantiate(doctor);
                    doctor.transform.SetParent(_parent, false);
                    doctor.GetComponent<Message>().UpdateMessage("那我去准备下用物，一会过来给您插尿管。");
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
        else if (step.Equals(9f))
        {
            if ((keywords.Contains("再次") || keywords.Contains("名字")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我叫王云。");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("准备") || keywords.Contains("导尿")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我已经准备好了。");
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
        else if (step.Equals(26f))
        {
            if ((keywords.Contains("请问") || keywords.Contains("名字")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我叫王云。");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("深呼吸") || keywords.Contains("轻柔")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
        else if (step.Equals(29f))
        {
            if ((keywords.Contains("插好") || keywords.Contains("不适")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("还好，没有感觉疼。");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("尿液") || keywords.Contains("配合")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
        else if (step.Equals(32f))
        {
            if ((keywords.Contains("脱落") || keywords.Contains("感染")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("没有了护士。");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("呼叫器") || keywords.Contains("好好休息")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
        else if (step.Equals(35f))
        {
            if ((keywords.Contains("精神") || keywords.Contains("紧张")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("好的。");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("告诉我") || keywords.Contains("姓名")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("我叫王云。");
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
        else if (step.Equals(39f))
        {
            if ((keywords.Contains("不舒服") || keywords.Contains("吗")) && steps.Equals(1f))//Contains()验证关键字
            {
                timer.start(0.5f, delegate
                {
                    GameObject patients = (GameObject)Resources.Load("Patients");
                    patients = Instantiate(patients);
                    patients.transform.SetParent(_parent, false);
                    patients.GetComponent<Message>().UpdateMessage("没有。");
                    steps = 2f;
                });
            }
            else if ((keywords.Contains("多喝水") || keywords.Contains("呼叫器")) && steps.Equals(2f))
            {
                timer.start(0.5f, delegate
                {
                    NextManage.nextManage.SetPrompt(true);
                    timer.reset();
                });
            }
        }
    }
}
