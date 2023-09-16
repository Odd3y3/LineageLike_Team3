using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillWindowSlot : UIProperty<SkillWindowSlot>
{
    public SkillInfo mySkill = null;
    public Button myLevleUpButton = null;
    //public int mySkillLevel = 1;
    //public int SkillRequirement = 1;
    public TMPro.TMP_Text mySkillLevelLavel = null;
    public TMPro.TMP_Text mySkillDamageLavel = null;
    public TMPro.TMP_Text mySkillRequiremenLavel = null;

    float defalutAddDamage = 0.0f;
    float defalutMultiDamage = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    { 

    }

    public void DefalutSetting()
    {
        //defalutAddDamage = mySkill.skill.AddDamage;
        //defalutMultiDamage = mySkill.skill.MultiDamage;
        myImage.sprite = mySkill.skill.Icon;
    }

    public void ChangeInfo()
    {
        mySkillDamageLavel.text=(mySkill.skill.TotalDamage(GameManager.Inst.inGameManager.myPlayer.curAttackPoint)).ToString();
        mySkillLevelLavel.text = mySkill.skillLV.ToString();
        mySkillRequiremenLavel.text = mySkill.skillLV.ToString();
        //GameManager.Inst.UiManager.mySkillWindow.ChangeInfo();
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
        if (GameManager.Inst.inGameManager.myPlayer.SkillPoint != 0&& GameManager.Inst.inGameManager.myPlayer.SkillPoint >= mySkill.skillLV)
        {
            //mySkill.AddDamage += 10.0f;
            //mySkill.MultiDamage += 1.0f;
            //mySkillLevel++;
            GameManager.Inst.inGameManager.myPlayer.SkillPoint -= mySkill.skillLV;
            mySkill.skillLV++;
            //SkillRequirement *= 2;
            GameManager.Inst.UiManager.mySkillWindow.ChangeInfo();

        }
    }

    private void OnDestroy()
    {
        //mySkill.skill.AddDamage = defalutAddDamage;
        //mySkill.skill.MultiDamage = defalutMultiDamage;
    }

}
