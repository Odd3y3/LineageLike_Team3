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
        //�⺻ ����
        if (Input.GetMouseButton(0) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetBool("BaseAttack", true);
        }

        //�뽬 Space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Dash");
        }

        //��ų

    }

    public void OnComboCheckStart()
    {
        comboCheckCoroutine = StartCoroutine(ComboChecking());
    }

    IEnumerator ComboChecking()
    {
        myAnim.SetBool("BaseAttack", false);

        while (true)
        {
            if(Input.GetMouseButton(0))
            {
                myAnim.SetBool("BaseAttack", true);
            }
            yield return null;
        }

    }

    public void OnComboCheckEnd()
    {
        StopCoroutine(comboCheckCoroutine);
    }

}
