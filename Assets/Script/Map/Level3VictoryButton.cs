using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 修改第三关的VictoryButton
public class Level3VictoryButton : MonoBehaviour
{
    public int levelNumber = 3; // 明确设置为3
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
            MapManager.Instance.CompleteLevel(levelNumber); // 先完成第三关
            Debug.Log($"已通知MapManager关卡 {levelNumber} 完成");
        }
        else
        {
            Debug.LogError("MapManager实例不存在！");
        }

        // 跳转到结尾场景
        Debug.Log("正在跳转到结尾场景...");
        SceneManager.LoadScene("End");
    }
}