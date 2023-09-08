using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestNPC : MonoBehaviour
{
    public QuestObject questObject;

    public Dialogue readyDialogue;
    public Dialogue acceptedDialogue;
    public Dialogue completedDialogue;

    bool isStartDialogue = false;
    GameObject interactGO = null;
    private void Start()
    {
        QuestManage.Instance.OnCompletedQuest += OnCompleteQuest;

    }

    #region IInteractable Interface
    [SerializeField]
    public float distance = 2.0f;
    public float Distance => distance;

    public Action<QuestObject> OnCompleteQuest { get; private set; }

    public void Intetact(GameObject other)
    {
        float calcDistance = Vector3.Distance(other.transform.position, transform.position);
        if (calcDistance > distance)
        {
            return;
        }
        if (isStartDialogue)
        {
            return;
        }
        this.interactGO = other;

        DialogueManage.Instance.OnEndDialogue += OnEndDialogue;
        isStartDialogue = true;
        if(questObject.status == QuestStatus.None)
        {
            DialogueManage.Instance.StartDialogue(readyDialogue);
            questObject.status = QuestStatus.Accepted;
        }
        else if(questObject.status == QuestStatus.Accepted)
        {
            DialogueManage.Instance.StartDialogue(acceptedDialogue);
        }

        else if (questObject.status ==QuestStatus.Completed)
        {
            DialogueManage.Instance.StartDialogue(completedDialogue);
         
            questObject.status = QuestStatus.Rewarded;
        }
     }
    public void StopInteract(GameObject other)
    {
        isStartDialogue = false;
    }
    #endregion IInteractable interface
    #region Methods
    private void OnEndDialogue()
    {
        StopInteract(interactGO);
    }
    private void OnCompletedQuest(GameObject questObject)
    {

    }
    #endregion Methods

}
