using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindow : UIProperty<SkillWindowSlot>
{
    public static bool skillWindowActivated = false;
    [SerializeField]
    private GameObject Base;
    [SerializeField]
    private SkillWindowSlot[] slots;

    public TMPro.TMP_Text mySkillPointLavel = null;

    //Skills mySkills;

    private void Awake()
    {
        slots = myAllSlots;
        CloseSkillWindow();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && !MenuUI.GameIsPaused)
        {
            TryOpenSkillWindow();
        }
        //if (SkillPoint <= 0)
        //{
        //    foreach (SkillWindowSlot slots in slots)
        //    {
        //        slots.DontShowLevelUpButton();
        //    }
        //}
    }

    public void TryOpenSkillWindow()
    {
            skillWindowActivated = !skillWindowActivated;
        if (!MenuUI.GameIsPaused)
        {
            if (skillWindowActivated)
            {
                OpenSkillWindow();
            }
            else
            {
                CloseSkillWindow();
            }
        }
    }

    private void OpenSkillWindow()
    {
        Base.SetActive(true);

    }

    private void CloseSkillWindow()
    {
        Base.SetActive(false);
    }

    public void setSkill()
    {
        //mySkills = GameManager.Inst.inGameManager.myPlayerSkill;
        //slots[0].mySkill = mySkills.QSkill;
        //slots[1].mySkill = mySkills.WSkill;
        //slots[2].mySkill = mySkills.ESkill;

        slots[0].mySkill = GameManager.Inst.inGameManager.myPlayer.QSkillInfo;
        slots[1].mySkill = GameManager.Inst.inGameManager.myPlayer.WSkillInfo;
        slots[2].mySkill = GameManager.Inst.inGameManager.myPlayer.ESkillInfo;

        foreach (SkillWindowSlot slot in slots)
        {
            slot.DefalutSetting();
            slot.ChangeInfo();
        }
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        mySkillPointLavel.text = GameManager.Inst.inGameManager.myPlayer.SkillPoint.ToString();
        foreach (SkillWindowSlot slot in slots)
        {
            slot.ChangeInfo();
        }
    }

    public void GetSkillPoint()
    {
        //SkillPoint  = 1*(3*lv)-3;
        ChangeInfo();
        foreach(SkillWindowSlot slot in slots)
        {
            slot.ShowLevelUpButton();
        }
    }
}
