using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AImovement : BattleSystem
{
    AIPerception _perception = null;
    protected AIPerception myPerception
    {
        get
        {
            if (_perception == null)
            {
                _perception = GetComponent<AIPerception>();
                if (_perception == null)
                {
                    _perception = GetComponentInChildren<AIPerception>();
                }
            }
            return _perception;
        }
    }

    protected void AttackTarget(Transform target)
    {
        StopAllCoroutines();
        if (target != null)
            StartCoroutine(Attacking(target));
    }

    IEnumerator Attacking(Transform target)
    {
        MoveTargetByPath(target);

        while (true)
        {
            if(playTime > 0)
                playTime -= Time.deltaTime;

            Vector3 dir = target.position - transform.position;
            dir.y = 0f;
            float dist = dir.magnitude;
            if(dist < BattleStat.AttackRange && !myAnim.GetBool("IsAttack"))
            {
                //АјАн
                StopMove();
                StopMoveTarget();

                myAnim.SetBool("IsMove", false);
                if (playTime <= 0)
                {
                    StartCoroutine(Rotating(dir));
                    playTime = BattleStat.AttackDelay;
                    myAnim.SetTrigger("Attack");
                }
            }
            else if (myAnim.GetBool("IsDamaged"))
            {
                StopMove();
                StopMoveTarget();
            }
            else if (!IsMoveToTarget)
            {
                MoveTargetByPath(target);
            }

            yield return null;
        }
    }

    //IEnumerator Attacking(Transform target)
    //{
    //    ILive live = target.GetComponent<ILive>();
    //    while (target != null)
    //    {
    //        if (live != null && !live.IsLive) break;
    //        playTime += Time.deltaTime;
    //        Vector3 dir = target.position - transform.position;
    //        float dist = dir.magnitude - BattleStat.AttackRange;
    //        if (dist < 0.01f) dist = 0.0f;
    //        dir.Normalize();

    //        float delta = moveSpeed * Time.deltaTime;
    //        if (!Mathf.Approximately(dist, 0.0f))
    //        {
    //            myAnim.SetBool("IsMove", true);

    //            if (delta > dist) delta = dist;
    //            if (!myAnim.GetBool("IsAttacking"))
    //            {
    //                transform.Translate(dir * delta, Space.World);
    //            }
    //        }
    //        else
    //        {
    //            myAnim.SetBool("IsMove", false);
    //            if (playTime >= BattleStat.AttackDelay)
    //            {
    //                playTime = 0.0f;
    //                myAnim.SetTrigger("Attack");
    //            }

    //        }
    //        float angle = Vector3.Angle(dir, transform.forward);
    //        float rotDir = 1.0f;
    //        if (Vector3.Dot(dir, transform.right) < 0.0f)
    //        {
    //            rotDir = -1.0f;
    //        }
    //        delta = rotSpeed * Time.deltaTime;

    //        if (!Mathf.Approximately(angle, 0.0f))
    //        {
    //            if (delta > angle) delta = angle;
    //            transform.Rotate(Vector3.up * delta * rotDir);
    //        }

    //        yield return null;
    //    }
    //}
}
    
