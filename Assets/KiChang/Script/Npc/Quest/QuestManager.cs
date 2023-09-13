using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    private static QuestManager _instance;
    public static QuestManager Instance {get{ return _instance; } }
    

    public QuestDataObject questdatabase;

    public event Action<QuestObject> OnCompletedQuest;

    public Transform QuestUI; // UI������Ʈ
    public TMPro.TextMeshProUGUI questTitle; // ����Ʈ����
    public TMPro.TextMeshProUGUI questExplan;// ����Ʈ ����

    public int currentQuestValue = 0; // ���� ����Ʈ ���൵
    public int questClearValue = 0; // ����Ʈ Ŭ���� ����

    private void Awake()
    {
        _instance = this;
    }
    // Start is called before the first frame update
    public void ProcessQuest(QuestType type, int target, int targetId)
    {
        foreach(QuestObject questObject in questdatabase.questObjects)
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
