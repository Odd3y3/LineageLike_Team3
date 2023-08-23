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

    public SkillInfo(Skill skill = null)
    {
        this.skill = skill;
        curSkillCool = 0.0f;
    }
}

public class PlayerBattleSystem : BattleSystem
{   
    protected enum SkillKey
    {
        Dash,
        QSkill,
        WSkill,
        ESkill
    }

    [SerializeField]
    Skills equippedSkills;

    SkillInfo DashInfo;
    SkillInfo QSkillInfo;
    SkillInfo WSkillInfo;
    SkillInfo ESkillInfo;

    public float RemainDashCool { get => DashInfo.curSkillCool; }
    public float RemainQSkillCool { get => QSkillInfo.curSkillCool; }
    public float RemainWSkillCool { get => WSkillInfo.curSkillCool; }
    public float RemainESkillCool { get => ESkillInfo.curSkillCool; }


    //protected float skillRadius = 0.0f;
    //protected float skillDamage = 0.0f;

    protected bool IsSkillAreaSelecting { get; private set; } = false;

    Skill usingSkill = null;
    Vector3 usingSkillPos = Vector3.zero;

    protected override void Initialize()
    {
        base.Initialize();

        usingSkillPos = transform.position;

        GameManager.Inst.UiManager?.mySkillUI.SetSkillUI(equippedSkills);

        //스킬 초기화
        InitSkill();
    }

    //public new void OnDamage(float dmg, AttackType attackType, Vector3 attackVec, float knockBackDist)
    //{
    //    curHP -= dmg - curDefensePoint;
    //}

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
            //Debug.Log("해당 스킬이 없습니다.");
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
            usingSkill = skillInfo.skill;
            usingSkillPos = effectPos;
        }
        myAnim.SetTrigger(skillInfo.skill.AnimationClip.name);
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
        if (!usingSkill.IsAreaSelect)
            pos = transform.position;

        if(usingSkill.EffectPrefab != null)
            Instantiate(usingSkill.EffectPrefab, pos, transform.rotation);
    }

    public void OnSkillAttack()
    {
        if (usingSkill != null)
        {
            float dmg = usingSkill.TotalDamage(BattleStat.DefaultAttackPoint);
            //usingSkill.damageArea?.Calculate(dmg, transform, enemyMask);
        }
    }
}
