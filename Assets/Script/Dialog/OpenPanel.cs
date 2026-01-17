using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class OpenPanel : MonoBehaviour
{
    public GameObject panel;
    public GraphicRaycaster raycaster;

    private void Start()
    {
    }

    void Update()
    {
        if(this.enabled && Input.GetMouseButtonDown(0))
        {
            if (raycaster != null)
            {
                // 创建一个指针事件数据，用于射线检测
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = Input.mousePosition;

                // 创建一个列表来存储射线检测到的结果
                List<RaycastResult> results = new List<RaycastResult>();

                // 对当前鼠标位置进行射线检测
                raycaster.Raycast(eventData, results);

                // 检查射线检测的结果
                foreach (RaycastResult result in results)
                {
                    // 如果点击到其他UI元素，则不触发
                    if (result.gameObject.CompareTag("Notrigger"))
                    {
                        Debug.Log("点击了其他按钮，不触发对话。");
                        return; // 立即返回，不执行下面的代码
                    }
                }
            }

            panel.SetActive(true);
            this.gameObject.SetActive(false);
            //Text.text=null;
        }
    }
}
