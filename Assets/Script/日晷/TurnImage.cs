using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurnImage : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Image imageToRotateOne; // 需要旋转的Image组件
    public float rotateSpeed = 0.5f; // 旋转速度  

    private float currentRotation = 90f; // 当前旋转角度  

    private Vector2 dragStartPosition; // 拖拽开始时的鼠标位置

    private float currentAngle; // 实时更新的当前角度

    private const float CLOSE_TO_90_DEGREES = 5f; // 定义接近90度的阈值  
    public bool image1Reached90 = false;
    private Color originalColor; // 存储原始颜色
    private bool isGreen = false; // 标记图片是否当前为绿色  
    private float greenDuration = 2f; // 图片保持绿色的时长  
    private float greenStartTime=0f; // 图片开始变绿的时间
    private Color lightGreen = new Color(0.7f, 0.9f, 0.7f); // 浅绿色                              

    private bool canDrag = true;

    /*
    //精灵（排除透明区域的影响）
    public Sprite sprite;
    public float circleRadius;
    public GameObject circle;
    private Transform circleTransform;
    private CircleCollider2D circleCollider;
    */

    private void Start()
    {
        //// 确保imageToRotate被赋值  
        if (imageToRotateOne == null)
        {
            //Debug.LogError("DragRotateImage: imageToRotate is not assigned in the inspector.");
            enabled = false;
            return;
        }
        //circleTransform=circle.transform;
        //circleCollider=circleTransform.GetComponent<CircleCollider2D>();
        originalColor = imageToRotateOne.color; // 存储原始颜色  

        currentAngle = imageToRotateOne.rectTransform.localEulerAngles.z;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        // 获取点击的世界坐标
        Vector2 localPoint;
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToRotateOne.rectTransform, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            return;
        }

        // 将本地坐标转换为UV坐标 (0-1)
        float u = (localPoint.x - imageToRotateOne.rectTransform.rect.x) / imageToRotateOne.rectTransform.rect.width;
        float v = (localPoint.y - imageToRotateOne.rectTransform.rect.y) / imageToRotateOne.rectTransform.rect.height;

        // 将UV坐标转换为像素坐标
        Sprite sprite = imageToRotateOne.sprite;
        Texture2D texture = sprite.texture;
        int x = Mathf.FloorToInt(u * texture.width);
        int y = Mathf.FloorToInt(v * texture.height);

        // 检查点击位置的像素透明度
        if (texture.GetPixel(x, y).a < 0.1f) // 0.1f 是透明度阈值，你可以根据需要调整
        {
            // 如果像素是透明的，则不执行任何操作，直接返回
            return;
        }

        // 如果像素不透明，记录拖拽开始位置并开始拖拽  
        dragStartPosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!canDrag)
        {
            return;
        }

        // 计算拖拽的距离  
        Vector2 delta = eventData.position - dragStartPosition;

        // 根据拖拽距离计算旋转角度  
        float deltaAngle = delta.x * rotateSpeed * Mathf.Deg2Rad;
        currentRotation += deltaAngle;

        // 限制旋转角度在 0 到 360 度之间  
        currentRotation = Mathf.Repeat(currentRotation, 360f * Mathf.Deg2Rad);

        // 应用旋转到图片  
        Quaternion rotationQuaternion = Quaternion.AngleAxis(currentRotation * Mathf.Rad2Deg, Vector3.forward);
        imageToRotateOne.rectTransform.rotation = rotationQuaternion;

        // 更新拖拽开始位置为当前位置，用于下一帧的拖拽计算  
        dragStartPosition = eventData.position;

        if (Mathf.Abs(currentRotation * Mathf.Rad2Deg - 0f) <= CLOSE_TO_90_DEGREES)
        {
            image1Reached90 = true;
        }

        // 检查是否接近90度  
        if (Mathf.Abs(currentRotation * Mathf.Rad2Deg - 0f) <= CLOSE_TO_90_DEGREES)
        {
            // 如果图片不是绿色，则开始变绿  
            if (!isGreen)
            {
                isGreen = true;
                greenStartTime = Time.time;
                imageToRotateOne.color = lightGreen; // 设置图片为绿色  
                canDrag = false;
            }
            // 检查是否过了绿色持续时间，如果是，则恢复原色  
            if (Time.time - greenStartTime > greenDuration)
            {
                isGreen = false;
                imageToRotateOne.color = originalColor; // 恢复图片为原色  
            }
        }
        else
        {
            // 如果图片是绿色但当前角度不接近90度，立即恢复原色  
            if (isGreen)
            {
                isGreen = false;
                imageToRotateOne.color = originalColor;
            }
        }


        //Vector2 currentPosition = eventData.position;
        ////将屏幕坐标转化为世界坐标
        //Vector2 worldPoint=Camera.main.ScreenToWorldPoint(currentPosition);

        //bool isInsideCollider = Physics2D.OverlapCircle(worldPoint, circleRadius, LayerMask.GetMask("RiGuiPart"));   

        //if (isInsideCollider)
        //{
        //    // 如果在Collider内，执行你的逻辑，比如旋转图片等  
        //    Debug.Log("Dragging inside the circle!");
        //    // ...你的旋转代码...  
        //}
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // 拖拽结束时的处理

    }
}





