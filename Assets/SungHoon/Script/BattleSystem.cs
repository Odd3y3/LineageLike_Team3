using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BattleStat
{
    public int LV;
    public float MaxHP;
    public float MaxMP;
    public float DefaultAttackPoint;
    public float DefaultDefensePoint;
    public float AttackRange;
    public float AttackDelay;
}

public interface IDamage
{
    void OnDamage(float dmg);
}

public interface ILive
{
    bool IsLive { get; }
}

public class BattleSystem : MoveMent , IDamage, ILive
{
    public List<Item> myItem;

    protected Transform myTarget = null;

    public void OnDamage(float dmg)
    {
        curHP -= dmg - curDefensePoint;
    }

    public bool IsLive
    {
        get
        {
            return curHP > 0.0f;
        }
    }

    protected virtual void Initialize()
    {
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
