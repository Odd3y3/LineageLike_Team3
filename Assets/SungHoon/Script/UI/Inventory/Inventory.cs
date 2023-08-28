using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : UIProperty
{
    public static bool invectoryActivated = false;

    [SerializeField]
    private GameObject go_InventoryBase;
    [SerializeField]
    private GameObject go_SlotsParent;
    [SerializeField]
    public InventorySlot[] slots;

    private void Awake()
    {
        slots = myAllInventorySlots;
        CloseInventory();
    }

    void Start()
    {
        
    }

    void Update()
    {
        TryOpenInventory();
    }

    private void TryOpenInventory()
    {
        if (Input.GetKeyDown(KeyCode.I) && !MenuUI.GameIsPaused)
        {
            invectoryActivated = !invectoryActivated;

            if (invectoryActivated)
            {
                OpenInventory();
            }
            else
            {
                CloseInventory();
            } 
        }
        
    }


    private void SetSlotUpdate(int a)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item != null)
            {
                slot.SetColor(1);
            }
        }
    }

    private void OpenInventory()
    {
        go_InventoryBase.SetActive(true);
        
    }

    private void CloseInventory()
    {
        go_InventoryBase.SetActive(false);
    }

    public void AcquireItem(Item _item, int _count = 1)
    {
        if (Item.ITEMTYPE.Equipment != _item.ItemType )
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null&&slots[i].itemCount<slots[i].item.Stack)
                {
                      slots[i].SetSlotCount(_count);
                      return;
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].item == null)
            {
                slots[i].AddItem(_item,_count);
                return;
            }
        }
    }
}