using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject 
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;
    [TextArea]//使一行文字变为文本框
    public string itemInfo;

    public bool equip;
}
