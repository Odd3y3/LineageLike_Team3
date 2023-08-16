using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSound : MonoBehaviour
{
    AudioSource m_Source;
    private void Awake()
    {
        m_Source = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (m_Source != null)
        {
            m_Source.Play();
        }
    }
}
