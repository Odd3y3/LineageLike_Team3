using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFadeIn : MonoBehaviour
{
    private void Start()
    {
        GameManager.Inst.FadeIn();
    }
}
