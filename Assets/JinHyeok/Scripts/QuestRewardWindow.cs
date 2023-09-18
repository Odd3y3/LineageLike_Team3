using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestRewardWindow : MonoBehaviour
{
    public TMPro.TextMeshProUGUI tmp;
    public Image[] rewardItems;

    private void Awake()
    {
        //tmp = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void SetReward(int exp, int gold, Item[] items)
    {
        tmp.text = $"EXP +{exp}\nGold +{gold}";
        tmp.gameObject.SetActive(true);
        foreach (Image item in rewardItems)
        {
            item.gameObject.SetActive(false);
        }
        for (int i = 0; i < Math.Min(rewardItems.Length, items.Length); ++i)
        {
            rewardItems[i].sprite = items[i].Sprite;
            rewardItems[i].gameObject.SetActive(true);
        }
    }

    public void HideReward()
    {
        tmp.gameObject.SetActive(false);
        foreach (Image item in rewardItems)
        {
            item.gameObject.SetActive(false);
        }
    }
}
