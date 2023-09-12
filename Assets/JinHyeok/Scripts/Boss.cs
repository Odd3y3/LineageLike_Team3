using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BattleSystem
{
    private void Start()
    {
        base.Initialize();

        myTarget = GameManager.Inst.inGameManager.myPlayer.transform;
    }

    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        //보스 몬스터는 데미지만
        float damage = dmg - curDefensePoint;
        damage = damage <= 1 ? 1 : damage;
        curHP -= damage;

        BattleManager.DamagePopup(transform, damage);

        if (!IsLive)
        {
            //보스 죽음, 클리어
            //컷씬, 보스 죽는 애니메이션
            //컷씬 후, 아이템 드랍, 맵 막힌곳 열림, 출구 생김
        }
    }

    //이동 및 공격

}
