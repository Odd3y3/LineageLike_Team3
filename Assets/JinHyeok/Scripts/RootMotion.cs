using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    Animator myAnim;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        if(myAnim.GetBool("IsAttack"))
            transform.parent.Translate(myAnim.deltaPosition, Space.World);
    }
}
