using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackAnimEvent : MonoBehaviour
{
    public UnityEvent ComboCheckStartAction;
    public UnityEvent ComboCheckEndAction;
    public UnityEvent<GameObject> SkillEffectStartAction;
    public UnityEvent SkillAction;
    public UnityEvent AttackAction;
    public UnityEvent DeadAction;

    public Transform Player = null;
    public UnityEvent<Transform, int> Skill1EffectStartAction;

    public void OnComboCheckStart()
    {
        ComboCheckStartAction?.Invoke();
    }
    public void OnComboCheckEnd()
    {
        ComboCheckEndAction?.Invoke();
    }
    
    public void OnAttack()
    {
        AttackAction?.Invoke();
    }
    public void OnSkill()
    {
        SkillAction?.Invoke();
    }
    public void OnDead()
    {
        DeadAction?.Invoke();
    }

    //public void OnSkillEffectStart()
    //{
    //    SkillEffectStartAction?.Invoke(Player.GetComponent<Player>().mySkill.Effect);
    //}
    public void OnSkillEffectStart(int skillNum)
    {
        Skill1EffectStartAction?.Invoke(transform.parent, skillNum);
    }
}