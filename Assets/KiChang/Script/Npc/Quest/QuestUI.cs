using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public LayerMask playerMask;
    public TMPro.TextMeshProUGUI openQuestText;
    public GameObject openQuestButton;

    void Start()
    {
        openQuestButton.SetActive(false);
    }
    public void OnQuestBox()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            openQuestButton.SetActive(true);
        }
        
    }
}
