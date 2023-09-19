using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData", order = int.MaxValue)]
public class SaveData : ScriptableObject
{
    //저장할 데이터들

    public bool IsEmpty = true;

    public PlayerInfo playerInfo;

    //인벤토리 아이템 정보
    public InventoryData[] InventoryDatas = new InventoryData[20];
    //장비창 아이템 정보
    public EquipmentData[] EquipmentDatas = new EquipmentData[4];
    //소비 아이템 정보 
    public ConsumptionItemData[] ConsumptionItemDatas = new ConsumptionItemData[3];

    public SerializedQuestData[] serializedQuestDatas = new SerializedQuestData[2];
}

[Serializable]
public class SerializedSaveData
{
    public bool IsEmpty = true;

    //맵 (어느 맵에 있었는지)
    public int SceneNum;
    public float PosX;
    public float PosY;
    public float PosZ;


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

    //인벤토리 아이템 정보
    public SerializedInvenData[] InventoryDatas = new SerializedInvenData[20];
    //장비창 아이템 정보
    public SerializedEquipData[] EquipmentDatas = new SerializedEquipData[4];
    //소비 아이템 정보 
    public SerializedComsumData[] ConsumptionItemDatas = new SerializedComsumData[3];

    public SerializedQuestData[] serializedQuestDatas = new SerializedQuestData[2];

    public SerializedSaveData(SaveData saveData)
    {
        this.IsEmpty = saveData.IsEmpty;

        this.SceneNum = saveData.playerInfo.SceneNum;
        this.PosX = saveData.playerInfo.Pos.x;
        this.PosY = saveData.playerInfo.Pos.y;
        this.PosZ = saveData.playerInfo.Pos.z;
        this.Gold = saveData.playerInfo.Gold;
        this.CurHP = saveData.playerInfo.CurHP;
        this.CurMP = saveData.playerInfo.CurMP;
        this.CurAttackPoint = saveData.playerInfo.CurAttackPoint;
        this.CurDefencePoint = saveData.playerInfo.CurDefencePoint;
        this.CurExp = saveData.playerInfo.CurExp;
        this.BattleStat = saveData.playerInfo.BattleStat;
        this.SkillPoint = saveData.playerInfo.SkillPoint;
        this.QSkillLV = saveData.playerInfo.QSkillLV;
        this.WSkillLV = saveData.playerInfo.WSkillLV;
        this.ESkillLV = saveData.playerInfo.ESkillLV;
        this.PlayTime = saveData.playerInfo.PlayTime;


        for (int i = 0; i < 20; ++i)
        {
            InventoryDatas[i].ItemID = saveData.InventoryDatas[i].Item?.itemID;
            InventoryDatas[i].ItemCount = saveData.InventoryDatas[i].ItemCount;
        }
        for(int i = 0; i < 4; ++i)
        {
            EquipmentDatas[i].ItemID = saveData.EquipmentDatas[i].Equipment?.itemID;
            EquipmentDatas[i].itemType = (int)saveData.EquipmentDatas[i].EquipmentType;
        }
        for(int i = 0; i < 3; ++i)
        {
            ConsumptionItemDatas[i].ItemID = saveData.ConsumptionItemDatas[i].Item?.itemID;
            ConsumptionItemDatas[i].ItemCount = saveData.ConsumptionItemDatas[i].ItemCount;
        }
        for(int i = 0; i < serializedQuestDatas.Length; ++i)
        {
            serializedQuestDatas[i] = saveData.serializedQuestDatas[i];
        }
    }

    public void ToSaveData(SaveData saveData)
    {
        saveData.IsEmpty = this.IsEmpty;
        saveData.playerInfo.SceneNum = this.SceneNum;
        saveData.playerInfo.Pos = new Vector3(PosX, PosY, PosZ);
        saveData.playerInfo.Gold = this.Gold;
        saveData.playerInfo.CurHP = this.CurHP;
        saveData.playerInfo.CurMP = this.CurMP;
        saveData.playerInfo.CurAttackPoint = this.CurAttackPoint;
        saveData.playerInfo.CurDefencePoint = this.CurDefencePoint;
        saveData.playerInfo.CurExp = this.CurExp;
        saveData.playerInfo.BattleStat = this.BattleStat;
        saveData.playerInfo.SkillPoint = this.SkillPoint;
        saveData.playerInfo.QSkillLV = this.QSkillLV;
        saveData.playerInfo.WSkillLV = this.WSkillLV;
        saveData.playerInfo.ESkillLV = this.ESkillLV;
        saveData.playerInfo.PlayTime = this.PlayTime;
        for(int i = 0; i < 20; ++i)
        {
            if (InventoryDatas[i].ItemID != null)
                saveData.InventoryDatas[i].Item = GameManager.Inst.itemDataBase.FindItem((int)InventoryDatas[i].ItemID);
            saveData.InventoryDatas[i].ItemCount = InventoryDatas[i].ItemCount;
        }
        for (int i = 0; i < 4; ++i)
        {
            if (EquipmentDatas[i].ItemID != null)
                saveData.EquipmentDatas[i].Equipment = GameManager.Inst.itemDataBase.FindItem((int)EquipmentDatas[i].ItemID);
            saveData.EquipmentDatas[i].EquipmentType = (Item.EQUIPMENTTYPE)EquipmentDatas[i].itemType;
        }
        for (int i = 0; i < 3; ++i)
        {
            if (ConsumptionItemDatas[i].ItemID != null) 
                saveData.ConsumptionItemDatas[i].Item = GameManager.Inst.itemDataBase.FindItem((int)ConsumptionItemDatas[i].ItemID);
            saveData.ConsumptionItemDatas[i].ItemCount = ConsumptionItemDatas[i].ItemCount;
        }
        for (int i = 0; i < serializedQuestDatas.Length; i++)
        {
            saveData.serializedQuestDatas[i] = serializedQuestDatas[i];
        }
    }
}
[Serializable]
public struct SerializedInvenData
{
    public int? ItemID;
    public int ItemCount;
}
[Serializable]
public struct SerializedEquipData
{
    public int? ItemID;
    public int itemType;
}
[Serializable]
public struct SerializedComsumData
{
    public int? ItemID;
    public int ItemCount;
}
[Serializable]
public struct SerializedQuestData
{
    public int questID;
    public int completeCount;
    public QuestStatus status;
}