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


    public void OnDamage(float dmg)
    {
        curHP -= dmg;
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
}
