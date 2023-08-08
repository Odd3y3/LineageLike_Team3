using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
   
    public RectTransform uiGroup;
    public Animator anim;
    public GameObject[] itemobj;
    public int[] itemPrice;
    //public Transform

    Player enterPlayer;
    // Start is called before the first frame update
    void Entert(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Exit()
    {
        uiGroup.anchoredPosition = Vector3.down * 1000;
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Shop")
        {
            Shop shop = GetComponent<Shop>();
            shop.Exit();
            
        }
    }
    public void Buy(int index)
    {

    }
}
