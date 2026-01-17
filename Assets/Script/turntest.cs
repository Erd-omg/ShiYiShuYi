using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class turn : MonoBehaviour
{
    private GameObject one;
    private GameObject two;
    private GameObject three;

    private GameObject shadow1;
    private GameObject shadow2;
    private GameObject shadow3;

    private Vector3 screenPoint;
    private GameObject draggedObj=null;
    private bool isDragging = false;

    public float rotateSpeed=0.5f;

    void Start()
    {
        one = GameObject.Find("one");
        two = GameObject.Find("two");
        three = GameObject.Find("three");

        shadow1 = GameObject.Find("shadowOne");
        shadow2 = GameObject.Find("shadowTwo");
        shadow3 = GameObject.Find("shadowThree");
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // 将鼠标屏幕坐标转换为射线  
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                // 检查与射线相交的物体
                if (hit.collider.gameObject == shadow1) draggedObj = one;
                 
                if(hit.collider.gameObject == shadow2) draggedObj = two;
                
                if (hit.collider.gameObject == shadow3) draggedObj = three;

                // 记录开始拖拽时的屏幕点  
                if (draggedObj!=null)
                {
                    isDragging = true;
                    screenPoint=Input.mousePosition;
                }
            }
        }

        else if (Input.GetMouseButton(0) && isDragging)
        {
            float deltaX = Input.mousePosition.x - screenPoint.x;
            float rotationAngle = deltaX * Mathf.Deg2Rad * rotateSpeed;

            Quaternion rotation = Quaternion.AngleAxis(rotationAngle, Vector3.forward);

            // 设置物体的旋转
            if (draggedObj != null)
            {
                draggedObj.transform.rotation = draggedObj.transform.rotation * rotation;
            }

            screenPoint = Input.mousePosition;
        }

        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            draggedObj= null;
        }
    }
}
