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
    public LayerMask playerMask;
    // Start is called before the first frame update
    void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    // Update is called once per frame
    void Exit()
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
    }
    public void Buy(int index)
    {

    }
}
