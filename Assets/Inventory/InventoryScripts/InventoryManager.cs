using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Inventory myBag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public TMP_Text itemInformation;
    public Image itemImage;

    public GameObject nextObj;
    public List<string> sticksItem;
    private bool hasAllItems=false;

    public List<GameObject> slots = new List<GameObject>();
    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        if (PersistenceManager.instance != null)
        {
            myBag = PersistenceManager.instance.playerInventory;
        }
        RefreshItem();
        instance.itemInformation.text = "";
        instance.itemImage.enabled=false;
    }

    public static void UpdateItemInfo(string itemDescription,Sprite itemImage)
    {
        instance.itemInformation.text = itemDescription;
        instance.itemImage.enabled=true;
        instance.itemImage.sprite = itemImage;
    }

   

    public static void RefreshItem()
    {
        if (instance == null) return;

        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            //CreateNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform,false);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
        }

        CheckAndShowButton();
    }

    // 检查背包中是否同时拥有所有指定物品
    public static void CheckAndShowButton()
    {
        if (instance.myBag.itemList.Count < instance.sticksItem.Count)
        {
            instance.nextObj.gameObject.SetActive(false);
            return;
        }

        // 遍历需要的物品列表，检查背包中是否都存在
        int foundCount = 0;
        foreach (string itemName in instance.sticksItem)
        {
            foreach (Item item in instance.myBag.itemList)
            {
                if (item != null && item.itemName == itemName)
                {
                    foundCount++;
                    break;
                }
            }
        }
        if (instance.nextObj != null)
        {
            // 如果找到的物品数量等于需要的物品数量，则显示按钮
            if (foundCount == instance.sticksItem.Count)
            {
                instance.nextObj.gameObject.SetActive(true);
                instance.hasAllItems = true;
            }
            else
            {
                instance.nextObj.gameObject.SetActive(false);
                instance.hasAllItems = false;
            }
        }
    }

    /*
    public void OnJumpButtonClick()
    {
        if (!hasAllItems)
        {
            Debug.Log("未集齐所有物品，无法跳转！");
            return;
        }

        // 清空指定的物品
        ClearSticks();

        // 加载下一个场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    */

    private void ClearSticks()
    {
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            if (instance.myBag.itemList[i] != null)
            {
                // 检查当前物品是否在需要清空的列表中
                if (instance.sticksItem.Contains(instance.myBag.itemList[i].itemName))
                {
                    instance.myBag.itemList[i] = null; // 清空该槽位
                }
            }
        }
    }

    //public static void CreateNewItem(Item item)
    //{
    //    Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity); 
    //    newItem.gameObject.transform.SetParent(instance.slotGrid.transform); 
    //    newItem.SlotItem = item;
    //    newItem.SlotImage.sprite = item.itemImage;
    //    newItem.SlotNum.text = item.itemHeld.ToString();
    //}
}


