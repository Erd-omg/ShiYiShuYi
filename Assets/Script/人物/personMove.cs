using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class personMove : MonoBehaviour
{
    private Vector3 m_camRot;
    private Transform m_camTransform; 
    private Transform m_transform; 
    public float m_movSpeed = 10;

    private Animator anim;
    private Rigidbody rb;
    private bool isWalking = false;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        m_camTransform = Camera.main.transform;
        m_transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
   
    private void Update()
    {
        Control();
    }
    void Control()
    {
        if (Input.GetMouseButton(0))
        {
            float rh = Input.GetAxis("Mouse X"); 
            float rv = Input.GetAxis("Mouse Y");
        }
        
        float xm = 0, ym = 0, zm = 0;
        //按键盘W键向上移动
        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movSpeed * Time.deltaTime;
        }           
        else if (Input.GetKey(KeyCode.S))//S键下移动
        {
            zm -= m_movSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))//A键左移动
        {
            xm -= m_movSpeed * Time.deltaTime;
        }         
        else if (Input.GetKey(KeyCode.D))
        {
            xm += m_movSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && m_transform.position.y <= 3)
        {
            ym += m_movSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.F) && m_transform.position.y >= 1)
        {
            ym -= m_movSpeed * Time.deltaTime;
        }
        m_transform.Translate(new Vector3(xm, ym, zm),Space.Self);

        // 检查角色是否正在行走  
        isWalking = (xm != 0 || ym != 0 || zm != 0);

        // 设置动画状态  
        anim.SetBool("walk", isWalking);

        //if (Input.GetKey(KeyCode.W)|| Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        //{
        //    anim.SetBool("walk",true);
        //}
        //else
        //{
        //    anim.SetBool("walk", false);
        //}
    }


}
