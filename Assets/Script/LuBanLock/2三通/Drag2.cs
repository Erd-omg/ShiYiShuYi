using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag2 : MonoBehaviour
{
    //拖拽
    private float zCoord;
    private Vector3 offset;
    private bool isDragging = false;

    //原位置
    private Vector3 targetPosition = new Vector3(-125, 40, 300);

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标屏幕坐标转换为射线  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 检查射线是否与物体相交  
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    // 记录物体z坐标到相机的距离，作为拖拽的平面深度
                    zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

                    // 计算鼠标点击点与物体中心位置的偏移
                    offset = transform.position - GetMouseWorldPos();

                    isDragging = true;
                }
            }
        }

        else if (Input.GetMouseButton(0) && isDragging)
        {
            transform.position = GetMouseWorldPos() + offset;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            HandleDragRelease();
        }
    }

    /// <summary>
    /// 获取当前鼠标在3D世界中的位置
    /// </summary>
    private Vector3 GetMouseWorldPos()
    {
        // 获取当前鼠标的屏幕坐标
        Vector3 mousePoint = Input.mousePosition;

        // 设定鼠标的z坐标为我们记录的深度
        mousePoint.z = zCoord;

        // 将屏幕坐标转换为世界坐标
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    /// <summary>
    /// 处理鼠标松开时的逻辑
    /// </summary>
    private void HandleDragRelease()
    {
        //实物显示，提示消失，透明消失，零件消失
        if (triggerCheck2_1.instance.IsEnterTrans)
        {
            triggerCheck2_1.instance.shiwu.SetActive(true);
            triggerCheck2_1.instance.transparency.SetActive(false);
            triggerCheck2_1.instance.right.SetActive(false);
            triggerCheck2_1.instance.wrong.SetActive(false);
            triggerCheck2_1.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (triggerCheck2_2.instance.IsEnterTrans)
        {
            triggerCheck2_2.instance.shiwu.SetActive(true);
            triggerCheck2_2.instance.transparency.SetActive(false);
            triggerCheck2_2.instance.right.SetActive(false);
            triggerCheck2_2.instance.wrong.SetActive(false);
            triggerCheck2_2.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (triggerCheck2_3.instance.IsEnterTrans)
        {
            triggerCheck2_3.instance.shiwu.SetActive(true);
            triggerCheck2_3.instance.transparency.SetActive(false);
            triggerCheck2_3.instance.right.SetActive(false);
            triggerCheck2_3.instance.wrong.SetActive(false);
            triggerCheck2_3.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}
