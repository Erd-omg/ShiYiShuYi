using UnityEngine;
using UnityEngine.UI;

public class ConditionalExitButton : MonoBehaviour
{
    [Header("需要检查的Image组件")]
    public Image[] requiredImages; // 前面三个按钮的Image组件

    private Button myButton;
    private Image myImage;

    void Start()
    {
        // 获取当前按钮的Image组件
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();

        // 初始状态设置为false
        SetButtonState(false);

        // 添加点击事件监听
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnExitButtonClick);
        }
    }

    void Update()
    {
        // 检查所有需要的Image是否都激活
        bool allActive = CheckAllRequiredImagesActive();

        // 如果所有需要的Image都激活，且当前按钮未激活，则激活它
        if (allActive && !myImage.gameObject.activeInHierarchy)
        {
            SetButtonState(true);
        }
        // 如果有任何一个需要的Image未激活，且当前按钮已激活，则禁用它
        else if (!allActive && myImage.gameObject.activeInHierarchy)
        {
            SetButtonState(false);
        }
    }

    // 检查所有需要的Image是否激活
    private bool CheckAllRequiredImagesActive()
    {
        foreach (Image img in requiredImages)
        {
            if (img != null && !img.gameObject.activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }

    // 设置按钮状态
    private void SetButtonState(bool state)
    {
        myImage.gameObject.SetActive(state);
        if (myButton != null)
        {
            myButton.interactable = state;
        }
    }

    // 退出游戏按钮点击事件
    public void OnExitButtonClick()
    {
        Debug.Log("退出游戏");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // 当脚本被禁用或对象被销毁时，移除事件监听
    private void OnDestroy()
    {
        if (myButton != null)
        {
            myButton.onClick.RemoveListener(OnExitButtonClick);
        }
    }
}