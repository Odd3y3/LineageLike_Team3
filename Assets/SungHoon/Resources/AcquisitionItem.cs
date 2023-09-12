using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcquisitionItem : MonoBehaviour
{
    public ItemDrop myParent;
    // Start is called before the first frame update
    void Start()
    {
        myParent = transform.parent.GetComponent<ItemDrop>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Player>(out Player player)) return;
            myParent.AcquisitionItem(other);
    }
}
