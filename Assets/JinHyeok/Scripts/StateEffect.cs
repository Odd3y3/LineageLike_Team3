using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEffect : MonoBehaviour
{
    public float disableTime = 1.0f;

    private void OnEnable()
    {
        GameManager.Inst.UiManager.myLevelUpText.LevelUp();
        StartCoroutine(Disable(disableTime));
    }
    IEnumerator Disable(float t)
    {
        yield return new WaitForSeconds(t);
        gameObject.SetActive(false);
    }
}
