using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public Item[] saleItems;
    public int[] itemPrice;
    public string[] SaleName;
    public string[] talkData;
    public TMPro.TMP_Text talkText;

    Player enterPlayer;
    public LayerMask playerMask;

    bool isActive = false;
   
    void Enter(Player player)
    {
        if(!isActive)
        {
            enterPlayer = player;
            uiGroup.anchoredPosition = Vector3.zero;
            isActive = true;
        }
    }

    
    public void Exit()
    {
        if(isActive)
        {
            uiGroup.anchoredPosition = Vector3.down * 1000;
            isActive = false;
        }

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
        Item SaleItem = saleItems[index];
        int price = itemPrice[index];
        uint ChangeGold = 0;
        
        if(GameManager.Inst.inGameManager.Gold >= price)
        {
            ChangeGold= GameManager.Inst.inGameManager.Gold -= (uint)price;
            GameManager.Inst.UiManager.myInventory.AcquireItem(SaleItem);
            GameManager.Inst.UiManager.myGoodsUI.ChangeCoin(ChangeGold);
        }
    }
}
