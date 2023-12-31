using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackAnimEvent : MonoBehaviour
{
    public UnityEvent ComboCheckStartAction;
    public UnityEvent ComboCheckEndAction;
    public UnityEvent SkillAction;
    public UnityEvent AttackAction;
    public UnityEvent DeadAction;
    public UnityEvent DashAction;

    //public Transform Player = null;
    public UnityEvent SkillEffectStartAction;

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
    public void OnSkillEffectStart()
    {
        SkillEffectStartAction?.Invoke();
    }

    public void OnDash()
    {
        DashAction?.Invoke();
    }
}
