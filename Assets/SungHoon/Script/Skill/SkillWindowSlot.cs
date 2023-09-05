using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillWindowSlot : UIProperty<SkillWindowSlot>
{
    public Skill mySkill = null;
    public Button myLevleUpButton = null;
    public int mySkillLevel = 1;
    public int SkillRequirement = 1;
    public TMPro.TMP_Text mySkillLevelLavel = null;
    public TMPro.TMP_Text mySkillDamageLavel = null;
    public TMPro.TMP_Text mySkillRequiremenLavel = null;

    float defalutAddDamage = 0.0f;
    float defalutMultiDamage = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        defalutAddDamage = mySkill.AddDamage;
        defalutMultiDamage = mySkill.MultiDamage;
        myImage.sprite = mySkill.Icon;
        ChangeInfo();
        
    }
    // Update is called once per frame
    void Update()
    { 

    }

    public void ChangeInfo()
    {
        mySkillDamageLavel.text=(mySkill.MultiDamage * GameManager.Inst.inGameManager.myPlayer.BattleStat.DefaultAttackPoint + mySkill.AddDamage).ToString();
        mySkillLevelLavel.text = mySkillLevel.ToString();
        mySkillRequiremenLavel.text = SkillRequirement.ToString();
        GameManager.Inst.UiManager.mySkillWindow.ChangeInfo();
    }

    public void ShowLevelUpButton()
    {
        myLevleUpButton.gameObject.SetActive(true);
    }

    public void DontShowLevelUpButton()
    {
        myLevleUpButton.gameObject.SetActive(false);
    }

    public void SkillLevelUp()
    {
        if (GameManager.Inst.UiManager.mySkillWindow.SkillPoint != 0&& GameManager.Inst.UiManager.mySkillWindow.SkillPoint>=SkillRequirement)
        {
            mySkill.AddDamage += 10.0f;
            mySkill.MultiDamage += 1.0f;
            mySkillLevel++;
            GameManager.Inst.UiManager.mySkillWindow.SkillPoint-=SkillRequirement;
            SkillRequirement *= 2;
            ChangeInfo();
            
        }
    }

    private void OnDestroy()
    {
        mySkill.AddDamage = defalutAddDamage;
        mySkill.MultiDamage = defalutMultiDamage;
    }

}
