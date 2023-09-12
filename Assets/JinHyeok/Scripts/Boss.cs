using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BattleSystem, ICutScene
{
    private void Start()
    {
        base.Initialize();

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
            //보스 죽는 애니메이션
            //HP바 없애기
            //컷씬 후, 아이템 드랍, 맵 막힌곳 열림, 출구 생김
        }
    }

    public void OnStartCutScene()   
    {
        myTarget = GameManager.Inst.inGameManager.myPlayer.transform;

        myAnim.SetTrigger("Start");
    }
    public void OnEndCutScene()
    {
        myAnim.SetBool("CanMove", true);
        AttackTarget(myTarget);

        //HP바 생성
    }

    //이동 및 공격
    void AttackTarget(Transform target)
    {
        if (target != null)
            StartCoroutine(Attacking(target));
    }

    IEnumerator Attacking(Transform target)
    {
        MoveTargetByPath(target);
        //playTime = 1.0f;
        while (true)
        {
            if (playTime > 0)
                playTime -= Time.deltaTime;

            Vector3 dir = target.position - transform.position;
            dir.y = 0f;
            float dist = dir.magnitude;
            if (dist < BattleStat.AttackRange && !myAnim.GetBool("IsAttack"))
            {
                //공격
                StopMove();
                StopMoveTarget();

                myAnim.SetBool("IsMove", false);
                if (playTime <= 0)
                {
                    StartCoroutine(Rotating(dir));
                    //playTime = BattleStat.AttackDelay;
                    //myAnim.SetTrigger("Attack");
                    RandomAttack(ref playTime);
                }
            }
            else if (!IsMoveToTarget)
            {
                MoveTargetByPath(target);
            }

            yield return null;
        }
    }

    void RandomAttack(ref float playTime)
    {
        int rndValue = Random.Range(1, 5);
        switch (rndValue)
        {
            case 1:
                myAnim.SetTrigger("Attack1");
                playTime = 3.0f;
                break;
            case 2:
                myAnim.SetTrigger("Attack2");
                playTime = 3.0f;
                break;
            case 3:
                myAnim.SetTrigger("Attack3");
                playTime = 4.0f;
                break;
            case 4:
                myAnim.SetTrigger("Attack4");
                playTime = 5.0f;
                break;
        }
    }

    public void BossAttack1()
    {

    }
}
