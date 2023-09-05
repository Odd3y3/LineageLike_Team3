using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIObject<T> : UIProperty<T>
{
    public void SetColor(float _alpha)
    {
        Color color = myImage.color;
        color.a = _alpha;
        myImage.color = color;
    }
}
