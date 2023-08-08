using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumptionItemSlot : UIObject,IDropHandler
{
    public Item consumptionItem;

    public int myIndex=0;

    public Sprite orgImg= null;

    public void AddConsumption(Item _item)
    {
        consumptionItem = _item;
        myImage.sprite = consumptionItem.Sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha,Sprite changeImgage)
    {
        myImage.sprite = changeImgage;
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearSlot()
    {
        consumptionItem = null;
        myImage.sprite = null;
        SetColor(1,orgImg);
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragInventorySlot != null)
        {
            Item ConsumptionItem = DragSlot.instance.dragInventorySlot.item;
            if (ConsumptionItem.ItemType == Item.ITEMTYPE.Equipment) 
            {
                Debug.Log("잘못된 아이템입니다");
                return;
            }
            GameManager.Inst.UiManager.myConsumptionItem.ConsumptionItems(ConsumptionItem,myIndex);
            DragSlot.instance.dragInventorySlot.ClearSlot();
        }
    }
}
