using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwordAOE : MonoBehaviour
{
    void Start()
    {
        float playerAttackDmg = GameManager.Inst.inGameManager.myPlayer.curAttackPoint;
        
        //Skill.WarriorSkill_3(playerAttackDmg, transform, LayerMask.GetMask("Enemy"));

        SkillInfo skill = GameManager.Inst.inGameManager.myPlayer.UsingSkill;
        skill?.skill.SkillAttack(playerAttackDmg, skill.skillLV, transform, LayerMask.GetMask("Enemy"));
    }
}
