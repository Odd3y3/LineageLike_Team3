using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Player : PlayerBattleSystem, ICutScene
{
    public Item PickUpItem = null;

    Coroutine comboCheckCoroutine;

    

    GameObject destinationMarker;

    public int Gold = 0;

    private void Awake()
    {
        //if (GameManager.Inst.myPlayer == null)
        //{
        //    GameManager.Inst.myPlayer = this;
        //}
        destinationMarker = Resources.Load<GameObject>("destinationMarker");

        Initialize();
    }

    void Start()
    {
        //Initialize();
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
                if (Input.GetMouseButtonDown(0) && !myAnim.GetBool("IsAttack")
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

        //Test용
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
        BattleStat.LV++;
        BattleStat.MaxHP += 10;
        curHP += 10;
        BattleStat.MaxMP += 10;
        curMP += 10;
        BattleStat.MaxExp *= 2;
        curExp = 0;
        curAttackPoint += 10;
        curDefensePoint+= 10;
        GameManager.Inst.UiManager.mySkillWindow.GetSkillPoint(BattleStat.LV);
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

    public Skills GetSkill()
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


    //대쉬 거리 증가
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

    //컷씬 관련
    public void OnStartCutScene()
    {
        CanMove = false;
        myAnim.SetBool("IsMove", false);
        StopMoveAndRotate();
    }

    public void OnEndCutScene()
    {
        CanMove = true;
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
        statList[0].text = BattleStat.LV.ToString();
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
