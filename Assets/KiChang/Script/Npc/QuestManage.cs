using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManage : MonoBehaviour
{
    private static QuestManage _instance;
    public static QuestManage Instance {get{ return _instance; } }
    

    public QuestDataObject questdatabase;

    public event Action<QuestObject> OnCompletedQuest;
    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    public void ProcessQuest(QuestType type, int target, int targetId)
    {
        foreach(QuestObject questObject in questdatabase.questObject)
        {
            if(questObject.status == QuestStatus.Accepted
               && questObject.data.type == type
               && questObject.data.targetId == targetId)
            {
                questObject.data.completeCount++;

                if (questObject.data.completeCount >= questObject.data.count)
                {
                    questObject.status = QuestStatus.Completed;
                    OnCompletedQuest?.Invoke(questObject);
                }
            }
        }
    }

}
