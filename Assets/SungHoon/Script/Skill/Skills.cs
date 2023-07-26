using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skills", menuName = "SkillObject/Skills", order = int.MaxValue)]
public class Skills : ScriptableObject
{
    [SerializeField]
    public Skill Q = null;
    public Skill W = null;
    public Skill E = null;
    public Skill R = null;
}
