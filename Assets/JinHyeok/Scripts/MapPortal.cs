using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPortal : MonoBehaviour
{
    [SerializeField]
    private LayerMask playerLayerMask;

    [SerializeField]
    private int destinationSceneNum = 2;
    [SerializeField]
    private int destinationSceneSpawnPointNum = 0;


    private void OnTriggerEnter(Collider other)
    {
        if((1 << other.gameObject.layer & playerLayerMask) != 0)
        {
            GameManager.Inst.MapChange(destinationSceneNum, destinationSceneSpawnPointNum);
        }
    }
}
