using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    Vector3 originPos = Vector3.zero;
    [SerializeField]
    Vector3 posOffSet = new Vector3(0, 1f, 0);
    [SerializeField]
    float textSpeed = 100.0f;
    [SerializeField]
    float destroyTime = 1.0f;

    float deltaTime = 0f;
    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(originPos + posOffSet)
            + Vector3.up * (textSpeed * deltaTime);
        deltaTime += Time.deltaTime;
    }

    public void SetPos(Transform pos)
    {
        originPos = pos.position;
    }
}
