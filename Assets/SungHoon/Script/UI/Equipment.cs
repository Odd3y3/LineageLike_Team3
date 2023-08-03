using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment :UIProperty 
{
    public static bool stateActivated = false;
    [SerializeField]
    private GameObject go_ststeBase;
    [SerializeField]
    public EquipmentSlot[] slots;
    void Start()
    {
        slots = myAllEquipmentSlots;
    }

    void Update()
    {
        TryOpenState();
    }

    private void TryOpenState()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            stateActivated = !stateActivated;

            if (stateActivated)
                OpenState();
            else
                CloseStare();
        }
    }

    private void OpenState()
    {
        go_ststeBase.SetActive(true);
    }

    private void CloseStare()
    {
        go_ststeBase.SetActive(false);
    }

   



    public void EquipmentItem(Item _item)
    {
        if (Item.ITEMTYPE.Equipment != _item.ItemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].Equipment != null)
                {
                    if (slots[i].Equipment.Name == _item.Name)
                    {
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].Equipment == null)
            {
                if (slots[i].myEquipmentType == _item.EquipmentType)
                {
                    slots[i].AddEquipment(_item);
                    return;
                }
            }
        }
    }
}
