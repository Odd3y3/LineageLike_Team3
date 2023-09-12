using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneCamera : MonoBehaviour, ICutScene
{
    Vector3 originPos = Vector3.zero;
    FollowCamera followCamera;

    [SerializeField]
    Vector3 destinationPos = new Vector3(-43f, 14f, -90f);

    private void Awake()
    {
        followCamera = GetComponent<FollowCamera>();
    }

    public void OnStartCutScene()
    {
        originPos = transform.position;
        StartCoroutine(StartingCutSceneCamera());
        followCamera.enabled = false;
    }
    public void OnEndCutScene()
    {
        followCamera.enabled = true;
    }
    IEnumerator StartingCutSceneCamera()
    {
        yield return StartCoroutine(CameraMoving(destinationPos, 1f));
        yield return StartCoroutine(CameraMoving(destinationPos, 5f));
        yield return StartCoroutine(CameraMoving(originPos, 1f));
    }
    IEnumerator CameraMoving(Vector3 pos, float t)
    {
        float time = t;
        while (t > 0)
        {
            t -= Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, pos, 0.1f);
            yield return null;
        }
    }

    
}
