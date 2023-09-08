using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    Vector3 myDir = Vector3.zero;
    float myDist = 0.0f;

    public Vector3 cameraPos = new Vector3(0.0f, 10.0f, -5.0f);
    public float zoomSpeed=2.0f;
    public float rotSpeed=360.0f;
    public float smoothMoveSpeed = 10.0f;

    public Vector2 zoomRange = new Vector2(1.0f, 5.0f);
    
    Transform followTarget = null;

    Vector3 targetpos = Vector3.zero;
    //Quaternion orgRot = Quaternion.identity;


    void Update()
    {
        if(followTarget != null)
        {
            myDist -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            myDist = Mathf.Clamp(myDist, zoomRange.x, zoomRange.y);
            targetpos = followTarget.position + myDir * myDist;
            transform.position = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * smoothMoveSpeed);
        }
        
    }

    public void SetTarget(Transform target)
    {
        transform.position = target.position + cameraPos;
        transform.LookAt(target);
        myDist = cameraPos.magnitude;
        myDir = cameraPos.normalized;

        targetpos = transform.position;

        followTarget = target;
    }
}
