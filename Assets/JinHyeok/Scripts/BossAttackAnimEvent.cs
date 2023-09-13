using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossAttackAnimEvent : MonoBehaviour
{
    public UnityEvent AttackAction1;
    public UnityEvent AttackAction2;
    public UnityEvent AttackAction3;
    public UnityEvent AttackAction4;

    public void OnAttack1()
    {
        AttackAction1?.Invoke();
    }
    public void OnAttack2()
    {
        AttackAction2?.Invoke();
    }
    public void OnAttack3()
    {
        AttackAction3?.Invoke();
    }
    public void OnAttack4()
    {
        AttackAction4?.Invoke();
    }
}
