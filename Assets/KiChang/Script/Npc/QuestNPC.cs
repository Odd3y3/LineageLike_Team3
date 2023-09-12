using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;



public class QuestNPC : MonoBehaviour, IInteractable
{
    public LayerMask playerMask;

    public QuestObject questObject;
    [SerializeField] GameObject QuestEffectGO;
    [SerializeField] GameObject QuestRewardGO;


    public Dialogue readyDialogue;
    public Dialogue acceptedDialogue;
    public Dialogue completedDialogue;

    bool isStartDialogue = false;
    GameObject interactGO = null;
   
    
    private void Start()
    {
        QuestManager.Instance.OnCompletedQuest += OnCompleteQuest;
    }
    private void Update()
    {
        if (questObject.status == QuestStatus.None)
        {
            QuestEffectGO.SetActive(true);
            QuestRewardGO.SetActive(false);
        }
        else if(questObject.status == QuestStatus.Accepted)
        {
            QuestEffectGO.SetActive(false);
            QuestRewardGO.SetActive(false);
        }
        else if (questObject.status == QuestStatus.Completed)
        {
            QuestEffectGO.SetActive(false);
            QuestRewardGO.SetActive(true);
        }
        else if (questObject.status == QuestStatus.Rewarded)
        {
            QuestEffectGO.SetActive(false);
            QuestRewardGO.SetActive(false);
        }
    }

    #region IInteractable Interface
    [SerializeField]
    public float distance = 2.0f;
    public float Distance => distance;

    public Action<QuestObject> OnCompleteQuest { get; private set; }

    public void Interact(GameObject other)
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

        DialogueManager.Instance.OnEndDialogue += OnEndDialogue;
        isStartDialogue = true;
        if(questObject.status == QuestStatus.None)
        {
            DialogueManager.Instance.StartDialogue(readyDialogue);
            questObject.status = QuestStatus.Accepted;
        }
        else if(questObject.status == QuestStatus.Accepted)
        {
            DialogueManager.Instance.StartDialogue(acceptedDialogue);
            
        }
        else if (questObject.status ==QuestStatus.Completed)
        {
            DialogueManager.Instance.StartDialogue(completedDialogue);
         
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
       //if (questObject.data.id == this.questObject.id && questObject.status == QuestStatus.Completed)
      //  {        }
    }
    #endregion Methods

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            Interact(other.gameObject);
        }
        //DialogueManager.Instance.OnEndDialogue += OnEndDialogue;
        //isStartDialogue = true;
        //DialogueManager.Instance.StartDialogue(dialogue);
    }
}
