using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : UIObject<EquipmentSlot>,IPointerClickHandler, IDropHandler
{
    public Item myEquipment;

    [SerializeField]
    public Item.EQUIPMENTTYPE myEquipmentType = Item.EQUIPMENTTYPE.None;


    Color orgCol = Color.black;

    public void AddEquipment(Item _item)
    {
        myEquipment = _item;
        myImage.sprite = myEquipment.Sprite;
        GameManager.Inst.inGameManager.myPlayer.OnEquipItem(_item);
        SetColor(1,Color.white);
    }

    public void SetColor(float _alpha,Color defaulColor)
    {
        Color color = myImage.color;
        color = defaulColor;
        color.a = _alpha;
        myImage.color = color;
    }

    private void Unmount()
    {
        GameManager.Inst.UiManager.myInventory.AcquireItem(myEquipment);
        GameManager.Inst.inGameManager.myPlayer.OnUnmountITem(myEquipment);
        myEquipment = null;
        myImage.sprite = null;
        
        SetColor(1,orgCol);
    }
    
    public void ChangeItem(Item ChangeItem)
    {
        DragSlot.instance.dragInventorySlot.ClearSlot();
        Unmount();
        AddEquipment(ChangeItem);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (eventData.clickCount == 2)
            {
                Unmount();
            }
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (DragSlot.instance.dragInventorySlot != null)
        {
            Item EquipmentItem = DragSlot.instance.dragInventorySlot.item;
            if (EquipmentItem.EquipmentType == myEquipmentType && myEquipment == null)
            {
                GameManager.Inst.UiManager.myEquipment.EquipmentItem(EquipmentItem);
                DragSlot.instance.dragInventorySlot.ClearSlot();
            }
            else
            {
                if(EquipmentItem.EquipmentType != myEquipmentType)
                {
                    Debug.Log("잘못된 위치 입니다");
                }
                else
                {
                    ChangeItem(EquipmentItem);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
