using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonEntryTrigger : MonoBehaviour
{
    public LayerMask playerMask;
    public Boss boss;
    public CutSceneCamera cutSceneCamera;


    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerMask) != 0)
        {
            StartCoroutine(StartCutScene());
        }
    }

    IEnumerator StartCutScene()
    {
        boss.OnStartCutScene();
        cutSceneCamera.OnStartCutScene();
        GameManager.Inst.inGameManager.myPlayer.OnStartCutScene();

        yield return new WaitForSeconds(7.0f);

        boss.OnEndCutScene();
        cutSceneCamera.OnEndCutScene();
        GameManager.Inst.inGameManager.myPlayer.OnEndCutScene();


        Destroy(gameObject);
    }
}

public interface ICutScene
{
    public void OnStartCutScene();
    public void OnEndCutScene();
}