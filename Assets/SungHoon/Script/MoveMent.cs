using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveMent : CharProperty
{
    public float moveSpeed = 2.0f;
    public float rotSpeed = 360.0f;
    public LayerMask skillClickMask;

    protected List<Coroutine> moveCoroutineList;

    public void MoveToPos(Vector3 Pos)
    {
        MoveToPos(Pos, null);
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
            float delta = Time.deltaTime * moveSpeed;
            if (delta > dist) delta = dist;
            dist -= delta;
            transform.Translate(dir * delta,Space.World);
            yield return null;
        }
        myAnim.SetBool("IsMove", false);
        done?.Invoke();
    }

    protected IEnumerator Rotating(Vector3 dir)
    {
        dir.y = 0;
        float angle = Vector3.Angle(dir, transform.forward);
        float rotDir = 1.0f;
        if (Vector3.Dot(dir, transform.right) < 0)
        {
            rotDir = -1.0f;
        }//왼쪽 클릭 했는지 확인

        while (!Mathf.Approximately(angle, 0.0f))
        {
            float delta = rotSpeed * Time.deltaTime; //회전할 각도 
            if (delta > angle)
            {
                delta = angle;
            }
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
                if (!myAnim.GetBool("IsAttacking"))
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
                    myAnim.SetTrigger("Attack");
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

    protected void StopMoveAndRotate()
    {
        StopMove();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, skillClickMask))
        {
            Vector3 vec = hit.point - transform.position;
            moveCoroutineList.Add(StartCoroutine(Rotating(vec)));
        }
    }

    protected void StopMove()
    {
        foreach (Coroutine co in moveCoroutineList)
        {
            if (co != null)
            {
                StopCoroutine(co);
            }
        }
        moveCoroutineList.Clear();
    }

    protected void ImmediateRotate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, skillClickMask))
        {
            Vector3 vec = hit.point - transform.position;
            vec.y = 0;

            float angle = Vector3.Angle(vec, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(vec, transform.right) < 0)
            {
                rotDir = -1.0f;
            }//왼쪽 클릭 했는지 확인
            transform.Rotate(Vector3.up * angle * rotDir);
        }
    }
}
