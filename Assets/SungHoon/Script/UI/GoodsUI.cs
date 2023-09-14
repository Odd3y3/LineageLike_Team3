using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsUI : UIProperty<GoodsUI>
{
    public uint myGold = 0;
    // Start is called before the first frame update
    void Start()
    {
        myText.text = myGold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCoin(uint Gold)
    {
        myGold = Gold;
        myText.text = myGold.ToString();
    }
}
