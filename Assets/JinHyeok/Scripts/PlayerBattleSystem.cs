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

    public void ResetCoolTime()
    {
        Dash.currentCoolTime = 0.0f;
        QSkill.currentCoolTime = 0.0f;
        WSkill.currentCoolTime = 0.0f;
        ESkill.currentCoolTime = 0.0f;
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
    
    public Skills equippedSkills;

    protected float skillRadius = 0.0f;
    protected float skillDamage = 0.0f;

    protected bool IsSkillAreaSelecting { get; private set; } = false;


    Skill usingSkill = null;
    Vector3 usingSkillPos = Vector3.zero;

    protected override void Initialize()
    {
        base.Initialize();

        moveCoroutineList = new List<Coroutine>();
        usingSkillPos = transform.position;

        //��Ÿ�� �ʱ�ȭ
        equippedSkills.ResetCoolTime();
    }

    protected void UseSkill(SkillKey skillkey)
    {
        switch (skillkey)
        {
            case SkillKey.Dash:
                UseSkill(equippedSkills.Dash);
                break;
            case SkillKey.QSkill:
                UseSkill(equippedSkills.QSkill);
                break;

            case SkillKey.WSkill:
                UseSkill(equippedSkills.WSkill);
                break;

            case SkillKey.ESkill:
                UseSkill(equippedSkills.ESkill);
                break;
        }
    }

    private void UseSkill(Skill skill)
    {
        if(skill == null)
        {
            Debug.Log("�ش� ��ų�� �����ϴ�.");
            return;
        }
        if(skill.currentCoolTime > 0.0f)
        {
            Debug.Log($"�ش� ��ų�� ��Ÿ�� �� �Դϴ�. ���� ��Ÿ�� : {skill.currentCoolTime}");
            return;
        }


        if(skill.IsAreaSelect)
        {
            //skill.AreaPrefab ����
            StartCoroutine(AreaSelecting(skill));
        }
        else
        {
            AnimateSkill(skill, transform.position);
        }
    }
    IEnumerator AreaSelecting(Skill skill)
    {
        yield return null;

        GameObject areaPrefab = skill.AreaPrefab;

        IsSkillAreaSelecting = true;
        GameObject area = Instantiate(areaPrefab, transform.position, Quaternion.identity);
        while(true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, skillClickMask))
            {
                area.transform.position = hit.point;
            }


            //Ű �Է�
            if (Input.GetMouseButtonDown(0))    //����
            {
                Vector3 areaPos = area.transform.position;
                Destroy(area);
                IsSkillAreaSelecting = false;
                AnimateSkill(skill, areaPos);
                break;
            }
            else if (Input.anyKeyDown)          //���
            {
                Destroy(area);
                IsSkillAreaSelecting = false;
                break;
            }

            yield return null;
        }
    }

    void AnimateSkill(Skill skill, Vector3 effectPos)
    {
        //��Ÿ��
        StartCoroutine(CoolingSkill(skill));

        //�뽬�� ���, ��� ȸ��
        if (skill.IsDash)
            ImmediateRotate();

        StopMoveAndRotate();

        usingSkill = skill;
        usingSkillPos = effectPos;
        myAnim.SetTrigger(skill.AnimationClip.name);
    }

    IEnumerator CoolingSkill(Skill skill)
    {
        skill.currentCoolTime = skill.CoolTime;

        while (skill.currentCoolTime >= 0.0f)
        {
            skill.currentCoolTime -= Time.deltaTime;
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

}
