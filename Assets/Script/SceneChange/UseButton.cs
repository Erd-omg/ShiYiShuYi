using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.Rendering.HDROutputUtils;

public class UseButton : MonoBehaviour
{
    public string SceneName;
    private AsyncOperation operation;
    private Button button;

    void Start()
    {
        button=this.GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
        StartCoroutine(loadScene());
    }

    //异步加载场景
    IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync(SceneName);

        //加载完场景后不要自动跳转
        operation.allowSceneActivation = false;

        yield return operation;
    }

    public void LoadScene()
    {
        operation.allowSceneActivation = true;
    }
}
