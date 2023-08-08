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

    
}
