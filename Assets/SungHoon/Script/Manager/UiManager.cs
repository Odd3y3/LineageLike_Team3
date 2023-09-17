using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public Slider myHpSlider;
    public Slider myExpSlider;

    public Inventory myInventory;
    public Equipment myEquipment;
    public ConsumptionItem myConsumptionItem;
    public SkillUI mySkillUI;
    public SkillWindow mySkillWindow;
    public GoodsUI myGoodsUI;
    public GameObject myGameOverWindow;
    public Transform myMiniMapIcons;
    public GameObject myBossHpBar;
    public QuestListUI myQuestListUI;


    public void DefalutSetting()
    {
        mySkillUI.SetSkillUI();
        mySkillWindow.setSkill();
        myQuestListUI.InitQuestList();
    }

    public void OnSaveButton()
    {
        GameManager.Inst.inGameManager.Save();
    }

    public void OnGotoTitleButton()
    {
        GameManager.Inst.GotoTitle();
    }
}
