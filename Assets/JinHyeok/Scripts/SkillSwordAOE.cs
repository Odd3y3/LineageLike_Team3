using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSwordAOE : MonoBehaviour
{
    void Start()
    {
        float playerAttackDmg = GameManager.Inst.myPlayer.BattleStat.DefaultAttackPoint;
        Skill.WarriorSkill_3(playerAttackDmg, transform, LayerMask.GetMask("Enemy"));
    }
}
