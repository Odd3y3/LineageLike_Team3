using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item myItem = null;
    public ItemList ItemList = null;
    // Start is called before the first frame update
    void Start()
    {
        myItem = ItemList.Items[Random.Range(0,ItemList.Items.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<Player>().myItem.Add(myItem);
        other.GetComponent<Player>().PickUpItem = myItem;
    }
}
