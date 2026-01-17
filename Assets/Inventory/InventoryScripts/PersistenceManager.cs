using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistenceManager : MonoBehaviour
{
    public Inventory playerInventory;

    public static PersistenceManager instance;

    // 需要清空的物品列表
    public string[] itemsToClear = { }; 

    private void Awake()
    {
        // 实现单例模式，确保只有一个实例
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        // 防止该游戏对象在加载新场景时被销毁
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // 注册场景加载事件
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 取消注册场景加载事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "SuanChou" || scene.name == "Map" || scene.name=="FanPaiGame")
        {
            ClearSpecifiedItems(itemsToClear);
        }

        if(scene.name == "Garden")
        {
            ClearAllItems();
        }
    }

    private void ClearSpecifiedItems(string[] itemNames)
    {
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] != null)
            {
                // 检查当前物品是否在需要清空的列表中
                foreach (string name in itemNames)
                {
                    if (playerInventory.itemList[i].itemName == name)
                    {
                        playerInventory.itemList[i] = null;
                        break;
                    }
                }
            }
        }

        // 刷新 UI，但需要确保 InventoryManager 实例存在
        if (InventoryManager.instance != null)
        {
            InventoryManager.RefreshItem();
        }
    }

    private void ClearAllItems()
    {
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] != null)
            {
                playerInventory.itemList[i] = null;
            }
        }

        // 刷新 UI，但需要确保 InventoryManager 实例存在
        if (InventoryManager.instance != null)
        {
            InventoryManager.RefreshItem();
        }
    }


    public void AddItemToInventory(Item itemToAdd)
    {
        if (itemToAdd == null)
        {
            Debug.LogError("尝试添加空物品到背包！");
            return;
        }

        // 检查背包中是否已经有这个物品
        bool isFound = false;
        for (int i = 0; i < playerInventory.itemList.Count; i++)
        {
            if (playerInventory.itemList[i] == itemToAdd)
            {
                playerInventory.itemList[i].itemHeld += 1;
                isFound = true;
                break;
            }
        }

        // 如果没有找到，将物品添加到第一个空槽位
        if (!isFound)
        {
            for (int i = 0; i < playerInventory.itemList.Count; i++)
            {
                if (playerInventory.itemList[i] == null)
                {
                    playerInventory.itemList[i] = itemToAdd;
                    break;
                }
            }
        }
    }
}