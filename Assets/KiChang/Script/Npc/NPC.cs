using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName = null;

    public Transform canvas;

    protected void Initialize()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI\\NPCName"), canvas);
        obj.GetComponent<Nick>().SetTarget(transform, npcName);
    }
}
