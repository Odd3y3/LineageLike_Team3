using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSlot : MonoBehaviour
{
    public SaveData saveData;
    public TMPro.TextMeshProUGUI tmp;
    public GameObject deleteButton;

    private void Awake()
    {
        UpdateSlot();
    }

    public void DeleteLoadSlot()
    {
        saveData.IsEmpty = true;
        saveData.playerInfo = default;
        UpdateSlot();
    }

    /// <summary>
    /// SaveData가 바뀌거나 처음에 한번 실행.
    /// SaveData를 가져와서 화면에 보여줌.
    /// </summary>
    void UpdateSlot()
    {
        if (saveData.IsEmpty)
        {
            tmp.text = "Empty";
            deleteButton.SetActive(false);
        }
        else
        {
            tmp.text = $"LV.{saveData.playerInfo.BattleStat.LV}";
            deleteButton.SetActive(true);
        }
    }
}
