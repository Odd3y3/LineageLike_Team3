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

    public Skills mySkills = null;
    public Skill mySkill = null;

    protected float skillRadius = 0.0f;
    protected float skillDamage = 0.0f;

    protected Transform myTarget = null;

    protected void SetSkillInfo(Skill skill)
    {
        mySkill = skill;
        skillRadius = skill.Range;
        skillDamage = skill.Damage;
    }

    public void OnDamage(float dmg)
    {
        curHP -= dmg-curDefensePoint;
    }

    public bool IsLive
    {
        get
        {
            return curHP > 0.0f;
        }
    }

    protected void Initialize()
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
