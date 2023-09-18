using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] myBackgroundSound;

    public AudioSource myBackGroundSource;
    public AudioSource myEffectSource;
    public AudioSource myUiSource;
    public AudioSource myAttackSource;
    // Start is called before the first frame update
    void Start()
    {
        myEffectSource = FindObjectOfType<Player>().GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (myBackGroundSource.isPlaying == false)
            {
                myBackGroundSource.Play();
            }
            myBackGroundSource.clip = myBackgroundSound[SceneManager.GetActiveScene().buildIndex - 1];
        }
        else
        {
            myBackGroundSource.Stop();
        }
    }
    

    public void OnSkillEffectSound(SkillInfo useSkill)
    {
        if (myEffectSource.isPlaying == true)
        {
            myEffectSource.Stop();
        }
        myEffectSource.clip = useSkill.skill.SkillSound;
        myEffectSource.Play();
    }
    public void OnUISound()
    {
        if (myUiSource.isPlaying == true)
        {
            myUiSource.Stop();
        }
        myUiSource.Play();
    }
    public void OnAttackSound()
    {
        if (myAttackSource.isPlaying == true)
        {
            myAttackSource.Stop();
        }
        myAttackSource.Play();
    }
}
