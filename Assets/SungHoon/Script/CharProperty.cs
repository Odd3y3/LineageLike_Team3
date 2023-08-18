using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    int _lv =1;

    protected int curLv
    {
        get => _lv;
        set
        {
            _lv = value;
        }
    }

    float _hp = 0.0f;

    protected float curHP
    {
        get => _hp;
        set
        {
            _hp = Mathf.Clamp(value, 0.0f, BattleStat.MaxHP);
            if (myHpBar != null) myHpBar.value = _hp / BattleStat.MaxHP;
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

    float _exp = 0.0f;

    protected float curExp
    {
        get => _exp;
        set
        {
            _exp = value;
        }
    }

    public Slider myHpBar = null;
    protected float playTime = 0.0f;
    public BattleStat BattleStat;

}
