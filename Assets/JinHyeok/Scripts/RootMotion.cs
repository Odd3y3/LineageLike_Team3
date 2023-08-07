using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RootMotion : MonoBehaviour
{
    Animator myAnim;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        if (myAnim.GetBool("IsAttack"))
        {
            transform.parent.Translate(myAnim.deltaPosition, Space.World);

            if (NavMesh.SamplePosition(transform.parent.position, out NavMeshHit hit, 5.0f, NavMesh.AllAreas))
            {
                transform.parent.position = hit.position;
            }
        }
    }
}
