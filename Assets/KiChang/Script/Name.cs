using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Name : MonoBehaviour
{
    public GameObject Cam;

    Vector3 startScale;
    public float distance = 3;

     void Start()
    {
        startScale = transform.localScale;
    }

    void Upedate()
    {
        float dist = Vector3.Distance(Cam.transform.position, transform.position);
        Vector3 newScale = startScale * dist / distance;
        transform.localScale = newScale;

        transform.rotation = Cam.transform.rotation;
    }
}
