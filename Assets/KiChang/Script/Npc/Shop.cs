using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemobj;
    public int[] itemPrice;

    Player enterplayer;
    public LayerMask playerMask;

    void Enter(Player player)
    {
        enterplayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    
    public void Exit()
    {
        
        uiGroup.anchoredPosition = Vector3.down * 1000;
  
    }
    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask) != 0)
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
        //gameObject.SetActive(false);

    }
    public void Buy(int index)
    {
        int price = itemPrice[index];
       /* if(price > enterPlayer.coin)
        {
            return;
        }*/
    }
}
