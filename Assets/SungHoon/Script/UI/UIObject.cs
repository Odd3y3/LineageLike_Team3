using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject : UIProperty
{
    public void SetColor(float _alpha)
    {
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;
    }
}
