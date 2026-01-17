using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    public int levelNumber;
    public Image levelImage;
    public GameObject colorImage;
    public Button button;

    void Start()
    {
        // 确保MapManager存在
        MapManager.EnsureInstanceExists();

        // 获取组件
        button = GetComponent<Button>();
        if (levelImage == null)
        {
            levelImage = GetComponent<Image>();
        }

        // 注册更新事件
        MapManager.OnProgressUpdated += UpdateButtonState;

        // 初始更新状态
        UpdateButtonState();

        Debug.Log($"按钮 {levelNumber} 初始化完成");
    }

    void OnDestroy()
    {
        // 取消注册事件
        MapManager.OnProgressUpdated -= UpdateButtonState;
    }

    void OnEnable()
    {
        // 确保MapManager存在并更新状态
        MapManager.EnsureInstanceExists();
        UpdateButtonState();
    }

    public void UpdateButtonState()
    {
        if (MapManager.Instance == null)
        {
            Debug.LogWarning("MapManager实例不存在，尝试确保存在");
            MapManager.EnsureInstanceExists();
            if (MapManager.Instance == null)
            {
                Debug.LogError("无法创建MapManager实例");
                return;
            }
        }

        bool isUnlocked = false;
        bool isCompleted = false;
        bool shouldShow = true;

        switch (levelNumber)
        {
            case 1:
                isUnlocked = true;
                isCompleted = MapManager.Instance.level1Completed;
                break;
            case 2:
                isUnlocked = MapManager.Instance.level1Completed;
                isCompleted = MapManager.Instance.level2Completed;
                break;
            case 3:
                isUnlocked = MapManager.Instance.level2Completed;
                isCompleted = MapManager.Instance.level3Completed;
                break;
            case 4:
                isUnlocked = MapManager.Instance.gameCompleted;
                isCompleted = MapManager.Instance.gameCompleted;
                shouldShow = MapManager.Instance.gameCompleted;
                break;
        }

        // 设置按钮显示状态
        gameObject.SetActive(shouldShow);

        // 设置按钮交互性
        if (button != null)
        {
            button.interactable = isUnlocked;
        }

        // 设置彩色图像显示状态
        if (colorImage != null)
        {
            colorImage.SetActive(isCompleted);
        }

        Debug.Log($"按钮 {levelNumber} 更新: 解锁={isUnlocked}, 完成={isCompleted}, 显示={shouldShow}");
    }

    public void OnExitButtonClick()
    {
        if (levelNumber == 4)
        {
            Debug.Log("退出游戏");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}