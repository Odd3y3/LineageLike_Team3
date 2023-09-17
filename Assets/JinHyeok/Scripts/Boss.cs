using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BattleSystem, ICutScene
{
    public Transform attackArea1;
    public Transform attackArea2;
    public Transform attackArea3;

    public GameObject exitPortal;

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
            StopAllCoroutines();
            StartCoroutine(StartDeadCoroutine());
        }
    }
    IEnumerator StartDeadCoroutine()
    {
        myAnim.SetTrigger("Die");
        GetComponent<Collider>().enabled = false;

        yield return new WaitForSeconds(2.0f);

        GameManager.Inst.UiManager.myBossHpBar.SetActive(false);
        DropExp(BattleStat.MaxExp);
        DropItem();
        exitPortal.SetActive(true);

        //퀘스트 진행
        GameManager.Inst.questManager.ProcessQuest(QuestType.DestroyEnemy, ID);
    }
    public void DropItem()
    {
        GameObject obj = Instantiate(Resources.Load("DropItem") as GameObject);
        obj.GetComponent<ItemDrop>().OnSetItem(this.gameObject.transform);
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
        GameManager.Inst.UiManager.myBossHpBar.SetActive(true);
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
                playTime = 2.0f;
                break;
            case 2:
                myAnim.SetTrigger("Attack2");
                playTime = 2.0f;
                break;
            case 3:
                myAnim.SetTrigger("Attack3");
                playTime = 3.0f;
                break;
            case 4:
                myAnim.SetTrigger("Attack4");
                playTime = 3.0f;
                break;
        }
    }

    public void BossAttack1()
    {
        BattleManager.AttackDirCircle(attackArea1.position, 2.0f, enemyMask, 20.0f,
            transform.forward, false, 2.0f);
    }
    public void BossAttack2()
    {
        BattleManager.AttackDirCircle(attackArea2.position, 2.0f, enemyMask, 20.0f,
            transform.forward, false, 2.0f);
    }
    public void BossAttack3()
    {
        BattleManager.AttackDirCircle(attackArea3.position, 2.5f, enemyMask, 50.0f,
            transform.forward, false, 4.0f);
    }
    public void BossAttack4()
    {
        BattleManager.AttackCircle(transform.position, 5.0f, enemyMask, 40.0f,
            false, 4.0f);
    }
}
