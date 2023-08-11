using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipManage : MonoBehaviour
{
    private Button skipbutton;
    public delegate void SkipHandler();
    private SkipHandler onSkipHandler;
    private void Awake()
    {
        skipbutton = this.GetComponent<Button>();
    }
    private void OnSkin(string title)
    {
        Question question = new Question();
        question.title = title;
        question.score = 0f;
        Universal.UniversalOverall.universalOverall.Questions.Add(question);
        onSkipHandler?.Invoke();
        this.gameObject.SetActive(false);
    }
    public void AddSkipHandler(string title,SkipHandler skipHandler)
    {
        skipbutton.onClick.RemoveAllListeners();
        this.gameObject.SetActive(true);
        onSkipHandler = skipHandler;
        skipbutton.onClick.AddListener(()=> OnSkin(title));
    }
}
