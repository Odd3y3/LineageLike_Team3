using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : UIProperty
{
    public SkillUISlot[] slots = null;

    private void Awake()
    {
        slots = myAllSkillSlots;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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

    public void SetSkillUI(Skills skills)
    {
        slots[0].mySkill = skills.QSkill;
        slots[1].mySkill = skills.WSkill;
        slots[2].mySkill = skills.ESkill;
    }
}
