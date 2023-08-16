using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public Inventory myInventory;
    public Equipment myEquipment;
    public ConsumptionItem myConsumptionItem;

    private void Awake()
    {
        Initialized();
    }
}
