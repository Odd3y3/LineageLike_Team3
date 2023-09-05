using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : UIObject<InventorySlot>, IPointerClickHandler,IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{

    //아이템 장착시 아이템 삭제 구현하기
    public Item item=null; //획득한 아이템

    public int itemCount; // 획득한 아이템의 개수

    public TMPro.TMP_Text myText=null;

    //인벤토리 새로운 아이템 슬롯 추가

    public void AddItem(Item _item, int _count = 1)
    {
        item = _item;
        itemCount = _count;
        myImage.sprite = item.Sprite;

        SetColor(1);

    }

    new public void SetColor(float _alpha)
    {
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;

        if (_alpha == 1&&itemCount>1)
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
        if (itemCount > item.Stack)
        {
            int lesscount = itemCount-item.Stack;
            itemCount = item.Stack;
            GameManager.Inst.UiManager.myInventory.AcquireItem(item,lesscount);
        }

        myText.text = itemCount.ToString();

        if (itemCount > 1)
        {
            myText.gameObject.SetActive(true);
            myText.text = itemCount.ToString();
        }

        if (itemCount <= 0)
            ClearSlot();
    }

    // 해당 슬롯 하나 삭제

    public void ClearSlot()
    {
        item = null;
        itemCount = 0;
        myImage.sprite = null;
        SetColor(0);

        myText.text = "0";
        myText.gameObject.SetActive(false);
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

    //아이템 드래그 앤 드롭
    public void OnBeginDrag(PointerEventData eventData)
    {
       
        if (item != null)
        {
            SetColor(0);
            DragSlot.instance.dragInventorySlot = this;
            DragSlot.instance.dragItemCount = this.itemCount;
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
        int _tempCount = itemCount;

        AddItem(DragSlot.instance.dragInventorySlot.item,DragSlot.instance.dragInventorySlot.itemCount);

        if (_tempItem != null)
            DragSlot.instance.dragInventorySlot.AddItem(_tempItem,_tempCount);
        else
            DragSlot.instance.dragInventorySlot.ClearSlot();
    }

    private void Awake()
    {
        SetColor(0);
    }

    void Start()
    {
       
    }


    void Update()
    {
       
    }
}
