using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Player : BattleSystem
{
    NavMeshPath path = null;
    public Item PickUpItem = null;
    // Start is called before the first frame update
    void Start()
    {
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePos(Vector3 pos)
    {
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            switch (path.status)
            {
                case NavMeshPathStatus.PathInvalid://알수 없음
                    Debug.Log("갈수 없음");
                    break;
                case NavMeshPathStatus.PathPartial://막혀 있음
                    Debug.Log("막혀 있음");
                    break;
                case NavMeshPathStatus.PathComplete://갈수 있음
                    Debug.Log("갈수 있음");
                    break;
            }
            StopAllCoroutines();

            StartCoroutine(MoningByPath(path.corners));
        }
    }

    IEnumerator MoningByPath(Vector3[] list)
    {
        int i = 0;
        while (i < list.Length - 1)
        {

            yield return StartCoroutine(MovingToPos(list[i + 1], () => ++i));
        }
    }
}
