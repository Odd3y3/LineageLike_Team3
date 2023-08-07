using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEditor.Timeline.Actions;

public class Player : PlayerBattleSystem
{
    NavMeshPath path = null;
    public Item PickUpItem = null;
    public Transform myWeaponPos = null;
    public LayerMask enemyMask;


    Coroutine comboCheckCoroutine;

    void Awake()
    {
        GameManager.Inst.myPlayer = this;
    }

    void Start()
    {
        Initialize();
        path = new NavMeshPath();
    }
    
    void Update()
    {
        //데미지
        //이펙트

        //대쉬 Space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopMove();
            ImmediateRotate();
            myAnim.SetTrigger("Dash");
        }

        if (!IsSkillAreaSelecting)
        {
            //기본 공격
            if (Input.GetMouseButton(0) && !myAnim.GetBool("IsAttack"))
            {
                StopMoveAndRotate();
                myAnim.SetBool("BaseAttack", true);
            }

            //스킬 애니메이션
            if (Input.GetKeyDown(KeyCode.Q) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.QSkill);
            }
            else if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.WSkill);
            }
            else if (Input.GetKeyDown(KeyCode.E) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.ESkill);
            }
        }

    }

    

    //public void OnSkillEffect(GameObject Effect)
    //{
    //    GameObject obj = Instantiate(Effect);
    //    obj.transform.position = transform.position;
    //    obj.transform.localRotation = transform.localRotation;
    //    StartCoroutine(Durating(mySkill.Duration, obj));
    //}

    IEnumerator Durating(float t,GameObject obj)
    {
        yield return new WaitForSeconds(t);
        Destroy(obj);
    }

    public void OnAttak()
    {
        Collider[] myCols = Physics.OverlapSphere(myWeaponPos.position, 1.0f, enemyMask);
        foreach(Collider col in myCols)
        {
            IDamage damage = col.GetComponent<IDamage>();
            if (damage != null) damage.OnDamage(curAttackPoint);
        }
    }

    public void OnSkill()
    {

            Collider[] myCols = Physics.OverlapSphere(transform.position, skillRadius, enemyMask);
        foreach (Collider col in myCols)
        {
            IDamage damage = col.GetComponent<IDamage>();
            if (damage != null) damage.OnDamage(curAttackPoint + skillDamage);
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
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path) && !myAnim.GetBool("IsAttack"))
        {
            StopMove();

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
}
