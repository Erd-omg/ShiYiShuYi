using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndingController : MonoBehaviour
{
    private float countTime = 0f;
    public float videoTime;//视频时长
    public RenderTexture rt;

    // 新增：按钮延迟显示时间（秒）
    public float skipButtonDelay = 3f;

    public GameObject rawimage;

    //视频播放完后显示的面板
    public GameObject NextPanel;
    public bool changeScene = false;
    public string SceneName;

    //跳过按钮引用
    public Button skipButton;

    bool isChanged = false;
    // 新增：标记按钮是否已显示
    bool isSkipButtonShown = false;

    private void Awake()
    {
        rt.Release();
    }

    void Start()
    {
        // 确保MapManager存在并标记第三关完成
        MapManager.EnsureInstanceExists();
        if (MapManager.Instance != null && !MapManager.Instance.level3Completed)
        {
            MapManager.Instance.CompleteLevel(3);
            Debug.Log("在结尾场景中标记第三关完成");
        }

        if (NextPanel != null)
            NextPanel.SetActive(false);

        // 初始隐藏跳过按钮
        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
            skipButton.onClick.AddListener(OnSkipButtonClicked);
        }
    }

    void Update()
    {
        if (rawimage.activeSelf && !isChanged)
        {
            countTime += Time.deltaTime;

            // 视频播放达到延迟时间后显示跳过按钮
            if (!isSkipButtonShown && countTime >= skipButtonDelay && skipButton != null)
            {
                skipButton.gameObject.SetActive(true);
                isSkipButtonShown = true;
            }

            // 视频播放完成
            if (countTime > videoTime)
            {
                CompleteVideoPlayback();
            }
        }
    }

    // 跳过按钮点击事件
    public void OnSkipButtonClicked()
    {
        if (rawimage.activeSelf && !isChanged)
        {
            CompleteVideoPlayback();
        }
    }

    // 在CompleteVideoPlayback方法中添加：
    private void CompleteVideoPlayback()
    {
        rt.Release();
        if (skipButton != null)
        {
            skipButton.gameObject.SetActive(false);
        }

        if (changeScene)
        {
            // 确保MapManager存在
            MapManager.EnsureInstanceExists();

            // 通知MapManager游戏完成
            if (MapManager.Instance != null)
            {
                MapManager.Instance.CompleteGame();
            }

            // 确保状态保存完成后再加载场景
            StartCoroutine(LoadSceneAfterDelay(0.1f));
        }
        else
        {
            rawimage.SetActive(false);
            if (NextPanel != null)
            {
                NextPanel.SetActive(true);
                isChanged = true;
            }
        }
    }

    IEnumerator LoadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneName);
    }
}