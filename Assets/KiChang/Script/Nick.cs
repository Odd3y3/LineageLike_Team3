using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class Nick : MonoBehaviour
{
    public Transform myTarget;

    TMPro.TextMeshProUGUI myText;

    private void Awake()
    {
        myText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Update()
    {
        if (myTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTarget.position + new Vector3(0, 2f, 0));
        }
    }

    public void SetTarget(Transform target, string str)
    {
        myTarget = target;
        myText.text = str;
    }
}
