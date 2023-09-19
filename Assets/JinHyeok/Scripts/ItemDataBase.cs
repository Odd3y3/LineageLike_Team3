using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataBase : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public Item FindItem(int id)
    {
        foreach (Item item in items)
        {
            if(item.itemID == id) return item;
        }

        return null;
    }
}
