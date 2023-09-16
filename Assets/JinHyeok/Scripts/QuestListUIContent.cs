using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestListUIContent : MonoBehaviour
{
    QuestObject questObj = null;

    public TMPro.TextMeshProUGUI title;
    public TMPro.TextMeshProUGUI description;
    public GameObject completeImage;

    public void SetQuestObj(QuestObject questObj)
    {
        this.questObj = questObj;
        this.questObj.questListUIContent = this;
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        if(questObj != null)
        {
            title.text = questObj.data.title;
            description.text = $"{questObj.data.description} < {questObj.data.completeCount} / {questObj.data.count} >";
            if(questObj.status == QuestStatus.Completed )
            {
                completeImage.SetActive(true);
            }
            else if(questObj.status == QuestStatus.Rewarded)
            {
                Destroy(gameObject);
            }
        }
    }
}
