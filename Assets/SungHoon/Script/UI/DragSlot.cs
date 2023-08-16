using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : UIObject
{
    static public DragSlot instance;
    public InventorySlot dragInventorySlot;
    public int dragItemCount;
    public TMPro.TMP_Text myText=null;

    [SerializeField]
    private Image imageItem;

    void Start()
    {
        instance = this;
    }

    new public void SetColor(float _alpha)
    {
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;

        if (_alpha == 1 && dragInventorySlot.item.ItemType !=Item.ITEMTYPE.Equipment &&dragItemCount>1)
        {
            myText.gameObject.SetActive(true);
            myText.text = dragItemCount.ToString();
        }
        else
        {
            myText.gameObject.SetActive(false);
            myText.text = dragItemCount.ToString();
        }
    }

    public void DragSetImage(Sprite _itemImage)
    {
        imageItem.sprite = _itemImage;
        SetColor(1);
    }
}
