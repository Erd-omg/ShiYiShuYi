using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDebugger : MonoBehaviour
{
    public Button targetButton;

    void Update()
    {
        if (targetButton != null)
        {
            Debug.Log($"按钮状态: interactable={targetButton.interactable}, enabled={targetButton.enabled}, active={targetButton.gameObject.activeInHierarchy}");

            // 检查是否有其他组件阻止交互
            CanvasGroup canvasGroup = targetButton.GetComponent<CanvasGroup>();
            if (canvasGroup != null)
            {
                Debug.Log($"CanvasGroup: alpha={canvasGroup.alpha}, interactable={canvasGroup.interactable}, blocksRaycasts={canvasGroup.blocksRaycasts}");
            }

            // 检查Raycast遮挡
            Image image = targetButton.GetComponent<Image>();
            if (image != null)
            {
                Debug.Log($"Image: raycastTarget={image.raycastTarget}");
            }
        }
    }

    // 手动测试按钮点击
    public void TestButtonClick()
    {
        if (targetButton != null && targetButton.interactable)
        {
            Debug.Log("手动触发按钮点击");
            targetButton.onClick.Invoke();
        }
        else
        {
            Debug.Log("按钮不可点击");
        }
    }
}