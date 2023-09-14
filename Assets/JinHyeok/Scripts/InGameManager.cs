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
    /// 인게임에서의 플레이어
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
        //Load Slot 가져오기 ( Save Data )
    }

    //세이브 로드 기능
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


        //저장
        //curSaveSlotNum = GameManager.Inst.curSceneNum;
        saveDatas[curSaveSlotNum].IsEmpty = false;
        saveDatas[curSaveSlotNum].playerInfo = _playerInfo;

        Debug.Log($"{curSaveSlotNum}번 슬롯 저장됨.");
    }

    public void Load(Player player)
    {
        //scriptable object에서 _playerInfo로 정보 가져오기
        _playerInfo = saveDatas[curSaveSlotNum].playerInfo;

        //player 정보 넣기
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
    //맵 (어느 맵에 있었는지)
    public int SceneNum;
    public Vector3 Pos;

    //인벤토리 아이템 정보

    //장비창 아이템 정보

    //골드 정보
    public uint Gold;

    public float CurHP;
    public float CurMP;
    public float CurAttackPoint;
    public float CurDefencePoint;
    public float CurExp;
    public BattleStat BattleStat;

    //플레이 시간?
    public ulong PlayTime;
}