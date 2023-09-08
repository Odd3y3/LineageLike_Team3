using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAct : MonoBehaviour
{
    
    GameObject scanObject;
    public LayerMask clickMask;


    void Update()
    {
        
        if (Input.GetMouseButtonDown(0) && scanObject != null)
        {
          //  manager.Action(scanObject);
        }
    }
}
