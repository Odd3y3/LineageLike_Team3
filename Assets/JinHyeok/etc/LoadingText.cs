using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    [SerializeField]
    private Slider slider;
    [SerializeField]
    private TMPro.TextMeshProUGUI text;

    void Update()
    {
        if(slider.value >= 1)
        {
            text.gameObject.SetActive(true);
        }
    }
}
