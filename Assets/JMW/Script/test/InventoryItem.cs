using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "New Item/item")]
public class InventoryItem : ScriptableObject
{
    public enum ItemType
    {
        Equipment, //장비
        Used,  //소모품
        Ingredient, //재료
        ETC //기타
    }

    public string itemName;
    public ItemType itemType;
    public Sprite itemImage;
    public GameObject itemPrefab;

    public string weaponType;
}
