using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotID;//ŒÔ∆∑ID
    public Item SlotItem;
    public Image SlotImage;
    public TMP_Text SlotNum;
    public string slotInfo;

    public GameObject itemInSlot;

    public void ItemOnClicked()
    {
        InventoryManager.UpdateItemInfo(slotInfo,SlotImage.sprite);
    }

    public void SetupSlot(Item item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        SlotImage.sprite = item.itemImage;
        SlotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }
}
