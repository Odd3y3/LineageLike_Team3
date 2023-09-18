using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    //private static QuestManager _instance;
    //public static QuestManager Instance {get{ return _instance; } }

    public QuestDataObject questdatabase;

    public event Action<QuestObject> OnCompletedQuest;

    public Transform QuestUI; // UI¿ÀºêÁ§Æ®
    public TMPro.TextMeshProUGUI questTitle; // Äù½ºÆ®Á¦¸ñ
    public TMPro.TextMeshProUGUI questExplan;// Äù½ºÆ® ¼³¸í

    public int currentQuestValue = 0; // ÇöÀç Äù½ºÆ® ÁøÇàµµ
    public int questClearValue = 0; // Äù½ºÆ® Å¬¸®¾î Á¶°Ç

    private void Awake()
    {
        //_instance = this;
    }

    public void InitQuestDatabase(int curSaveSlotNum)
    {
        questdatabase = Resources.Load<QuestDataObject>($"SaveSlot{curSaveSlotNum}\\Quest Database");
    }
    public void ResetQuestData()
    {
        foreach (QuestObject questObject in questdatabase.questObjects)
        {
            questObject.status = QuestStatus.None;
            questObject.data.completeCount = 0;
        }
    }

    public void ProcessQuest(QuestType type, int targetId)
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

                questObject.questListUIContent?.ChangeInfo();
            }
        }
    }

    /// <summary>
    /// Äù½ºÆ® º¸»ó ¹Þ´Â ÇÔ¼ö
    /// </summary>
    public void QuestReward(QuestObject questObject)
    {
        Player player = GameManager.Inst.inGameManager.myPlayer;
        //°æÇèÄ¡ È¹µæ
        player.DropExp(questObject.data.rewardExp);
        //°ñµå È¹µæ
        GameManager.Inst.inGameManager.GoldDrop((uint)questObject.data.rewardGold);
        //¾ÆÀÌÅÛ È¹µæ
        foreach (Item item in questObject.data.rewardItems)
        {
            GameManager.Inst.UiManager.myInventory.AcquireItem(item);
        }
    }
}
