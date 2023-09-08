using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent AttackAction;
    public UnityEvent DeadAction;
    public UnityEvent ComboCheckStartAct;
    public UnityEvent ComboCheckEndAct;
    public UnityEvent SkillAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnAttack()
    {
        AttackAction?.Invoke();
    }

    public void OnDead()
    {
        DeadAction?.Invoke();
    }

    public void ComboCheckStart()
    {
        ComboCheckStartAct?.Invoke();
    }

    public void ComboCheckEnd()
    {
        ComboCheckEndAct?.Invoke();
    }

    public void OnSkill()
    {
        SkillAction?.Invoke();
    }
}