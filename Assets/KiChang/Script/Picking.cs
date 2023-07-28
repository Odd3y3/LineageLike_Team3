using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Picking : MonoBehaviour
{
    public LayerMask clickMask;
    public LayerMask attackMask;
    public UnityEvent<Vector3> clickAction;
    public UnityEvent<Transform> attckAction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickMask | attackMask))
            {
                if ((1 << hit.transform.gameObject.layer & attackMask) != 0)
                {
                    attckAction?.Invoke(hit.transform);
                }
                else
                {
                    clickAction?.Invoke(hit.point);
                }
            }
        }
    }
}
