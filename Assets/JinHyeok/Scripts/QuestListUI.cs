using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUI : MonoBehaviour
{
    public Transform contents;

    GameObject contentOrigin = null;

    private void Awake()
    {
        contentOrigin = Resources.Load<GameObject>("UI\\QuestListContent");
    }

    public void InitQuestList()
    {
        QuestObject[] questObjects = GameManager.Inst.questManager.questdatabase.questObjects;
        foreach (QuestObject questObject in questObjects)
        {
            if(questObject.status == QuestStatus.Accepted || questObject.status == QuestStatus.Completed)
            {
                CreateContentUI(questObject);
            }
        }
    }

    public void CreateContentUI(QuestObject questObj)
    {
        GameObject obj = Instantiate(contentOrigin, contents);
        obj.GetComponent<QuestListUIContent>().SetQuestObj(questObj);
    }
}