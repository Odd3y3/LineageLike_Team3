using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MoveMent : CharProperty
{
    public float moveSpeed = 2.0f;
    public float rotSpeed = 360.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        done?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)
    {
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
}
