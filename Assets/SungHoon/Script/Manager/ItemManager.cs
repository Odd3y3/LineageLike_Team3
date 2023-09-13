using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton<ItemManager>
{
    public ItemList ItemList = null;
    // Start is called before the first frame update

    

    public void OnRandomSetItemGrade(Item newItem)
    {
        if (newItem.ItemType == Item.ITEMTYPE.Equipment)
        {
            int Ran = Random.Range(1, 100);
            if (Ran <= 1)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Mythology;
            }
            else if (Ran > 1 && Ran <= 10)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Legendary;
            }
            else if (Ran > 10 && Ran <= 30)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Eqic;
            }
            else if (Ran > 30 && Ran <= 50)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Unique;
            }
            else if (Ran > 50 && Ran <= 70)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Rare;
            }
            else if (Ran > 70 && Ran <= 99)
            {
                newItem.ItemGrade = Item.EQUIPMENGRADE.Normal;
            }
            newItem.StatPoint = (int)newItem.ItemGrade * 10;
        }   
    }


    private void Awake()
    {
        base.Initialize();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
