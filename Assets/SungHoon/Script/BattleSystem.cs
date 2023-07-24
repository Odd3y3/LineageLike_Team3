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

public class BattleSystem : MoveMent
{
    public List<Item> myItem;
}
