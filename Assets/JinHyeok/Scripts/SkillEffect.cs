using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillEffect : MonoBehaviour
{
    [SerializeField]
    private float effectDuration = 1f;


    private void OnEnable()
    {
        StartCoroutine(EffectDestroy(effectDuration));
    }

    IEnumerator EffectDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
