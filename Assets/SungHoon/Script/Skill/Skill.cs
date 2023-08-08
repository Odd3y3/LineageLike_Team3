using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "SkillObject/Skill", order = int.MaxValue)]
public class Skill : ScriptableObject
{
    [SerializeField]
    public GameObject Icon = null;
    [SerializeField]
    public AnimationClip AnimationClip = null;
    [SerializeField]
    public GameObject EffectPrefab = null;

    public bool IsAreaSelect = false;
    public GameObject AreaPrefab = null;


    //데미지 관련해서는 MultiDamage * 플레이어공격력 + AddDamage 로 계산하여 데미지를 줄 예정
    [SerializeField]
    public float MultiDamage = 1.0f;
    [SerializeField]
    public float AddDamage = 0.0f;

    [SerializeField]
    public float Range = 1.0f;


    //일단 사용하지 않음.
    [SerializeField]
    public float Duration = 0.0f;
}
