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


    //������ �����ؼ��� MultiDamage * �÷��̾���ݷ� + AddDamage �� ����Ͽ� �������� �� ����
    [SerializeField]
    public float MultiDamage = 1.0f;
    [SerializeField]
    public float AddDamage = 0.0f;

    [SerializeField]
    public float Range = 1.0f;


    //�ϴ� ������� ����.
    [SerializeField]
    public float Duration = 0.0f;
}
