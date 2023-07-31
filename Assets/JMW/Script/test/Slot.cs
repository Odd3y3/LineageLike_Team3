using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : UIProperty, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public InventoryItem item; //ȹ���� ������

    //������ ���� ����

    

    private void SetColor(float _alpha)
    {
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;
    }

    //�κ��丮 ���ο� ������ ���� �߰�

    public void AddItem(InventoryItem _item)
    {
        item = _item;
        myImage.sprite = item.itemImage;
        SetColor(1);
    }

    // �ش� ���� �ϳ� ����

    private void ClearSlot()
    {
        item = null;
        myImage.sprite = null;
        SetColor(0);
    }

    //������ �巡�� �� ���

    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //    if (item != null)
        //    {
        //        DragSlot.instance.dragSlot = this;
        //        DragSlot.instance.DragSetImage(itemImage);
        //        DragSlot.instance.transform.position = eventData.position;
        //    }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //    if (item != null)
        //        DragSlot.instance.transform.position = eventData.position;
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //    DragSlot.instance.SetColor(0);
        //    DragSlot.instance.dragSlot = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //    if (DragSlot.instance.dragSlot != null)
        //        ClearSlot();
    }

    private void ChangeSlot()
    {
        //    Item _tempItem = item;
        //    int _tempItemCount = itemCount;

        //    AddItem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        //    if (_tempItem != null)
        //        DragSlot.instance.dragSlot.AddItem(_tempItem, _tempItemCount);
        //    else
        //        DragSlot.instance.dragSlot.ClearSlot();
    }

    void Start()
    {
        SetColor(0);
    }


    void Update()
    {

    }
}
