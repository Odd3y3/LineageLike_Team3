using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : UIProperty<ShopUISlot>
{
    public Shop myShop = null;
    public ShopUISlot[] slots = null;
    // Start is called before the first frame update
    void Start()
    {
        slots = myAllSlots;
        Setting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setting()
    {
        int Count = slots.Length - myShop.saleItems.Length;
        for(int i=0;i<slots.Length-Count;i++)
        {
                slots[i].saleItemImage.sprite = myShop.saleItems[i].Sprite;
                slots[i].saleName.text = myShop.SaleName[i];
                slots[i].salePrice.text = myShop.itemPrice[i].ToString();
        }
        if(Count != 0)
        {
            for(int i = 1; i <=Count; i++)
            {
                slots[slots.Length - i].gameObject.SetActive(false);
            }
        }
    }
}
