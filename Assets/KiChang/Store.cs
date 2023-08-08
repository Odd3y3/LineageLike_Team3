using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store : MonoBehaviour
{
    public ItemBuff itemBuff;
    public Transform slotRoot;
    private List<Slot> slots;
    // Start is called before the first frame update
    void Start()
    {
        slots = new List<Slot>();

        int slotCnt = slotRoot.childCount;

        for(int i =0; i< slotCnt; ++i)
        {
            var slot = slotRoot.GetChild(i).GetComponent<Slot>();

            if( i < itemBuff.items.Count)
            {
                slot.SetItem(itemBuff.items[i]);
            }
            slots.Add(slot);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
