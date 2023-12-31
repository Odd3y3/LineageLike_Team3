using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    private static DialogueManager _instance;
    public static DialogueManager Instance { get { return _instance; } }
    
    public TMPro.TextMeshProUGUI nameText;
    public TMPro.TextMeshProUGUI dialogueText;
    public GameObject nextButton;
    public GameObject acceptButton;
    public QuestRewardWindow questRd;

    public Animator animator = null;

    private Queue <string> sentences;

    public event Action OnStartDialogue;
    public event Action OnEndDialogue;

    QuestNPC curQNPC = null;
    

    private void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }
    public void StartDialogue(Dialogue dialogue, QuestNPC Qnpc = null)
    {
        if(Qnpc != null)
            curQNPC = Qnpc;

        nextButton.SetActive(true);
        acceptButton.SetActive(false);
        questRd.HideReward();

        OnStartDialogue?.Invoke();
        animator?.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            //OnEndDialogue();
            EndDialogue();
            return;
        }
        else if(sentences.Count == 1)
        {
            nextButton.SetActive(false);
            acceptButton.SetActive(true);

            //퀘스트 보상 표시
            if(curQNPC != null && curQNPC.questObject.status == QuestStatus.Completed)
            {
                questRd.SetReward(curQNPC.questObject.data.rewardExp, curQNPC.questObject.data.rewardGold
                    , curQNPC.questObject.data.rewardItems);
            }
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(Typingsentence(sentence));
    }
    IEnumerator Typingsentence(string sentence)
    {
        dialogueText.text = string.Empty;
            yield return new WaitForSeconds(0.25f);
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        animator?.SetBool("IsOpen", false);
        OnEndDialogue?.Invoke();
    }

    public void OnAccept()
    {
        if(curQNPC != null)
        {
            curQNPC.Accept();
        }
    }
}
