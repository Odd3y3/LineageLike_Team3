using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "SkillObject/Skill", order = int.MaxValue)]
public class Skill : ScriptableObject
{
    [SerializeField]
    public GameObject Effect=null;
    [SerializeField]
    public float Damage=0.0f;
    [SerializeField]
    public float Range= 0.0f;
    [SerializeField]
    public float Duration = 0.0f;
}
