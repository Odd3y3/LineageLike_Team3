using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill_1 : IDamageArea
{
    //public void Calculate(float dmg, Transform transform, LayerMask enemyMask)
    //{
    //    BattleManager.AttackDirCircle(transform.position + transform.forward * 3.0f, 3.0f,
    //        enemyMask, dmg, transform.forward, false, 5.0f);
    //}

    public void Calculate(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackDirCircle(transform.position + transform.forward * 3.0f, 3.0f,
            enemyMask, dmg, transform.forward, false, 5.0f);
    }
}
