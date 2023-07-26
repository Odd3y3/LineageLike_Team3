using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : CharProperty
{

    private void Awake()
    {

    }

    private void OnAnimatorMove()
    {
        if(myAnim.GetBool("IsAttack"))
            transform.parent.Translate(myAnim.deltaPosition, Space.World);
    }
}
