using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    AIperception _perception = null;
    protected AIperception myPerception
    {
        get
        {
            if (_perception == null)
            {
                _perception = GetComponent<AIperception>();
                if (_perception == null)
                {
                    _perception = GetComponentInChildren<AIperception>();
                }
            }
            return _perception;
        }
    }
    public float moveSpeed = 1.0f;
    public float rotSpeed = 360.0f;
    public void MoveToPos(Vector3 pos)
    {
        MoveToPos(pos, null);
    }
    public void MoveToPos(Vector3 pos, UnityAction done)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToPos(pos, done));
    }
}
