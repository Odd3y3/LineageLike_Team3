using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Player : PlayerBattleSystem
{
    public Item PickUpItem = null;
    public Transform myAttackArea = null;
    public LayerMask enemyMask;
    Coroutine comboCheckCoroutine;

    private void Awake()
    {
        GameManager.Inst.myPlayer = this;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        //데미지
        //이펙트


        //대쉬 Space bar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //StopMove();
            //ImmediateRotate();
            UseSkill(SkillKey.Dash);
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
            if (Input.GetKeyDown(KeyCode.W) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.WSkill);
            }
            if (Input.GetKeyDown(KeyCode.E) && !myAnim.GetBool("IsAttack"))
            {
                UseSkill(SkillKey.ESkill);
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            OnDamage(10);
        }
    }

    public new void OnDamage(float dmg)
    {
        curHP -= dmg;
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

    public void OnBaseAttack()
    {
        BattleManager.AttackCircle(myAttackArea.position, 1.0f, enemyMask);
        
    }

    //public void OnSkill()
    //{

    //        Collider[] myCols = Physics.OverlapSphere(transform.position, skillRadius, enemyMask);
    //    foreach (Collider col in myCols)
    //    {
    //        IDamage damage = col.GetComponent<IDamage>();
    //        if (damage != null) damage.OnDamage(curAttackPoint + skillDamage);
    //    }
    //}


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

    public void OnAcquisition(Item acquisitionItem) 
    {
        GameManager.Inst.UiManager.myInventory.AcquireItem(acquisitionItem);
    }

    public void OnEquipItem(Item EquipmentItem)
    {
        if(EquipmentItem != null)
        {
            if (EquipmentItem.EquipmentType == Item.EQUIPMENTTYPE.Weapon)
            {
                curAttackPoint += EquipmentItem.StatPoint;
            }
            else
            {
                curDefensePoint += EquipmentItem.StatPoint;
            }
        }
    }
    public void OnUnmountITem(Item EquipmentItem)
    {
        if (EquipmentItem != null)
        {
            if (EquipmentItem.EquipmentType == Item.EQUIPMENTTYPE.Weapon)
            {
                curAttackPoint -= EquipmentItem.StatPoint;
            }
            else
            {
                curDefensePoint -= EquipmentItem.StatPoint;
            }
        }
    }

    public void OnUsePotion(Item Item)
    {
        if(Item != null)
        {
            if (Item.PotionType == Item.POTIONTYPE.Hp)
            {
                curHP += Item.StatPoint;
            }
            else
            {
                curMP += Item.StatPoint;
            }
        }
    }

    

    public void SetStatus(TMPro.TMP_Text[] statList)
    {
        statList[0].text = curLv.ToString();
        statList[1].text = BattleStat.MaxHP.ToString();
        statList[2].text = BattleStat.MaxMP.ToString();
        statList[3].text = curAttackPoint.ToString();
        statList[4].text = curDefensePoint.ToString();
    }

    
}
