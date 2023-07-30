using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class EffectColorChange : MonoBehaviour
{
    //Color �� Hue���� ���
    public Color colorHue = Color.red;


    //�ν����Ϳ��� ��ƼŬ ���� ����
    public void ChangeColor()
    {
        ParticleSystem[] psArr = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in psArr)
        {
            var main = ps.main;
            Color.RGBToHSV(colorHue, out float inputHue, out _, out _);
            Color.RGBToHSV(main.startColor.color, out float H, out float S, out float V);
            var rgb = Color.HSVToRGB(inputHue, S, V);
            main.startColor = new Color(rgb.r, rgb.g, rgb.b, main.startColor.color.a);
        }
    }
}
