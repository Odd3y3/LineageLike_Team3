using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickUp : MonoBehaviour
{
    // Start is called before the first frame update
    public Item myItem = null;
    void Start()
    {
        myItem = GameManager.Inst.ItemManager.ItemList.Items[Random.Range(0,GameManager.Inst.ItemManager.ItemList.Items.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeItem()
    {
        myItem = GameManager.Inst.ItemManager.ItemList.Items[Random.Range(0, GameManager.Inst.ItemManager.ItemList.Items.Count)];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>().myItem.Count < GameManager.Inst.UiManager.myInventory.slots.Length)
        {
            other.GetComponent<Player>().myItem.Add(myItem);
            other.GetComponent<Player>().PickUpItem = myItem;
            other.GetComponent<Player>().OnAcquisition(myItem);
            ChangeItem();
        }
    }
}
