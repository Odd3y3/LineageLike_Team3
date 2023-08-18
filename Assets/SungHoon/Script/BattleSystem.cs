using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

public interface IDamage
{
    void OnDamage(float dmg, AttackType attackType, Vector3 attackVec, float knockBackDist);
}

public interface ILive
{
    bool IsLive { get; }
}

public class BattleSystem : MoveMent , IDamage, ILive
{
    public List<Item> myItem;

    [SerializeField] Transform myAttackArea = null;
    
    [SerializeField] LayerMask enemyMask;

    protected Transform myTarget = null;

    public void OnDamage(float dmg, AttackType attackType, Vector3 attackVec, float knockBackDist)
    {
        curHP -= dmg - curDefensePoint;
        switch(attackType)
        {
            case AttackType.Normal:
                Debug.Log($"Attack type is NORMAL \n Damage is {dmg}");
                break;
            case AttackType.Stagger:
                Debug.Log($"Attack type is Stagger \n Damage is {dmg}");
                break;
            case AttackType.Down:
                Debug.Log($"Attack type is Down \n Damage is {dmg}");
                break;
        }
    }
    public void OnBaseAttack()
    {
        BattleManager.AttackCircle(myAttackArea.position,
            1.0f,
            enemyMask,
            BattleStat.DefaultAttackPoint,
            transform.forward,
            AttackType.Normal);
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

        curLv = BattleStat.LV;
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
