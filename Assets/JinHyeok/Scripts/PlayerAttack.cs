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
        // (Test)�̵� �ִϸ��̼�
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myAnim.SetBool("IsMove", true);
        }
        if(Input.GetKeyUp(KeyCode.UpArrow))
        {
            myAnim.SetBool("IsMove", false);
        }

        //�뽬 Space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Dash");
        }

        //�⺻ ����
        if (Input.GetMouseButton(0) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetBool("BaseAttack", true);
        }

        //��ų �ִϸ��̼�
        if (Input.GetKeyDown(KeyCode.Q) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetTrigger("Skill1");
        }

    }
}
