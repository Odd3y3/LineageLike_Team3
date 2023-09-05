using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIProperty<T> : MonoBehaviour 
{
    Image _img = null;
    protected Image myImage
    {
        get
        {
            if (_img == null)
            {
                _img = GetComponent<Image>();
                if (_img == null)
                {
                    _img = GetComponentInChildren<Image>();
                }
            }
            return _img;
        }
    }

    Image[] _allimg = null;
    protected Image[] myAllImage
    {
        get
        {
            if (_allimg == null)
            {
                _allimg = GetComponentsInChildren<Image>();
            }
            return _allimg;
        }
    }

    

    T _slot = default(T);
    protected T mySlot
    {
        get
        {
            if (_slot == null)
            {
                _slot = GetComponent<T>();
                if (_slot == null)
                {
                    _slot = GetComponentInChildren<T>();
                }
            }
            return _slot;
        }
    }

    T[] _allslots = null;
    protected T[] myAllSlots
    {
        get
        {
            if (_allslots == null)
            {
                _allslots = GetComponentsInChildren<T>();
            }
            return _allslots;
        }
    }

    TMPro.TMP_Text _text = null;
    protected TMPro.TMP_Text myText
    {
        get
        {
            if (_text == null)
            {
                _text = GetComponent<TMPro.TMP_Text>();
                if (_text == null)
                {
                    _text = GetComponentInChildren<TMPro.TMP_Text>();
                }
            }
            return _text;
        }
    }
}
