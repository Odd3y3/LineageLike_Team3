using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharProperty : MonoBehaviour
{
    Animator _anim = null;
    protected Animator myAnim
    {
        get
        {
            if (_anim == null)
            {
                _anim = GetComponent<Animator>();
                if (_anim == null)
                {
                    _anim = GetComponentInChildren<Animator>();
                }
            }
            return _anim;
        }
    }

    float _hp = 0.0f;

    protected float curHP
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, 0.0f, BattleStat.MaxHP);
        }
    }

    float _mp = 0.0f;

    protected float curMP
    {
        get => _mp;
        set
        {
            _mp = Mathf.Clamp(value, 0.0f, BattleStat.MaxMP);
        }
    }

    float _ap = 0.0f;

    protected float curAttackPoint
    {
        get => _ap;
        set
        {
            _ap = value;
        }
    }

    float _dp = 0.0f;

    protected float curDefensePoint
    {
        get => _dp;
        set
        {
            _dp = value;
        }
    }

    public BattleStat BattleStat;
}
