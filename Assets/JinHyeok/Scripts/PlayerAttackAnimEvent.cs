using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackAnimEvent : MonoBehaviour
{
    public UnityEvent ComboCheckStartAction;
    public UnityEvent ComboCheckEndAction;
    public UnityEvent<Transform, int> Skill1EffectStartAction;

    public void OnComboCheckStart()
    {
        ComboCheckStartAction?.Invoke();
    }
    public void OnComboCheckEnd()
    {
        ComboCheckEndAction?.Invoke();
    }
    public void OnSkillEffectStart(int skillNum)
    {
        Skill1EffectStartAction?.Invoke(transform.parent, skillNum);
    }
}
