using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public Item thisItem;
    //public Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddNewItem();
            Destroy(gameObject);
        }
    }


    public void AddNewItem()
    {
        Inventory playerInventory = PersistenceManager.instance.playerInventory;
        
        // 检查背包中是否已经有这个物品
        if (playerInventory.itemList.Contains(thisItem))
        {
            // 如果有，找到这个物品并增加数量
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == thisItem)
                {
                    playerInventory.itemList[i].itemHeld += 1;
                    break;
                }
            }
        }
        else
        {
            // 如果没有，找到第一个空槽位并添加
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = thisItem;
                    // 由于新拾取的物品数量默认为1，这里不需要再 itemHeld += 1
                    break;
                }
            }
        }

        InventoryManager.RefreshItem();
    }
}
