using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionItem : UIObject<ConsumptionItemSlot>
{
    public ConsumptionItemSlot[] slots;
    private void Awake()
    {
        slots = myAllSlots;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MenuUI.GameIsPaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                OnUseItem(slots[0].consumptionItem);
                slots[0].SetSlotCount(-1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                OnUseItem(slots[1].consumptionItem);
                slots[1].SetSlotCount(-1);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                OnUseItem(slots[2].consumptionItem);
                slots[2].SetSlotCount(-1);
            }
        }
    }

    public void OnUseItem(Item item)
    {
        if (item != null)
        {
            switch (item.ItemType)
            {
                case Item.ITEMTYPE.Potion:
                    GameManager.Inst.inGameManager.myPlayer.OnUsePotion(item);

                    break;
                case Item.ITEMTYPE.BattlePotion:
                    //배틀효과 여기 구현
                    break;
                default:
                    break;
            }
        }
        
    }

    public void ConsumptionItems(Item _item,int _itemCount,int index)
    {
        if (Item.ITEMTYPE.Equipment != _item.ItemType)
        {
            slots[index].AddConsumption(_item,_itemCount);
            return;
        }
    }
}
