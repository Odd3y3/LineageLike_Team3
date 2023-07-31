//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public interface IDamage
//{
//    void OnDamage(float dmg);
//}
//public interface ILive
//{
//    bool IsLive
//    {
//        get;
//    }
//}
//[System.Serializable]
//public struct BattleStat
//{
//    public float MaxHealPoint;
//    public float AttackPoint;
//    public float AttackRange;
//    public float AttackDelay;
//}


//public class BattleSystem : Movement, IDamage, ILive
//{

//    protected Transform myTarget = null;
//    public bool IsLive
//    {
//        get
//        {
//            return curHP > 0.0f;
//        }
//    }
//    protected void Initialize()
//    {
//        curHP = battleStat.MaxHealPoint;
//    }
//    public void OnAttack()
//    {
//        IDamage damage = myTarget.GetComponent<IDamage>();
//        if (damage != null) damage.OnDamage(battleStat.AttackPoint);
//    }
//    // Start is called before the first frame update
//    public void OnDamage(float dmg)
//    {
//        curHP -= dmg;
//        if (curHP > 0.0f)
//        {
//            myAnim.SetTrigger("Damage");
//            StartCoroutine(DamagingEff(0.3f));
//        }
//        else
//        {
//            OnDead();
//            myAnim.SetTrigger("Die");
//        }
//    }
//    protected virtual void OnDead()
//    {

//    }
//    IEnumerator DamagingEff(float t)
//    {
//        foreach (Renderer render in myAllRenders)
//        {
//            render.material.color = Color.red;
//        }

//        yield return new WaitForSeconds(t);

//        foreach (Renderer render in myAllRenders)
//        {
//            render.material.color = Color.white;
//        }
//    }
//}
