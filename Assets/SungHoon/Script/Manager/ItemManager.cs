using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public ItemList ItemList = null;
    // Start is called before the first frame update

    public void OnRandomSetItemGrade(Item newItem)
    {
        int Ran = Random.Range(1, 100);
        if (Ran <= 1)
        {
            newItem.ItemGrade = Item.ITEMGRADE.Mythology;
        }
        else if (Ran > 1 && Ran <= 10)
        {
            newItem.ItemGrade = Item.ITEMGRADE.Legendary;
        }else if(Ran >10 &&Ran <= 30)
        {
            newItem.ItemGrade = Item.ITEMGRADE.Eqic;
        }else if(Ran>30 && Ran <= 50)
        {
            newItem.ItemGrade = Item.ITEMGRADE.Unique;
        }else if(Ran >50 && Ran <= 70)
        {
            newItem.ItemGrade = Item.ITEMGRADE.Rare;
        }else if(Ran>70 && Ran <= 99)
        {
            newItem.ItemGrade = Item.ITEMGRADE.Normal;
        }
        newItem.StatPoint= (int)newItem.ItemGrade * 10;
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
