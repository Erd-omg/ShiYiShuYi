using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // 引入 List

public class AddAward : MonoBehaviour
{
    public List<Item> itemsToAward;

    public void addAwards()
    {
        if (PersistenceManager.instance != null)
        {
            // 遍历列表，依次添加每个物品
            foreach (Item item in itemsToAward)
            {
                PersistenceManager.instance.AddItemToInventory(item);
            }
        }
        else
        {
            Debug.LogError("GameManager 实例未找到！");
        }
    }
}
