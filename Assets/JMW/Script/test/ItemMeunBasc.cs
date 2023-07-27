using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMeunBasc : MonoBehaviour
{
    public List<Item> items;

    [SerializeField]
    private Transform slotitemParent;
    [SerializeField]
    private SlotItem[] slotitems;

    private void OnValidate()
    {
        slotitems = slotitemParent.GetComponentsInChildren<SlotItem>();
    }

    private void Awake()
    {
        FreshSlot();
    }

    public void FreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < slotitems.Length; i++)
        {
            slotitems[i].item = items[i];
        }
        for (; i < slotitems.Length; i++)
        {
            slotitems[i].item = null;
        }
    }

    public void AddItem(Item _item)
    {
        if (items.Count < slotitems.Length)
        {
            items.Add(_item);
            FreshSlot();
        }
        else
        {
            print("½½·ÔÀÌ °¡µæ Â÷ ÀÖ½À´Ï´Ù.");
        }

    }
}
