using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterStatBar : MonoBehaviour
{
    public Transform myTarget = null;
    public Slider mySlider = null;
    public void Initialize(Transform target)
    {
        myTarget = target;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (myTarget != null)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(myTarget.position);
            if (pos.z < 0.0f)
            {
                transform.position = new Vector3(0, 10000, 0);
            }
            else
            {
                transform.position = pos;
            }
        }
    }
}
