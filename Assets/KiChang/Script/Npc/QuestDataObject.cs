using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest Database", menuName="Quest System/Quests/New Quest Database")]

public class QuestDataObject : ScriptableObject
{
    public QuestObject[] questObject;
    public void OnValidate()
    {
        for(int index=0; index < questObject.Length; ++index)
        {
            questObject[index].data.id = index;
        }
    }
}
