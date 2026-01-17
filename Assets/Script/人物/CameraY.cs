using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraY : MonoBehaviour
{
    //控制左右旋转
    public Transform y_Axis;
    //控制上下旋转
    public Transform x_Axis;
    //控制左右倾斜
    public Transform z_Axis;
    //控制远近距离
    public Transform zoom_Axis;

    public Vector3 cameraPosition;

    //玩家对象
    public Transform player;

    //旋转速度
    public float roSpeed = 20;
    //缩放速度
    //public float scSpeed = 50;
    //限定角度
    public float limitAngle = 10;
    //鼠标左右滑动数值、滚轮数值
    private float hor, ver; 
    private float scrollView;
    float x = 0;
    //float sc = 10;
    //是否跟随玩家
    public bool followFlag;
    //是否控制玩家旋转
    public bool turnFlag;

    public void UpDate()
    {
        y_Axis.position = cameraPosition;
    }

    private void LateUpdate()
    {
        //输入获取
        hor = Input.GetAxis("Mouse X");
        ver = Input.GetAxis("Mouse Y");//鼠标滚轮数值
        scrollView = Input.GetAxis("Mouse ScrollWheel");

        if (!followFlag || !turnFlag)
        {
            //左右滑动鼠标
            if (hor != 0)
            {
                // 围绕Y轴旋转，Vector3.up是本地坐标的位置
                y_Axis.Rotate(Vector3.up * roSpeed * hor * Time.deltaTime);
            }

            //上下滑动鼠标
            if (ver != 0)
            {
                x += -ver * Time.deltaTime * roSpeed;
                x = Mathf.Clamp(x, -limitAngle, limitAngle);
                Quaternion q = Quaternion.identity;
                q = Quaternion.Euler(new Vector3(x, x_Axis.eulerAngles.y, x_Axis.eulerAngles.z));
                x_Axis.rotation = Quaternion.Lerp(x_Axis.rotation, q, Time.deltaTime * roSpeed);
            }

            ////缩放远近
            //if (scrollView != 0)
            //{
            //    sc -= scrollView * scSpeed;
            //    sc = Mathf.Clamp(sc, 3, 10);
            //    zoom_Axis.transform.localPosition = new Vector3(0, 0, -sc);
            //}

        }

            //跟随玩家
        if (followFlag && player != null)
        {
            y_Axis.position = Vector3.Lerp(y_Axis.position, player.position + Vector3.up, Time.deltaTime * 10f);
        }

        //旋转玩家
        if (turnFlag && player != null)
        {
            player.transform.forward = new Vector3(transform.forward.x, 0, transform.forward.z);
        }
            
    }







}
