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
}

