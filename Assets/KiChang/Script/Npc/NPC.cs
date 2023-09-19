using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string npcName = null;

    public Transform canvas;

    MiniMapIcon miniMapIcon = null;

    protected GameObject NPCNameObj;

    protected void Initialize()
    {
        NPCNameObj = Instantiate(Resources.Load<GameObject>("UI\\NPCName"), canvas);
        NPCNameObj.GetComponent<Nick>().SetTarget(transform, npcName, Color.green);

        //�̴ϸ� ������ ����
        miniMapIcon = Instantiate(Resources.Load<GameObject>("UI\\MiniMapIcon"),
            FindObjectOfType<UiManager>().myMiniMapIcons).GetComponent<MiniMapIcon>();
        miniMapIcon.SetTarget(transform, Color.yellow);
    }
}
