using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : UIObject, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    //아이템 장착시 아이템 삭제 구현하기
    public Item item; //획득한 아이템


    //인벤토리 새로운 아이템 슬롯 추가

    public void AddItem(Item _item)
    {
        item = _item;
        myImage.sprite = item.Sprite;
        SetColor(1);
    }

    // 해당 슬롯 하나 삭제

    public void ClearSlot()
    {
        item = null;
        myImage.sprite = null;
        SetColor(0);
    }

    //아이템 드래그 앤 드롭
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            
            DragSlot.instance.dragInventorySlot = this;
            DragSlot.instance.DragSetImage(item.Sprite);
            DragSlot.instance.transform.position = eventData.position;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (item != null) DragSlot.instance.transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragInventorySlot = null;
        if(item != null)
        {
            SetColor(1);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragInventorySlot != null)
            ChangeSlot();
    }

    private void ChangeSlot()
    {
        Item _tempItem = item;

        AddItem(DragSlot.instance.dragInventorySlot.item);

        if (_tempItem != null)
            DragSlot.instance.dragInventorySlot.AddItem(_tempItem);
        else
            DragSlot.instance.dragInventorySlot.ClearSlot();
    }

    void Start()
    {
        SetColor(0);
    }


    void Update()
    {

    }
}
