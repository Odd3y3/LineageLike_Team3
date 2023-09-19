using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void OnGameExit()
    {
        GameManager.Inst.OnGameExit();
    }

    public void OnUISound()
    {
        GameManager.Inst.SoundManager.OnUISound();
    }
}
