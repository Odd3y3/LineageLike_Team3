//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class CharacterProperty : MonoBehaviour
//{
//    Animator _anim = null;
//    protected Animator myAnim
//    {
//        get
//        {
//            if (_anim == null)
//            {
//                _anim = GetComponent<Animator>();
//                if (_anim == null)
//                {
//                    _anim = GetComponentInChildren<Animator>();
//                }
//            }
//            return _anim;
//        }
//    }
//    Renderer _renderer = null;
//    protected Renderer myRenderer
//    {
//        get
//        {
//            if (_renderer == null)
//            {
//                _renderer = GetComponent<Renderer>();
//                if (_renderer == null)
//                {
//                    _renderer = GetComponentInChildren<Renderer>();
//                }
//            }
//            return _renderer;
//        }
//    }
//    Renderer[] _allRenders = null;
//    protected Renderer[] myAllRenders
//    {
//        get
//        {
//            if (_allRenders == null)
//            {
//                _allRenders = GetComponentsInChildren<Renderer>();
//            }
//            return _allRenders;
//        }
//    }
//    public BattleStat battleStat;
//    public Slider myHpBar = null;
//    float _hp = 0.0f;
//    protected float curHP
//    {
//        get => _hp;
//        set
//        {
//            _hp = Mathf.Clamp(value, 0.0f, battleStat.MaxHealPoint);
//            if (myHpBar != null) myHpBar.value = _hp / battleStat.MaxHealPoint;
//        }
//    }
//    protected float playTime = 0.0f;
//}

