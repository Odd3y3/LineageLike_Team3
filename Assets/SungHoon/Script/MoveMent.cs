using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MoveMent : CharProperty
{
    public float moveSpeed = 2.0f;
    public float rotSpeed = 360.0f;
    public LayerMask skillClickMask;
    public LayerMask virtualGroundMask;

    List<Coroutine> moveCoroutineList = new List<Coroutine>();
    Coroutine moveTargetCoroutine;

    NavMeshPath path = null;

    protected virtual void Initialize()
    {
        path = new NavMeshPath();
    }

    /// <summary>
    /// Target�� Navmesh ���� �̵��ϴ� �Լ�. (0.5�ʸ��� target�� ��ġ�� �̵�)
    /// �̵� ���߷��� StopMoveTarget() ȣ��.
    /// </summary>
    protected void MoveTargetByPath(Transform target)
    {
        StopMoveTarget();
        moveTargetCoroutine = StartCoroutine(MovingTargetByPath(target));
    }
    IEnumerator MovingTargetByPath(Transform target)
    {
        while(target != null)
        {
            MovePosByPath(target.position);
            yield return new WaitForSeconds(0.5f);
        }
    }
    protected void StopMoveTarget()
    {
        if (moveTargetCoroutine != null)
            StopCoroutine(moveTargetCoroutine);
        moveTargetCoroutine = null;
    }

    /// <summary>
    /// pos������ Navmesh ���� �̵��ϴ� �Լ�.
    /// </summary>
    public void MovePosByPath(Vector3 pos)
    {
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path) && !myAnim.GetBool("IsAttack"))
        {
            StopMove();
            //StopAllCoroutines();

            moveCoroutineList.Add(StartCoroutine(MovingByPath(path.corners)));
        }
    }

    IEnumerator MovingByPath(Vector3[] list)
    {
        int i = 0;
        while (i < list.Length - 1)
        {
            Coroutine co = StartCoroutine(MovingToPos(list[i + 1], () => ++i));
            moveCoroutineList.Add(co);
            yield return co;
        }
    }


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
        //dir.y = 0.0f;
        float dist = dir.magnitude;
        dir.Normalize();

        moveCoroutineList.Add(StartCoroutine(Rotating(dir)));
        while (dist > 0.0f)
        {
            float delta = Time.deltaTime * moveSpeed;
            if (delta > dist) delta = dist;
            dist -= delta;

            transform.Translate(dir * delta, Space.World);

            if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
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
        }//���� Ŭ�� �ߴ��� Ȯ��

        while (!Mathf.Approximately(angle, 0.0f))
        {
            float delta = rotSpeed * Time.deltaTime; //ȸ���� ���� 
            if (delta > angle)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up * delta * rotDir);
            yield return null;
        }
    }

    protected void StopMoveAndRotate()
    {
        StopMove();

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, skillClickMask) ||
            Physics.Raycast(ray, out hit, Mathf.Infinity, virtualGroundMask))
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
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, skillClickMask) ||
            Physics.Raycast(ray, out hit, Mathf.Infinity, virtualGroundMask))
        {
            Vector3 vec = hit.point - transform.position;
            vec.y = 0;

            float angle = Vector3.Angle(vec, transform.forward);
            float rotDir = 1.0f;
            if (Vector3.Dot(vec, transform.right) < 0)
            {
                rotDir = -1.0f;
            }//���� Ŭ�� �ߴ��� Ȯ��
            transform.Rotate(Vector3.up * angle * rotDir);
        }
    }
}
