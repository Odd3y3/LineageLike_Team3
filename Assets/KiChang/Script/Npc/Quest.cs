using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestType
 {
    DestroyEnemy,
    AcquireItem
}

[Serializable]
public struct Quest
{
    
    public int id;
    public QuestType type;
    public int targetId;
    public int count;
    public int completeCount;

    public int rewardExp;
    public int rewardGold;
    public int rewardItemId;

    public string title;
    public string description;
}
