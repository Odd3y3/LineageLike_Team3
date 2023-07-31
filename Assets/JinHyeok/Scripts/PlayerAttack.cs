using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator myAnim;
    Coroutine comboCheckCoroutine;

    private void Awake()
    {
        myAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        // (Test)이동 애니메이션
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myAnim.SetBool("IsMove", true);
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            myAnim.SetBool("IsMove", false);
        }

        //대쉬 Space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Dash");
        }

        //기본 공격
        if (Input.GetMouseButton(0) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetBool("BaseAttack", true);
        }

        //스킬 애니메이션
        if (Input.GetKeyDown(KeyCode.Q) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetTrigger("Skill1");
        }

    }
}
