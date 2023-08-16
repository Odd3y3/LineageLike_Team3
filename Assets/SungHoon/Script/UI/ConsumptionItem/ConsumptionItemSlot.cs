using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConsumptionItemSlot : UIObject,IDropHandler, IPointerClickHandler
{
    public Item consumptionItem;

    public int myIndex=0;

    public Sprite orgImg= null;

    public TMPro.TMP_Text myText = null;

    public int itemCount = 0;

    public void AddConsumption(Item _item,int _itemCount)
    {
        consumptionItem = _item;
        itemCount = _itemCount;
        myImage.sprite = consumptionItem.Sprite;
        SetColor(1);
    }

    public void SetColor(float _alpha,Sprite changeImgage)
    {
        myImage.sprite = changeImgage;
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;
        OnText();
    }
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnText()
    {
        if (itemCount > 1)
        {
            myText.gameObject.SetActive(true);
            myText.text = itemCount.ToString();
        }
        else
        {
            myText.gameObject.SetActive(false);
            myText.text = itemCount.ToString();
        }
    }

    public void SetSlotCount(int _count)
    {
        itemCount += _count;
        myText.text = itemCount.ToString();

        if (itemCount > 1)
        {
            myText.gameObject.SetActive(true);
            myText.text = itemCount.ToString();
        }

        if (itemCount <= 0)
            ClearSlot();
    }

    public void ClearSlot()
    {
        consumptionItem = null;
        myImage.sprite = null;
        itemCount = 0;
        SetColor(1,orgImg);
        OnText();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (eventData.clickCount == 2)
            {
                GameManager.Inst.UiManager.myInventory.AcquireItem(consumptionItem, itemCount);
                ClearSlot();
            }
        }
    }


    public void OnDrop(PointerEventData eventData)
    {
        Item ConsumptionItem = DragSlot.instance.dragInventorySlot.item;
        int ConsumptionItemCount = DragSlot.instance.dragInventorySlot.itemCount;
        if (DragSlot.instance.dragInventorySlot != null)
        {
            if (consumptionItem == null)
            {
                GameManager.Inst.UiManager.myConsumptionItem.ConsumptionItems(ConsumptionItem, ConsumptionItemCount, myIndex);
                DragSlot.instance.dragInventorySlot.ClearSlot();
            }
            else
            {
                int resetcount = ConsumptionItemCount + itemCount;
                OnResetCount(resetcount,ConsumptionItemCount);
            }
            
        }
        OnText();
    }

    public void OnResetCount(int resetcount,int consumptionItemCount)
    {
        int lesscount = resetcount - consumptionItem.Stack;
        if (lesscount < 0)
        {
            DragSlot.instance.dragInventorySlot.ClearSlot();
            GameManager.Inst.UiManager.myConsumptionItem.slots[myIndex].SetSlotCount(consumptionItemCount);
        }
        else
        {
            DragSlot.instance.dragInventorySlot.itemCount = 0;
            DragSlot.instance.dragInventorySlot.SetSlotCount(lesscount);
            GameManager.Inst.UiManager.myConsumptionItem.slots[myIndex].itemCount=consumptionItem.Stack;
        }
    }
}
