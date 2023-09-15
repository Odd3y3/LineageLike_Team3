using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Equipment :UIProperty<EquipmentSlot>
{
    public static bool stateActivated = false;
    [SerializeField]
    private GameObject go_ststeBase;
    [SerializeField]
    public EquipmentSlot[] slots;
    [SerializeField]
    public TMPro.TMP_Text[] myStatusList = null;
    private void Awake()
    {
        slots = myAllSlots;
        CloseState();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !MenuUI.GameIsPaused)
        {
            TryOpenState();
        }
        GameManager.Inst.inGameManager.myPlayer?.SetStatus(myStatusList);
    }

    public void TryOpenState()
    { 
            stateActivated = !stateActivated;

        if (!MenuUI.GameIsPaused)
        {
            if (stateActivated)
                OpenState();
            else
                CloseState();
        }    
    }

    private void OpenState()
    {
        go_ststeBase.SetActive(true);
    }

    private void CloseState()
    {
        go_ststeBase.SetActive(false);
    }


    public void EquipmentItem(Item _item)
    {
        if (Item.ITEMTYPE.Equipment != _item.ItemType)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].myEquipment != null)
                {
                    if (slots[i].myEquipment.Name == _item.Name)
                    {
                        return;
                    }
                }
            }
        }
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].myEquipment == null)
            {
                if (slots[i].myEquipmentType == _item.EquipmentType)
                {
                    slots[i].AddEquipment(_item);
                    return;
                }
            }
        }
    }

    public EquipmentData[] GetEquipmentData()
    {
        EquipmentData[] PlayerEquipmentData = GameManager.Inst.inGameManager.EquipmentDatas;
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].UpdateData();
            PlayerEquipmentData[i] = slots[i].GetData();
        }
        return PlayerEquipmentData;
    }

    public void SetEquipmentData(EquipmentData[] equipmentSlots)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetData(equipmentSlots[i]);
        }
    }
}
