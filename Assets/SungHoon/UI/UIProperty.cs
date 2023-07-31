using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIProperty : MonoBehaviour
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

    Slot _slot = null;
    protected Slot mySlot
    {
        get
        {
            if (_slot == null)
            {
                _slot = GetComponent<Slot>();
                if (_slot == null)
                {
                    _slot = GetComponentInChildren<Slot>();
                }
            }
            return _slot;
        }
    }

    Slot[] _allslots = null;
    protected Slot[] myAllSlots
    {
        get
        {
            if (_allslots == null)
            {
                _allslots = GetComponentsInChildren<Slot>();
            }
            return _allslots;
        }
    }
}
