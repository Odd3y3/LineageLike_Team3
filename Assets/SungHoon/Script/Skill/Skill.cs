using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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


    //데미지 관련해서는 MultiDamage * 플레이어공격력 + AddDamage 로 계산하여 데미지를 줄 예정
    [SerializeField]
    private float MultiDamage = 1.0f;
    [SerializeField]
    private float AddDamage = 0.0f;

    /// <summary>
    /// 플레이어 공격력 넣으면 총 데미지가 얼마인지 반환해주는 함수
    /// </summary>
    /// <param name="playerAttackPoint">플레이어 공격력</param>
    public float TotalDamage(float playerAttackPoint)
    {
        return MultiDamage * playerAttackPoint + AddDamage;
    }


    public float CoolTime = 0.0f;

    //일단 사용하지 않음.
    [SerializeField]
    public float Range = 1.0f;

    //일단 사용하지 않음.
    [SerializeField]
    public float Duration = 0.0f;
}