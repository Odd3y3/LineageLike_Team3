using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : UIProperty<SkillUISlot>
{
    public SkillUISlot[] slots = null;

    Skills mySkills;

    private void Awake()
    {
        slots = myAllSlots;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        //SetSkillUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoolTimeSkill(SkillInfo skill)
    {
        foreach(SkillUISlot slots in slots)
        {
            if (slots.mySkill == skill.skill)
            {
                StartCoroutine(slots.Cooling(skill.curSkillCool));
            }
        }
        
    }

    public void SetSkillUI()
    {
        mySkills = GameManager.Inst.inGameManager.myPlayerSkill;
        slots[0].mySkill = mySkills.QSkill;
        slots[1].mySkill = mySkills.WSkill;
        slots[2].mySkill = mySkills.ESkill;
        slots[3].mySkill = mySkills.Dash;
    }

    
}
