using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAttackAnimEvent : MonoBehaviour
{
    public UnityEvent ComboCheckStartAction;
    public UnityEvent ComboCheckEndAction;

    public void OnComboCheckStart()
    {
        ComboCheckStartAction?.Invoke();
    }
    public void OnComboCheckEnd()
    {
        ComboCheckEndAction?.Invoke();
    }

}
