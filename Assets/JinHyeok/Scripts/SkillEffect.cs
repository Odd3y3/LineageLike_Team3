using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffect : MonoBehaviour
{
    public GameObject[] effectPrefabs;
    public float effectDuration = 2.0f;

    List<GameObject> playingEffects;

    private void Awake()
    {
        playingEffects = new List<GameObject>();
    }

    public void OnSkillEffectStart()
    {
        StartCoroutine(SkillEffectOn(effectDuration));
    }

    IEnumerator SkillEffectOn(float duration)
    {
        foreach(GameObject effect in effectPrefabs)
        {
            playingEffects.Add(Instantiate(effect, transform));
        }

        yield return new WaitForSeconds(duration);

        foreach(GameObject effect in playingEffects)
        {
            Destroy(effect);
        }
    }
}
