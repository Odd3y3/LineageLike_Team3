using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MousePicking : MonoBehaviour
{
    public LayerMask clickMask;
    public UnityEvent<Vector3> clickAction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickMask))
            {
                clickAction?.Invoke(hit.point);
            }
        }
    }
}
