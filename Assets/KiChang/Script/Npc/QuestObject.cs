using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestStatus
{
    None,
    Accepted,
    Completed,
    Rewarded
}

[CreateAssetMenu(fileName = "new Quest", menuName = "Quest system/Quests/ New Quest")]
public class QuestObject : ScriptableObject
{
    public Quest data;
    public QuestStatus status;
}