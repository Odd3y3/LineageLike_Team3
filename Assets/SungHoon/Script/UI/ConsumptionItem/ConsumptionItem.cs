using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionItem : UIObject
{
    public ConsumptionItemSlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = myAllConsumptionSlots;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            OnUseItem(slots[0].consumptionItem);
            slots[0].ClearSlot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            slots[1].ClearSlot();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            slots[2].ClearSlot();
        }
    }

    public void OnUseItem(Item item)
    {
        switch (item.ItemType)
        {
            case Item.ITEMTYPE.Potion:
                    GameManager.Inst.myPlayer.OnUsePotion(item);
                break;
            case Item.ITEMTYPE.BattlePotion:
                //배틀효과 여기 구현
                break;
            default:
                break;
        }
    }

    public void ConsumptionItems(Item _item,int index)
    {
        if (Item.ITEMTYPE.Equipment != _item.ItemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].consumptionItem != null)
                {
                    if (slots[i].consumptionItem.Name == _item.Name)
                    {
                        return;
                    }
                }
            }
        }
        slots[index].AddConsumption(_item);
        return;

    }

}
