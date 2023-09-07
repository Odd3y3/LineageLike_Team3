using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveData", menuName = "SaveData", order = int.MaxValue)]
public class SaveData : ScriptableObject
{
    //������ �����͵�

    public bool IsEmpty = true;

    public PlayerInfo playerInfo;
}

