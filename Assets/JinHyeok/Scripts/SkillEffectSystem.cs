using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[사용 안함]
public class SkillEffectSystem : MonoBehaviour
{
    public GameObject[] playerSkillEffectPrefabs;

    public void OnSkillEffectStart(Transform target, int skillNum)
    {
        int num = skillNum - 1;
        if (num >= 0 && num < playerSkillEffectPrefabs.Length)
        {
            GameObject obj = Instantiate(playerSkillEffectPrefabs[num], transform);
            obj.transform.Translate(target.position);
            obj.transform.rotation = target.rotation;
        }
    }
}
