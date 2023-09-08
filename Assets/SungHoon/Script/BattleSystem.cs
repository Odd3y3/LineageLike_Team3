using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

[System.Serializable]
public struct BattleStat
{
    public int LV;
    public float MaxHP;
    public float MaxMP;
    public float MaxExp;
    public float DefaultAttackPoint;
    public float DefaultDefensePoint;
    public float AttackRange;
    public float AttackDelay;
    public float AttackSize;
}

public interface IDamage
{
    void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown);
}

public interface ILive
{
    bool IsLive { get; }
}

public class BattleSystem : MoveMent , IDamage, ILive
{
    public List<Item> myItem;

    [SerializeField] Transform myAttackArea = null;
    
    [SerializeField] protected LayerMask enemyMask;

    protected Transform myTarget = null;


    public virtual void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        float damage = dmg - curDefensePoint;
        damage = damage <= 1 ? 1 : damage;
        curHP -= damage;
        if (!isDown)
        {
            //일반 공격일 때, (경직)
            OnCharStagger();
        }
        else
        {
            //다운 공격일 때,
            OnCharDown();
        }
        KnockBack(attackVec, knockBackDist);
        BattleManager.DamagePopup(transform, damage);
    }

    protected virtual void OnCharStagger()
    {
        StopMove();
        //myAnim.SetTrigger("Damaged");
        myAnim.Play("Damaged", -1, 0f);
        
    }
    protected virtual void OnCharDown()
    {

    }

    void KnockBack(Vector3 attackVec, float knockBackDist)
    {
        transform.forward = -attackVec;
        //transform.Translate(attackVec * knockBackDist, Space.World);
        StartCoroutine(Moving(attackVec * knockBackDist));
    }
    IEnumerator Moving(Vector3 dir)
    {
        float dist = dir.magnitude;
        dir.Normalize();

        while (dist > 0.0f)
        {
            float delta = Time.deltaTime * 30.0f;   //넉백 속도 일단 고정. 30.0f
            if (delta > dist) delta = dist;
            dist -= delta;

            transform.Translate(dir * delta, Space.World);

            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
            yield return null;
        }
    }

    public void OnBaseAttack()
    {
        BattleManager.AttackDirCircle(myAttackArea.position,
            BattleStat.AttackSize,
            enemyMask,
            curAttackPoint,
            transform.forward,
            false, 0.5f);
    }

    public bool IsLive
    {
        get
        {
            return curHP > 0.0f;
        }
    }

    protected override void Initialize()
    {
        base.Initialize();

        curHP = BattleStat.MaxHP;
        curMP = BattleStat.MaxMP;
        curAttackPoint = BattleStat.DefaultAttackPoint;
        curDefensePoint = BattleStat.DefaultDefensePoint;
    }

    //public void OnAttack()
    //{
    //    IDamage damage = myTarget.GetComponent<IDamage>();
    //    if (damage != null) damage.OnDamage(battleStat.AttackPoint);
    //}
}
