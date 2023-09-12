using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsUI : UIProperty<GoodsUI>
{
    public int myGold = 0;
    // Start is called before the first frame update
    void Start()
    {
        myText.text = myGold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropCoin(int Gold)
    {
        myGold = Gold;
        myText.text = myGold.ToString();
    }
}
