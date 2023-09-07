using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Slider myHpSlider;

    public Inventory myInventory;
    public Equipment myEquipment;
    public ConsumptionItem myConsumptionItem;
    public SkillUI mySkillUI;
    public SkillWindow mySkillWindow;


    public void DefalutSetting()
    {
        mySkillUI.SetSkillUI();
        mySkillWindow.SetSkill();

    }

    public void OnSaveButton()
    {
        GameManager.Inst.inGameManager.Save();
    }

    public void OnGoTitleButton()
    {

    }
}
