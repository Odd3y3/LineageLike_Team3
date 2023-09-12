using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : PlayerBattleSystem
{
    public Item PickUpItem = null;

    Coroutine comboCheckCoroutine;

    public bool CanMove { get; set; } = false;

    GameObject destinationMarker;

    public int Gold = 0;

    private void Awake()
    {
        //if (GameManager.Inst.myPlayer == null)
        //{
        //    GameManager.Inst.myPlayer = this;
        //}
        destinationMarker = Resources.Load<GameObject>("destinationMarker");
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        if (CanMove)
        {
            //대쉬 Space bar
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //StopMove();
                //ImmediateRotate();
                UseSkill(SkillKey.Dash);
            }

            if (!IsSkillAreaSelecting && !myAnim.GetBool("IsDamaged"))
            {
                //기본 공격
                if (Input.GetMouseButton(0) && !myAnim.GetBool("IsAttack")
                    && !EventSystem.current.IsPointerOverGameObject())
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

        }

        if (Input.GetKeyDown(KeyCode.F1))
        {
            LevelUp();
        }
    }

    public void OnMouseClickMove(Vector3 pos)
    {
        if(CanMove && !myAnim.GetBool("IsDamaged") && !EventSystem.current.IsPointerOverGameObject())
        {
            //destination point 생성
            GameObject marker = Instantiate(destinationMarker);
            marker.transform.position = pos;
            //이동
            MovePosByPath(pos);
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

    public void LevelUp()
    {
        curLv++;
        BattleStat.MaxHP += 10;
        BattleStat.MaxMP += 10;
        BattleStat.MaxExp *= 2;
        curAttackPoint += 10;
        curDefensePoint+= 10;
        GameManager.Inst.UiManager.mySkillWindow.GetSkillPoint(curLv);
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

    public Skills setSkill()
    {
        return equippedSkills;
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
            if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                myAnim.SetBool("BaseAttack", true);
                break;
            }
            yield return null;
        }

    }

    public void OnComboCheckEnd()
    {
        if(comboCheckCoroutine != null)
            StopCoroutine(comboCheckCoroutine);
    }

    public void OnDash()
    {
        StartCoroutine(DashCoroutine(0.5f, 3.0f));
    }
    IEnumerator DashCoroutine(float time, float speed)
    {
        float t = 0;
        while (t < time)
        {
            transform.position += transform.forward * speed * Time.deltaTime;

            yield return null;
            t += Time.deltaTime;
        }
    }

    //==============================================================================

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

    public void GoldDrop(int gold)
    {
        Gold += gold;
        GameManager.Inst.UiManager.myGoodsUI.DropCoin(Gold);
    }

}
