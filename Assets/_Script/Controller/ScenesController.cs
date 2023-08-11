using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    private void Awake()
    {
       
    }
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        // 添加场景加载委托
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(scene.name)); // 这里不设置的话 SceneManager.GetActiveScene() 会取不到场景 
        Debug.Log("SceneName = " + scene.name);
        Debug.Log("ActiveSceneName = " + SceneManager.GetActiveScene().name);
    }
    private void OnDisable()
    {
        // 移除场景委托
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void GoToScene(string nextscene)
    {
        // 卸载当前场景
        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(SceneManager.GetActiveScene().name));
        // 加载下一个场景
        SceneManager.LoadScene(nextscene, LoadSceneMode.Additive);  
    }
}
