using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "SkillObject/Skill", order = int.MaxValue)]
public class Skill : ScriptableObject
{
    [SerializeField]
    public Sprite Icon = null;
    [SerializeField]
    public AnimationClip AnimationClip = null;
    [SerializeField]
    public GameObject EffectPrefab = null;

    public bool IsAreaSelect = false;
    public GameObject AreaPrefab = null;
    public bool IsDash = false;
    [SerializeField]
    public UnityEvent<float, Transform, LayerMask> damageArea = null;

    //데미지 관련해서는 MultiDamage * 플레이어공격력 + AddDamage 로 계산하여 데미지를 줄 예정
    [SerializeField]
    public float MultiDamage = 1.0f;
    [SerializeField]
    public float AddDamage = 0.0f;


    public float CoolTime = 0.0f;

    public int AttackCount = 1;
    public float AttackInterval = 0.2f;
    public float KnockackDist = 0.0f;

    //일단 사용하지 않음.
    [SerializeField]
    public float Range = 1.0f;

    //일단 사용하지 않음.
    [SerializeField]
    public float Duration = 0.0f;


    /// <summary>
    /// 스킬의 실질적인 공격(판정)하는 함수
    /// 스킬의 공격 판정 함수에 따라서, 각각 다르게 데미지를 줌.
    /// </summary>
    /// <param name="dmg">캐릭터 현재 공격력</param>
    /// <param name="transform">스킬 사용자의 transform</param>
    public void SkillAttack(float charDmg, int skillLV, Transform transform, LayerMask enemyMask)
    {
        damageArea?.Invoke(TotalDamage(charDmg, skillLV), transform, enemyMask);
    }

    /// <summary>
    /// 플레이어 공격력 넣으면 총 데미지가 얼마인지 반환해주는 함수
    /// </summary>
    /// <param name="playerAttackPoint">플레이어 공격력</param>
    public float TotalDamage(float playerAttackPoint, int skillLV)
    {
        return Mathf.Round((MultiDamage * playerAttackPoint + AddDamage) * (0.1f * skillLV + 1.0f));
    }



    //============ 각 스킬의 피해 판정 함수 ===========================
    public void WarriorSkill_1(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackDirCircle(transform.position + transform.forward * 3.0f, 3.0f,
            enemyMask, dmg, transform.forward, false, KnockackDist);
    }

    public void WarriorSkill_2(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackCircle(transform.position, 3.0f, enemyMask, dmg,
            false, KnockackDist);
    }
    public static void WarriorSkill_3(float dmg, Transform transform, LayerMask enemyMask)
    {
        BattleManager.AttackCircle(transform.position, 1.0f, enemyMask, dmg,
            false, 0.1f);
    }
}