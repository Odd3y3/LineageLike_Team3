using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData", order = int.MaxValue)]
public class SaveData : ScriptableObject
{
    //������ �����͵�

    public bool IsEmpty = true;

    public PlayerInfo playerInfo;

    //�κ��丮 ������ ����
    public InventoryData[] InventoryDatas = new InventoryData[20];
    //���â ������ ����
    public EquipmentData[] EquipmentDatas = new EquipmentData[4];
    //�Һ� ������ ���� 
    public ConsumptionItemData[] ConsumptionItemDatas = new ConsumptionItemData[3];
}

