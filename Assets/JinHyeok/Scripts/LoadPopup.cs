using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPopup : MonoBehaviour
{
    [SerializeField]
    LoadSlot[] loadSlots;
    [SerializeField]
    SaveData[] saveDatas;

    public GameObject startPopUp;
    public GameObject selectPopUp;

    int selectedSlotNum = -1;
    public void SelectSlot(int num)
    {
        selectedSlotNum = num;
    }

    public void DeleteSlotData()
    {
        loadSlots[selectedSlotNum].DeleteLoadSlot();
    }

    public void OnClickPlayButton(int num)
    {
        selectedSlotNum = num;
        if (saveDatas[num].IsEmpty)
        {
            startPopUp.SetActive(true);
        }
        else
        {
            selectPopUp.SetActive(true);
        }
    }
    
    public void StartLoadGame()
    {
        GameManager.Inst.StartLoadGame(selectedSlotNum);
    }
}
