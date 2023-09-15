using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[System.Serializable]
public struct EquipmentData
{
    public Item Equipment;
    public Item.EQUIPMENTTYPE EquipmentType;
}


public class EquipmentSlot : UIObject<EquipmentSlot>,IPointerClickHandler, IDropHandler
{
    public Item myEquipment;

    [SerializeField]
    public Item.EQUIPMENTTYPE myEquipmentType = Item.EQUIPMENTTYPE.None;


    EquipmentData myData;

    Color orgCol = Color.black;

    public void AddEquipment(Item _item)
    {
        myEquipment = _item;
        if (myEquipment == null) return;
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

    public void UpdateData()
    {
        myData.Equipment = myEquipment;
        myData.EquipmentType = myEquipmentType;
    }

    public EquipmentData GetData()
    {
        return myData;
    }

    public void SetData(EquipmentData data)
    {
        myData = data;
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        AddEquipment(myData.Equipment);
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
