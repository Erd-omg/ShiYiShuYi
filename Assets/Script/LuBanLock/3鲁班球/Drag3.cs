using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag3 : MonoBehaviour
{
    //拖拽
    private Vector3 screenPoint;
    public Vector3 offset;
    private float zCoord;
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

    private void HandleDragRelease()
    {
        //实物显示，提示消失，透明消失，零件消失
        if (triggerCheck3_1.instance.IsEnterTrans)
        {
            triggerCheck3_1.instance.shiwu.SetActive(true);
            triggerCheck3_1.instance.transparency.SetActive(false);
            triggerCheck3_1.instance.right.SetActive(false);
            triggerCheck3_1.instance.wrong.SetActive(false);
            triggerCheck3_1.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (triggerCheck3_2.instance.IsEnterTrans)
        {
            triggerCheck3_2.instance.shiwu.SetActive(true);
            triggerCheck3_2.instance.transparency.SetActive(false);
            triggerCheck3_2.instance.right.SetActive(false);
            triggerCheck3_2.instance.wrong.SetActive(false);
            triggerCheck3_2.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (triggerCheck3_3.instance.IsEnterTrans)
        {
            triggerCheck3_3.instance.shiwu.SetActive(true);
            triggerCheck3_3.instance.transparency.SetActive(false);
            triggerCheck3_3.instance.right.SetActive(false);
            triggerCheck3_3.instance.wrong.SetActive(false);
            triggerCheck3_3.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (triggerCheck3_4.instance.IsEnterTrans)
        {
            triggerCheck3_4.instance.shiwu.SetActive(true);
            triggerCheck3_4.instance.transparency.SetActive(false);
            triggerCheck3_4.instance.right.SetActive(false);
            triggerCheck3_4.instance.wrong.SetActive(false);
            triggerCheck3_4.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }

        if (triggerCheck3_5.instance.IsEnterTrans)
        {
            triggerCheck3_5.instance.shiwu.SetActive(true);
            triggerCheck3_5.instance.transparency.SetActive(false);
            triggerCheck3_5.instance.right.SetActive(false);
            triggerCheck3_5.instance.wrong.SetActive(false);
            triggerCheck3_5.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }
        if (triggerCheck3_6.instance.IsEnterTrans)
        {
            triggerCheck3_6.instance.shiwu.SetActive(true);
            triggerCheck3_6.instance.transparency.SetActive(false);
            triggerCheck3_6.instance.right.SetActive(false);
            triggerCheck3_6.instance.wrong.SetActive(false);
            triggerCheck3_6.instance.part.SetActive(false);
        }
        else
        {
            transform.position = targetPosition;
        }
    }
}
