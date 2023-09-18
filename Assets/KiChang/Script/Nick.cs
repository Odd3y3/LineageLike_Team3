using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Nick : MonoBehaviour
{
    public Transform myTarget;
    public GameObject QuestEffectGO;
    public GameObject QuestRewardGO;


    TMPro.TextMeshProUGUI myText;

    public QuestObject myQuestObject;  

    private void Awake()
    {
        myText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        if (myTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTarget.position + new Vector3(0, 2f, 0));
        }
        if(myQuestObject != null)
        {
            if (myQuestObject.status == QuestStatus.None)
            {
                QuestEffectGO.SetActive(true);
                QuestRewardGO.SetActive(false);

            }
            else if (myQuestObject.status == QuestStatus.Accepted)
            {
                QuestEffectGO.SetActive(false);
                QuestRewardGO.SetActive(false);
            }
            else if (myQuestObject.status == QuestStatus.Completed)
            {
                QuestEffectGO.SetActive(false);
                QuestRewardGO.SetActive(true);
            }
            else if (myQuestObject.status == QuestStatus.Rewarded)
            {
                QuestEffectGO.SetActive(false);
                QuestRewardGO.SetActive(false);
            }
        }
    }

    public void SetTarget(Transform target, string str, Color textColor)
    {
        myTarget = target;
        myText.text = str;
        myText.color = textColor;
    }
    public void SetQuestObj(QuestObject questObject)
    {
        myQuestObject = questObject;
    }
}
