using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : CharProperty
{
    public float moveSpeed = 1.0f;
    public float rotSpeed = 360.0f;
   
    // Start is called before the first frame update
    public void MoveToPos(Vector3 pos)
    {
        MoveToPos(pos, null);
    }
    public void MoveToPos(Vector3 pos, UnityAction done)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToPos(pos, done));
    }
    protected IEnumerator MovingToPos(Vector3 pos, UnityAction done)
    {
        myAnim.SetBool("IsMove", true);
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
       
        StartCoroutine(Rotating(dir));

        while (dist > 0.0f)
        {
            if (!myAnim.GetBool("IsAttack"))
            {
                float delta = moveSpeed * Time.deltaTime;
                if (delta > dist) delta = dist;
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("IsMove", false);
        done?.Invoke();
    }
    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(dir, transform.forward);
        float rotDir = 1.0f;
        if (Vector3.Dot(dir, transform.right) < 0.0f)
        {
            rotDir = -1.0f;
        }
        while (!Mathf.Approximately(angle, 0.0f))
        {
            float delta = rotSpeed * Time.deltaTime;
            if (delta > angle) delta = angle;
            angle -= delta;
            transform.Rotate(Vector3.up * delta * rotDir);
            yield return null;
        }
    }
    protected void AttackTarget(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(Attacking(target));
    }
    IEnumerator Attacking(Transform target)
    {
        ILive live = target.GetComponent<ILive>();
        while (target != null)
        {
            if (live != null && !live.IsLive) break;
            playTime += Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            float dist = dir.magnitude - BattleStat.AttackRange;
            if (dist < 0.01f) dist = 0.0f;
            dir.Normalize();

            float delta = moveSpeed * Time.deltaTime;

            if (!Mathf.Approximately(dist, 0.0f))
            {
                myAnim.SetBool("IsMove", true);
                if (delta > dist) delta = dist;
                if (!myAnim.GetBool("IsAttack"))
                {
                    transform.Translate(dir * delta, Space.World);
                }
            }
            else
            {
                myAnim.SetBool("IsMove", false);
                if (playTime >= BattleStat.AttackDelay)
                {
                    playTime = 0.0f;
                    myAnim.SetTrigger("IsAttack");
                }
            }

            float angle = Vector3.Angle(dir, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(dir, transform.right) < 0.0f)
            {
                rotDir = -1.0f;
            }
            delta = rotSpeed * Time.deltaTime;
            if (!Mathf.Approximately(angle, 0.0f))
            {
                if (delta > angle) delta = angle;
                transform.Rotate(Vector3.up * delta * rotDir);
            }

            yield return null;
        }
    }
}
