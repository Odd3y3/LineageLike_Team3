using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkillEffect : MonoBehaviour
{
    [SerializeField]
    private float effectDuration = 1f;

    [Range(0f, 1f)]
    public float colorHue = 0f;

    private void OnEnable()
    {
        StartCoroutine(EffectDestroy(effectDuration));
    }

    IEnumerator EffectDestroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


    //인스펙터에서 파티클 색상 변경
    public void ChangeColor()
    {
        ParticleSystem[] psArr = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in psArr)
        {
            var main = ps.main;
            Color.RGBToHSV(main.startColor.color, out float H, out float S, out float V);
            var rgb = Color.HSVToRGB(colorHue, S, V);
            main.startColor = new Color(rgb.r, rgb.g, rgb.b, main.startColor.color.a);
        }
    }
}
