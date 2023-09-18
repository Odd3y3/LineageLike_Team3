using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpText : MonoBehaviour
{
    Animator myAnim;
    Transform myTarget;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
    }

    public void LevelUp()
    {
        myAnim.SetTrigger("LevelUp");
    }

    public void SetTarget()
    {
        myTarget = GameManager.Inst.inGameManager.myPlayer.transform;
    }

    private void Update()
    {
        if(myTarget != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(myTarget.position + new Vector3(0, 2f, 0));
        }
    }
}
