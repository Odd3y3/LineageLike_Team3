using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : UIObject, IPointerClickHandler,IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    //������ ������ ������ ���� �����ϱ�
    public Item item; //ȹ���� ������


    //�κ��丮 ���ο� ������ ���� �߰�

    public void AddItem(Item _item)
    {
        item = _item;
        myImage.sprite = item.Sprite;
        SetColor(1);
    }

    // �ش� ���� �ϳ� ����

    public void ClearSlot()
    {
        item = null;
        myImage.sprite = null;
        SetColor(0);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Middle)
        {
            if (eventData.clickCount == 2)
            {
                ClearSlot();
            }
        }
    }

    //������ �巡�� �� ���
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            SetColor(0);
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
