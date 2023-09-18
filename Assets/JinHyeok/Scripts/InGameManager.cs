using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    [HideInInspector]
    public SaveData[] saveDatas = new SaveData[3];

    QuestDataObject[] questDataObj = new QuestDataObject[3];

    public InventoryData[] InventoryDatas= new InventoryData[20];
    public EquipmentData[] EquipmentDatas=new EquipmentData[4];
    public ConsumptionItemData[] ConsumptionItemDatas = new ConsumptionItemData[3];

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
        //Load Slot 가져오기 ( Save Data ), + 퀘스트 Slot
        for(int i = 0; i < 3; i++)
        {
            saveDatas[i] = Resources.Load<SaveData>($"SaveData{i}");

            questDataObj[i] = Resources.Load<QuestDataObject>($"SaveSlot{i}\\Quest Database");
        }


        //FileManager에서 데이터 가져오기
        SerializedSaveData saveData0 = FileManager.LoadBinary<SerializedSaveData>("saveData0.sav");
        if (saveData0 != null)
        {
            saveData0.ToSaveData(saveDatas[0]);
            for(int i = 0; i < saveData0.serializedQuestDatas.Length; ++i)
            {
                questDataObj[0].questObjects[i].data.completeCount = saveData0.serializedQuestDatas[i].completeCount;
                questDataObj[0].questObjects[i].status = saveData0.serializedQuestDatas[i].status;
            }
        }

        SerializedSaveData saveData1 = FileManager.LoadBinary<SerializedSaveData>("saveData1.sav");
        if (saveData1 != null)
        {
            saveData1.ToSaveData(saveDatas[1]);
            for (int i = 0; i < saveData1.serializedQuestDatas.Length; ++i)
            {
                questDataObj[1].questObjects[i].data.completeCount = saveData1.serializedQuestDatas[i].completeCount;
                questDataObj[1].questObjects[i].status = saveData1.serializedQuestDatas[i].status;
            }
        }

        SerializedSaveData saveData2 = FileManager.LoadBinary<SerializedSaveData>("saveData2.sav");
        if (saveData2 != null)
        {
            saveData2.ToSaveData(saveDatas[2]);
            for (int i = 0; i < saveData2.serializedQuestDatas.Length; ++i)
            {
                questDataObj[2].questObjects[i].data.completeCount = saveData2.serializedQuestDatas[i].completeCount;
                questDataObj[2].questObjects[i].status = saveData2.serializedQuestDatas[i].status;
            }
        }
    }

    //세이브 로드 기능
    public void Save()
    {
        SaveData _saveData = saveDatas[curSaveSlotNum];
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

        _playerInfo.Gold = Gold;

        _playerInfo.SkillPoint = player.SkillPoint;
        _playerInfo.QSkillLV = player.QSkillInfo.skillLV;
        _playerInfo.WSkillLV = player.WSkillInfo.skillLV;
        _playerInfo.ESkillLV = player.ESkillInfo.skillLV;

        InventoryDatas = GameManager.Inst.UiManager.myInventory.GetInventoryData();
        for(int i = 0; i < 20; i++)
        {
            _saveData.InventoryDatas[i] = InventoryDatas[i];
        }
        EquipmentDatas = GameManager.Inst.UiManager.myEquipment.GetEquipmentData();
        for (int i = 0; i < 4; i++)
        {
            _saveData.EquipmentDatas[i] = EquipmentDatas[i];
        }
        ConsumptionItemDatas = GameManager.Inst.UiManager.myConsumptionItem.GetConsumptionItemData();
        for (int i = 0; i < 3; i++)
        {
            _saveData.ConsumptionItemDatas[i] = ConsumptionItemDatas[i];
        }
        //저장
        //curSaveSlotNum = GameManager.Inst.curSceneNum;
        saveDatas[curSaveSlotNum].IsEmpty = false;
        saveDatas[curSaveSlotNum].playerInfo = _playerInfo;

        //퀘스트정보
        for(int i = 0; i < saveDatas[curSaveSlotNum].serializedQuestDatas.Length; ++i)
        {
            saveDatas[curSaveSlotNum].serializedQuestDatas[i].questID = questDataObj[curSaveSlotNum].questObjects[i].data.id;
            saveDatas[curSaveSlotNum].serializedQuestDatas[i].completeCount
                = questDataObj[curSaveSlotNum].questObjects[i].data.completeCount;
            saveDatas[curSaveSlotNum].serializedQuestDatas[i].status
                = questDataObj[curSaveSlotNum].questObjects[i].status;
        }

        FileManager.SaveBinary($"saveData{curSaveSlotNum}.sav", new SerializedSaveData(saveDatas[curSaveSlotNum]));

        Debug.Log($"{curSaveSlotNum}번 슬롯 저장됨.");
    }

    public void Load(Player player)
    {
        //scriptable object에서 _playerInfo로 정보 가져오기
        SaveData _saveData = saveDatas[curSaveSlotNum];
        _playerInfo = _saveData.playerInfo;

        //player 정보 넣기
        if (!saveDatas[curSaveSlotNum].IsEmpty)
        {
            GameManager.Inst.UiManager.myInventory.SetInventoryData(_saveData.InventoryDatas);
            GameManager.Inst.UiManager.myEquipment.SetEquipmentData(_saveData.EquipmentDatas);
            GameManager.Inst.UiManager.myConsumptionItem.SetConsumptionItemData(_saveData.ConsumptionItemDatas);

            player.transform.position = _playerInfo.Pos;
            player.BattleStat = _playerInfo.BattleStat;
            player.curHP = _playerInfo.CurHP;
            player.curMP = _playerInfo.CurMP;
            player.curAttackPoint = _playerInfo.CurAttackPoint;
            player.curDefensePoint = _playerInfo.CurDefencePoint;
            player.curExp = _playerInfo.CurExp;
            player.SkillPoint = _playerInfo.SkillPoint;
            player.QSkillInfo.skillLV = _playerInfo.QSkillLV;
            player.WSkillInfo.skillLV = _playerInfo.WSkillLV;
            player.ESkillInfo.skillLV = _playerInfo.ESkillLV;
            
            Gold = _playerInfo.Gold;
            GameManager.Inst.UiManager.myGoodsUI.ChangeCoin(_playerInfo.Gold);
        }
    }
}

[System.Serializable]
public struct PlayerInfo
{
    //맵 (어느 맵에 있었는지)
    public int SceneNum;
    public Vector3 Pos;

    
    //골드 정보
    public uint Gold;

    public float CurHP;
    public float CurMP;
    public float CurAttackPoint;
    public float CurDefencePoint;
    public float CurExp;
    public BattleStat BattleStat;

    public int SkillPoint;
    public int QSkillLV;
    public int WSkillLV;
    public int ESkillLV;

    //플레이 시간?
    public ulong PlayTime;
}