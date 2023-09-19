using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Skills
{
    public Skill Dash;
    public Skill QSkill;
    public Skill WSkill;
    public Skill ESkill;
}

public class SkillInfo
{
    public Skill skill;
    public float curSkillCool;
    public int skillLV;

    public SkillInfo(Skill skill = null)
    {
        this.skill = skill;
        curSkillCool = 0.0f;
        skillLV = 1;
    }
}

public class PlayerBattleSystem : BattleSystem
{
    public bool CanMove { get; set; } = true;

    protected enum SkillKey
    {
        Dash,
        QSkill,
        WSkill,
        ESkill
    }

    [SerializeField]
    protected Skills equippedSkills;

    public SkillInfo DashInfo;
    public SkillInfo QSkillInfo;
    public SkillInfo WSkillInfo;
    public SkillInfo ESkillInfo;

    public int SkillPoint { get; set; } = 0;

    public float RemainDashCool { get => DashInfo.curSkillCool; }
    public float RemainQSkillCool { get => QSkillInfo.curSkillCool; }
    public float RemainWSkillCool { get => WSkillInfo.curSkillCool; }
    public float RemainESkillCool { get => ESkillInfo.curSkillCool; }


    //protected float skillRadius = 0.0f;
    //protected float skillDamage = 0.0f;

    protected bool IsSkillAreaSelecting { get; private set; } = false;

    public GameObject levelUpEffect;

    SkillInfo usingSkill = null;
    Vector3 usingSkillPos = Vector3.zero;
    public SkillInfo UsingSkill
    {
        get => usingSkill;
    }

    protected override void Initialize()
    {
        base.Initialize();

        usingSkillPos = transform.position;

        //스킬 초기화
        InitSkill();
    }


    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        if (!myAnim.GetBool("IsImmunity"))
        {
            base.OnDamage(dmg, attackVec, knockBackDist, isDown);
            if (!IsLive)
            {
                //Game Over
                GameManager.Inst.GameOver();
                PlayerDead();
            }
        }
    }

    public void LevelUp()
    {
        levelUpEffect.SetActive(true);

        BattleStat.LV++;
        BattleStat.MaxExp *= 2;
        curExp = 0;
        BattleStat.MaxHP += 10;
        curHP += 10;
        BattleStat.MaxMP += 10;
        curMP += 10;
        curAttackPoint += 10;
        curDefensePoint += 10;

        SkillPoint += 3;
        GameManager.Inst.UiManager.mySkillWindow.ChangeInfo();
    }

    void PlayerDead()
    {
        CanMove = false;
        myAnim.SetTrigger("Die");
        myAnim.SetBool("IsImmunity", true);
    }

    void InitSkill()
    {
        DashInfo = new SkillInfo(equippedSkills.Dash);
        QSkillInfo = new SkillInfo(equippedSkills.QSkill);
        WSkillInfo = new SkillInfo(equippedSkills.WSkill);
        ESkillInfo = new SkillInfo(equippedSkills.ESkill);
    }

    protected void UseSkill(SkillKey skillkey)
    {
        switch (skillkey)
        {
            case SkillKey.Dash:
                UseSkill(DashInfo);
                break;
            case SkillKey.QSkill:
                UseSkill(QSkillInfo);
                break;

            case SkillKey.WSkill:
                UseSkill(WSkillInfo);
                break;

            case SkillKey.ESkill:
                UseSkill(ESkillInfo);
                break;
        }
    }

    private void UseSkill(SkillInfo skillInfo)
    {
        if(skillInfo.skill == null)
        {
            Debug.Log("해당 스킬이 없습니다.");
            return;
        }
        if(skillInfo.curSkillCool > 0.0f)
        {

            //Debug.Log($"해당 스킬이 쿨타임 중 입니다. 남은 쿨타임 : {skillInfo.curSkillCool}");
            return;
        }


        if(skillInfo.skill.IsAreaSelect)
        {
            //skill.AreaPrefab 생성
            StartCoroutine(AreaSelecting(skillInfo));
        }
        else
        {
            AnimateSkill(skillInfo, transform.position);
        }
        GameManager.Inst.SoundManager.OnSkillEffectSound(skillInfo);
    }
    IEnumerator AreaSelecting(SkillInfo skillInfo)
    {
        yield return null;

        GameObject areaPrefab = skillInfo.skill.AreaPrefab;

        IsSkillAreaSelecting = true;
        GameObject area = Instantiate(areaPrefab, transform.position, Quaternion.identity);
        while(true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, skillClickMask))
            {
                area.transform.position = hit.point;
            }


            //키 입력
            if (Input.GetMouseButtonDown(0))    //선택
            {
                Vector3 areaPos = area.transform.position;
                Destroy(area);
                IsSkillAreaSelecting = false;
                AnimateSkill(skillInfo, areaPos);
                break;
            }
            else if (Input.anyKeyDown)          //취소
            {
                Destroy(area);
                IsSkillAreaSelecting = false;
                break;
            }

            yield return null;
        }
    }

    void AnimateSkill(SkillInfo skillInfo, Vector3 effectPos)
    {
        //쿨타임
        StartCoroutine(CoolingSkill(skillInfo));

        //대쉬일 경우, 즉시 회전
        if (skillInfo.skill.IsDash)
            ImmediateRotate();

        StopMoveAndRotate();

        if (!skillInfo.skill.IsDash)
        {
            usingSkill = skillInfo;
            usingSkillPos = effectPos;
        }
        myAnim.SetTrigger(skillInfo.skill.AnimationClip.name);

        myAnim.SetBool("IsAttack", true);
    }

    IEnumerator CoolingSkill(SkillInfo skillInfo)
    {
        skillInfo.curSkillCool = skillInfo.skill.CoolTime;

        GameManager.Inst.UiManager.mySkillUI.CoolTimeSkill(skillInfo);

        while (skillInfo.curSkillCool >= 0.0f)
        {
            skillInfo.curSkillCool -= Time.deltaTime;
            yield return null;
        }
    }

    public void OnSkillEffectStart()
    {
        Vector3 pos = usingSkillPos;
        if (!usingSkill.skill.IsAreaSelect)
            pos = transform.position;

        if(usingSkill.skill.EffectPrefab != null)
            Instantiate(usingSkill.skill.EffectPrefab, pos, transform.rotation);
    }

    public void OnSkillAttack()
    {
        if (usingSkill != null)
        {
            //usingSkill.SkillAttack(curAttackPoint, transform, enemyMask);
            StartCoroutine(SkillAttackCoroutine(usingSkill));
        }
    }
    IEnumerator SkillAttackCoroutine(SkillInfo usingSkill)
    {
        var t = new WaitForSeconds(usingSkill.skill.AttackInterval);
        for(int i = 0; i < usingSkill.skill.AttackCount; i++)
        {
            usingSkill.skill.SkillAttack(curAttackPoint, UsingSkill.skillLV, transform, enemyMask);
            yield return t;
        }
    }
}
