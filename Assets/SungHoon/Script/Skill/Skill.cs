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


    //������ �����ؼ��� MultiDamage * �÷��̾���ݷ� + AddDamage �� ����Ͽ� �������� �� ����
    [SerializeField]
    private float MultiDamage = 1.0f;
    [SerializeField]
    private float AddDamage = 0.0f;

    /// <summary>
    /// �÷��̾� ���ݷ� ������ �� �������� ������ ��ȯ���ִ� �Լ�
    /// </summary>
    /// <param name="playerAttackPoint">�÷��̾� ���ݷ�</param>
    public float TotalDamage(float playerAttackPoint)
    {
        return MultiDamage * playerAttackPoint + AddDamage;
    }


    public float CoolTime = 0.0f;

    //�ϴ� ������� ����.
    [SerializeField]
    public float Range = 1.0f;

    //�ϴ� ������� ����.
    [SerializeField]
    public float Duration = 0.0f;
}