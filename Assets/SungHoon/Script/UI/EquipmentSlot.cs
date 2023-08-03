using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : UIObject,IPointerClickHandler, IDropHandler
{
    public Item Equipment;

    [SerializeField]
    public Item.EQUIPMENTTYPE myEquipmentType = Item.EQUIPMENTTYPE.None;

    Color orgCol = Color.black;

    public void AddEquipment(Item _item)
    {
        Equipment = _item;
        myImage.sprite = Equipment.Sprite;
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
        GameManager.Inst.UiManager.myInventory.AcquireItem(Equipment);
        Equipment = null;
        myImage.sprite = null;
        SetColor(1,orgCol);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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
        Item Equipment = DragSlot.instance.dragInventorySlot.item;
        if(Equipment.EquipmentType == myEquipmentType)
        {
            GameManager.Inst.UiManager.myEquipment.EquipmentItem(Equipment);
        }
        else
        {
            Debug.Log("잘못된 위치 입니다");
        }
        
    }
}
