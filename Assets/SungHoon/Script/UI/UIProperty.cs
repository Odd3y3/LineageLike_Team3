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

    InventorySlot _inventoryslot = null;
    protected InventorySlot myInventorySlot
    {
        get
        {
            if (_inventoryslot == null)
            {
                _inventoryslot = GetComponent<InventorySlot>();
                if (_inventoryslot == null)
                {
                    _inventoryslot = GetComponentInChildren<InventorySlot>();
                }
            }
            return _inventoryslot;
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

    InventorySlot[] _allInvenoryslots = null;
    protected InventorySlot[] myAllInventorySlots
    {
        get
        {
            if (_allInvenoryslots == null)
            {
                _allInvenoryslots = GetComponentsInChildren<InventorySlot>();
            }
            return _allInvenoryslots;
        }
    }

    EquipmentSlot _equipmentslot = null;
    protected EquipmentSlot myEquipmentSlot
    {
        get
        {
            if (_equipmentslot == null)
            {
                _equipmentslot = GetComponent<EquipmentSlot>();
                if (_equipmentslot == null)
                {
                    _equipmentslot = GetComponentInChildren<EquipmentSlot>();
                }
            }
            return _equipmentslot;
        }
    }

    EquipmentSlot[] _allequipmentslots = null;
    protected EquipmentSlot[] myAllEquipmentSlots
    {
        get
        {
            if (_allequipmentslots == null)
            {
                _allequipmentslots = GetComponentsInChildren<EquipmentSlot>(); 
            }
            return _allequipmentslots;
        }
    }

    ConsumptionItemSlot _consumptionslot = null;
    protected ConsumptionItemSlot myConsumptionSlot
    {
        get
        {
            if (_consumptionslot == null)
            {
                _consumptionslot = GetComponent<ConsumptionItemSlot>();
                if (_consumptionslot == null)
                {
                    _consumptionslot = GetComponentInChildren<ConsumptionItemSlot>();
                }
            }
            return _consumptionslot;
        }
    }

    ConsumptionItemSlot[] _allconsumptionslots = null;
    protected ConsumptionItemSlot[] myAllConsumptionSlots
    {
        get
        {
            if (_allconsumptionslots == null)
            {
                _allconsumptionslots = GetComponentsInChildren<ConsumptionItemSlot>();
            }
            return _allconsumptionslots;
        }
    }


    SkillUISlot _skillslot = null;
    protected SkillUISlot mySkillSlot
    {
        get
        {
            if (_skillslot == null)
            {
                _skillslot = GetComponent<SkillUISlot>();
                if (_skillslot == null)
                {
                    _skillslot = GetComponentInChildren<SkillUISlot>();
                }
            }
            return _skillslot;
        }
    }

    SkillUISlot[] _allskillslots = null;
    protected SkillUISlot[] myAllSkillSlots
    {
        get
        {
            if (_allskillslots == null)
            {
                _allskillslots = GetComponentsInChildren<SkillUISlot>();
            }
            return _allskillslots;
        }
    }

}
