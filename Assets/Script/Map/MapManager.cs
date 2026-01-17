using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance;

    // 存储关卡解锁状态
    public bool level1Completed = false;
    public bool level2Completed = false;
    public bool level3Completed = false;
    public bool gameCompleted = false;

    // 状态改变事件
    public static event System.Action OnProgressUpdated;

    // 标识是否已经初始化过（用于防止重复重置）
    private static bool hasInitialized = false;

    void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 每次启动游戏时重置进度
            if (!hasInitialized)
            {
                ResetProgress();
                hasInitialized = true;
                Debug.Log("MapManager已创建并初始化（进度已重置）");
            }
            else
            {
                LoadProgress();
                Debug.Log("MapManager已创建并初始化（加载已有进度）");
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 确保在任何场景中都能访问到MapManager
    public static void EnsureInstanceExists()
    {
        if (Instance == null)
        {
            GameObject managerObj = new GameObject("MapManager");
            Instance = managerObj.AddComponent<MapManager>();
            DontDestroyOnLoad(managerObj);

            // 新创建的实例也需要重置进度
            if (!hasInitialized)
            {
                Instance.ResetProgress();
                hasInitialized = true;
                Debug.Log("动态创建了MapManager实例（进度已重置）");
            }
            else
            {
                Instance.LoadProgress();
                Debug.Log("动态创建了MapManager实例（加载已有进度）");
            }
        }
    }

    // 关卡完成时调用
    public void CompleteLevel(int level)
    {
        EnsureInstanceExists();

        switch (level)
        {
            case 1:
                level1Completed = true;
                break;
            case 2:
                level2Completed = true;
                break;
            case 3:
                level3Completed = true;
                break;
        }

        SaveProgress();
        OnProgressUpdated?.Invoke();

        Debug.Log($"关卡 {level} 完成状态已更新");
    }

    // 游戏完成时调用
    public void CompleteGame()
    {
        EnsureInstanceExists();

        gameCompleted = true;
        level3Completed = true;

        SaveProgress();
        OnProgressUpdated?.Invoke();

        Debug.Log("游戏已完成，退出按钮已解锁");
    }

    // 保存进度
    void SaveProgress()
    {
        PlayerPrefs.SetInt("Level1Completed", level1Completed ? 1 : 0);
        PlayerPrefs.SetInt("Level2Completed", level2Completed ? 1 : 0);
        PlayerPrefs.SetInt("Level3Completed", level3Completed ? 1 : 0);
        PlayerPrefs.SetInt("GameCompleted", gameCompleted ? 1 : 0);
        PlayerPrefs.Save();

        Debug.Log("进度已保存");
    }

    // 加载进度
    void LoadProgress()
    {
        level1Completed = PlayerPrefs.GetInt("Level1Completed", 0) == 1;
        level2Completed = PlayerPrefs.GetInt("Level2Completed", 0) == 1;
        level3Completed = PlayerPrefs.GetInt("Level3Completed", 0) == 1;
        gameCompleted = PlayerPrefs.GetInt("GameCompleted", 0) == 1;

        Debug.Log($"加载进度: L1={level1Completed}, L2={level2Completed}, L3={level3Completed}, Game={gameCompleted}");
    }

    // 重置进度到初始状态
    public void ResetProgress()
    {
        level1Completed = false;
        level2Completed = false;
        level3Completed = false;
        gameCompleted = false;

        // 清除PlayerPrefs中的保存数据
        PlayerPrefs.DeleteKey("Level1Completed");
        PlayerPrefs.DeleteKey("Level2Completed");
        PlayerPrefs.DeleteKey("Level3Completed");
        PlayerPrefs.DeleteKey("GameCompleted");
        PlayerPrefs.Save();

        Debug.Log("关卡进度已重置到初始状态");
        Debug.Log($"重置后状态: L1={level1Completed}, L2={level2Completed}, L3={level3Completed}, Game={gameCompleted}");
    }

    // 手动保存进度（可选）
    public void ManualSave()
    {
        SaveProgress();
        Debug.Log("手动保存进度完成");
    }

    // 手动加载进度（可选）
    public void ManualLoad()
    {
        LoadProgress();
        Debug.Log("手动加载进度完成");
        OnProgressUpdated?.Invoke();
    }

    // 显示当前状态（调试用）
    public void DebugStatus()
    {
        Debug.Log($"当前状态: L1={level1Completed}, L2={level2Completed}, L3={level3Completed}, Game={gameCompleted}");
    }

    // 应用程序退出时清理
    void OnApplicationQuit()
    {
        // 可以选择在退出时重置标识，这样下次启动会再次重置进度
        hasInitialized = false;
        Debug.Log("应用程序退出，重置标识已清理");
    }
}