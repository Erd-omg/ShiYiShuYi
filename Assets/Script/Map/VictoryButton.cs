using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryButton : MonoBehaviour
{
    public int levelNumber;
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnVictoryButtonClick);
    }

    void OnVictoryButtonClick()
    {
        Debug.Log($"点击了关卡 {levelNumber} 的胜利按钮");

        // 确保MapManager存在
        MapManager.EnsureInstanceExists();

        if (MapManager.Instance != null)
        {
            MapManager.Instance.CompleteLevel(levelNumber);
            Debug.Log($"已通知MapManager关卡 {levelNumber} 完成");
        }
        else
        {
            Debug.LogError("MapManager实例不存在！");
        }

        // 直接使用SceneManager.LoadScene，不要使用异步加载
        Debug.Log("正在跳转到Map场景...");
        SceneManager.LoadScene("Map");
    }

    // 添加一个简单的协程来确保场景跳转
    IEnumerator LoadMapSceneCoroutine()
    {
        yield return null; // 等待一帧

        // 再次检查并确保状态保存
        if (MapManager.Instance != null)
        {
            MapManager.Instance.CompleteLevel(levelNumber);
        }

        // 直接加载场景
        SceneManager.LoadScene("Map");
    }
}