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

    //������ �����ؼ��� MultiDamage * �÷��̾���ݷ� + AddDamage �� ����Ͽ� �������� �� ����
    [SerializeField]
    public float MultiDamage = 1.0f;
    [SerializeField]
    public float AddDamage = 0.0f;


    public float CoolTime = 0.0f;

    public int AttackCount = 1;
    public float AttackInterval = 0.2f;
    public float KnockackDist = 0.0f;

    //�ϴ� ������� ����.
    [SerializeField]
    public float Range = 1.0f;

    //�ϴ� ������� ����.
    [SerializeField]
    public float Duration = 0.0f;


    /// <summary>
    /// ��ų�� �������� ����(����)�ϴ� �Լ�
    /// ��ų�� ���� ���� �Լ��� ����, ���� �ٸ��� �������� ��.
    /// </summary>
    /// <param name="dmg">ĳ���� ���� ���ݷ�</param>
    /// <param name="transform">��ų ������� transform</param>
    public void SkillAttack(float charDmg, int skillLV, Transform transform, LayerMask enemyMask)
    {
        damageArea?.Invoke(TotalDamage(charDmg, skillLV), transform, enemyMask);
    }

    /// <summary>
    /// �÷��̾� ���ݷ� ������ �� �������� ������ ��ȯ���ִ� �Լ�
    /// </summary>
    /// <param name="playerAttackPoint">�÷��̾� ���ݷ�</param>
    public float TotalDamage(float playerAttackPoint, int skillLV)
    {
        return Mathf.Round((MultiDamage * playerAttackPoint + AddDamage) * (0.1f * skillLV + 1.0f));
    }



    //============ �� ��ų�� ���� ���� �Լ� ===========================
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