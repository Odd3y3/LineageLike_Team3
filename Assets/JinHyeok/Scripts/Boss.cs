using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : BattleSystem
{
    private void Start()
    {
        base.Initialize();

        myTarget = GameManager.Inst.inGameManager.myPlayer.transform;
    }

    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        //���� ���ʹ� ��������
        float damage = dmg - curDefensePoint;
        damage = damage <= 1 ? 1 : damage;
        curHP -= damage;

        BattleManager.DamagePopup(transform, damage);

        if (!IsLive)
        {
            //���� ����, Ŭ����
            //�ƾ�, ���� �״� �ִϸ��̼�
            //�ƾ� ��, ������ ���, �� ������ ����, �ⱸ ����
        }
    }

    //�̵� �� ����

}
