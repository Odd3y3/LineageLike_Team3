using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : Singleton<UiManager>
{
    public Inventory myInventory;
    public Equipment myEquipment;
    public ConsumptionItem myConsumptionItem;
    public SkillUI mySkillUI;
    public SkillWindow mySkillWindow;


    private void Awake()
    {
       base.Initialize();
    }
}
