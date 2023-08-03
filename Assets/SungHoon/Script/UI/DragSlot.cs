using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : UIObject
{
    static public DragSlot instance;
    public InventorySlot dragInventorySlot;
    public EquipmentSlot dragEquipmentSlot;

    [SerializeField]
    private Image imageItem;

    void Start()
    {
        instance = this;
    }

    public void DragSetImage(Sprite _itemImage)
    {
        imageItem.sprite = _itemImage;
        SetColor(1);
    }
}
