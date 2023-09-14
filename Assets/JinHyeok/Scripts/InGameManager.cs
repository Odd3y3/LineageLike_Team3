using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public SaveData[] saveDatas;

    public InvenotryData[] InnventoryDatas;
    public EquipmentData[] EquipmentDatas;

    Player _player;
    /// <summary>
    /// �ΰ��ӿ����� �÷��̾�
    /// </summary>
    public Player myPlayer
    {
        get
        {
            if (_player == null)
                _player = FindObjectOfType<Player>();
            return _player;
        }
        set { _player = value; }
    }
    
    public Skills myPlayerSkill
    {
        get
        {
            return myPlayer.GetSkill();
        }
    }

    PlayerInfo _playerInfo;

    //[HideInInspector]
    public int curSaveSlotNum = -1;

    public uint Gold
    {
        get => _playerInfo.Gold;
        set
        {
            _playerInfo.Gold = value;
        }
    }

    public void GoldDrop(uint gold)
    {
        Gold += gold;
        GameManager.Inst.UiManager.myGoodsUI.ChangeCoin(Gold);
    }

    private void Awake()
    {
        //Load Slot �������� ( Save Data )
    }

    //���̺� �ε� ���
    public void Save()
    {
        //PlayerInfo _playerInfo = new PlayerInfo();
        Player player = GameManager.Inst.inGameManager.myPlayer;
        _playerInfo.SceneNum = GameManager.Inst.curSceneNum;
        _playerInfo.Pos = player.transform.position;
        _playerInfo.BattleStat = player.BattleStat;
        _playerInfo.CurHP = player.curHP;
        _playerInfo.CurMP = player.curMP;
        _playerInfo.CurAttackPoint = player.curAttackPoint;
        _playerInfo.CurDefencePoint = player.curDefensePoint;
        _playerInfo.CurExp = player.curExp;
        
        _playerInfo.PlayTime = 0;


        //����
        //curSaveSlotNum = GameManager.Inst.curSceneNum;
        saveDatas[curSaveSlotNum].IsEmpty = false;
        saveDatas[curSaveSlotNum].playerInfo = _playerInfo;

        Debug.Log($"{curSaveSlotNum}�� ���� �����.");
    }

    public void Load(Player player)
    {
        //scriptable object���� _playerInfo�� ���� ��������
        _playerInfo = saveDatas[curSaveSlotNum].playerInfo;

        //player ���� �ֱ�
        if (!saveDatas[curSaveSlotNum].IsEmpty)
        {
            player.transform.position = _playerInfo.Pos;
            player.BattleStat = _playerInfo.BattleStat;
            player.curHP = _playerInfo.CurHP;
            player.curMP = _playerInfo.CurMP;
            player.curAttackPoint = _playerInfo.CurAttackPoint;
            player.curDefensePoint = _playerInfo.CurDefencePoint;
            player.curExp = _playerInfo.CurExp;
        }
    }
}

[Serializable]
public struct PlayerInfo
{
    //�� (��� �ʿ� �־�����)
    public int SceneNum;
    public Vector3 Pos;

    //�κ��丮 ������ ����

    //���â ������ ����

    //��� ����
    public uint Gold;

    public float CurHP;
    public float CurMP;
    public float CurAttackPoint;
    public float CurDefencePoint;
    public float CurExp;
    public BattleStat BattleStat;

    //�÷��� �ð�?
    public ulong PlayTime;
}