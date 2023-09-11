using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemobj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;
    public TMPro.TMP_Text talkText;

    Player enterPlayer;
    public LayerMask playerMask;
   
    void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    
    public void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            Enter(other.GetComponent<Player>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & playerMask) != 0)
        {
            Exit();
        }
    }
    public void Buy(int index)
    {
        int price = itemPrice[index];
        /* if(price > enterPlayer.coin) {
             return;
         }*/
    }
   
}
