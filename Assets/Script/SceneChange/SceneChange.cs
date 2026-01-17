using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(LoadScene);
        }
        else
        {
            Debug.LogError("UseButton: 没有找到Button组件");
        }
    }

    public void LoadScene()
    {
        Debug.Log($"正在加载场景: {SceneName}");

        if (!string.IsNullOrEmpty(SceneName))
        {
            // 直接同步加载场景
            SceneManager.LoadScene(SceneName);
        }
        else
        {
            Debug.LogError("场景名称为空！");
        }
    }
}
