using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    public Transform myTarget;
    private void Update()
    {
        if(myTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTarget.position + new Vector3(0, 2f, 0));
        }
    }

    public void SetTarget(Transform target)
    {
        myTarget = target;
    }
}
