using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemOnDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform originalParent;
    public Inventory myBag;
    private int currentItemID;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;
        currentItemID = originalParent.GetComponent<Slot>().slotID;
        transform.SetParent(transform.parent.parent);
        transform.position = eventData.position;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log(eventData.pointerCurrentRaycast.gameObject.name);
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        // 默认为归位
        Transform returnParent = originalParent;
        Vector3 returnPosition = originalParent.position;

        if (eventData.pointerCurrentRaycast.gameObject != null)
        {
            // 如果拖拽到另一个物品图片上（交换）
            if (eventData.pointerCurrentRaycast.gameObject.name == "ItemImage")
            {
                Transform targetParent = eventData.pointerCurrentRaycast.gameObject.transform.parent.parent;
                int targetID = targetParent.GetComponent<Slot>().slotID;

                // 交换 itemList 中的物品
                var tempItem = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = myBag.itemList[targetID];
                myBag.itemList[targetID] = tempItem;

                // 交换 UI
                transform.SetParent(targetParent);
                transform.position = targetParent.position;
                eventData.pointerCurrentRaycast.gameObject.transform.parent.SetParent(originalParent);
                eventData.pointerCurrentRaycast.gameObject.transform.parent.position = originalParent.position;

                returnParent = null; // 成功交换，无需归位
            }
            // 如果拖拽到空槽位上（移动）
            else if (eventData.pointerCurrentRaycast.gameObject.name == "slot(Clone)")
            {
                Transform targetParent = eventData.pointerCurrentRaycast.gameObject.transform;
                int targetID = targetParent.GetComponent<Slot>().slotID;

                // 移动 itemList 中的物品
                myBag.itemList[targetID] = myBag.itemList[currentItemID];
                myBag.itemList[currentItemID] = null;

                // 移动 UI
                transform.SetParent(targetParent);
                transform.position = targetParent.position;

                returnParent = null; // 成功移动，无需归位
            }
        }

        // 如果 returnParent 仍然不为 null，说明拖拽失败，归位
        if (returnParent != null)
        {
            transform.SetParent(returnParent);
            transform.position = returnPosition;
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
        InventoryManager.RefreshItem();
    }
}