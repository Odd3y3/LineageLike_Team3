using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Player : BattleSystem
{
    NavMeshPath path = null;
    public Item PickUpItem = null;
    Coroutine comboCheckCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        //��ų ��� ���߿� �̵� ����
        //������
        //����Ʈ?

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            myAnim.SetBool("IsMove", true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow))
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
        if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAttack"))
        {
            myAnim.SetTrigger("Skill2");
        }
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
            if (Input.GetMouseButton(0))
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

    public void OnEquipItem(Item myItem)
    {
        if(myItem != null)
        {
            if (myItem.EquipmentType == Item.EQUIPMENTTYPE.Weapon)
            {
                curAttackPoint += myItem.StatPoint;
            }
            else
            {
                curDefensePoint += myItem.StatPoint;
            }
        }
    }

    public void MovePos(Vector3 pos)
    {
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            StopAllCoroutines();

            StartCoroutine(MoningByPath(path.corners));
        }
    }

    IEnumerator MoningByPath(Vector3[] list)
    {
        int i = 0;
        while (i < list.Length - 1)
        {

            yield return StartCoroutine(MovingToPos(list[i + 1], () => ++i));
        }
    }
}
