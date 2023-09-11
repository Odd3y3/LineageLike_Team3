using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour, IInteractable
{
    public LayerMask playerMask;

    [SerializeField]
    Dialogue dialogue;

    bool isStartDialogue = false;

    GameObject interactGO;

    [SerializeField]
    float distance = 2.0f;

    public float Distance => distance;

    public void Interact(GameObject other)
    {
        float calcDistance = Vector3.Distance(other.transform.position, transform.position);
        if (calcDistance>distance) {
            return; 
        }
        if (isStartDialogue)
        {
            return;
        }
        interactGO = other;

        DialogueManager.Instance.OnEndDialogue += OnEndDialogue;
        isStartDialogue = true;
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    public void StopInteract(GameObject other)
    {
        isStartDialogue = false;
    }
    private void OnEndDialogue()
    {
        StopInteract(interactGO);
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            DialogueManager.Instance.OnEndDialogue += OnEndDialogue;
            isStartDialogue = true;
            DialogueManager.Instance.StartDialogue(dialogue);
        }
        //DialogueManager.Instance.OnEndDialogue += OnEndDialogue;
        //isStartDialogue = true;
        //DialogueManager.Instance.StartDialogue(dialogue);
    }

}
