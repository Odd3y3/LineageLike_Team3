using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AIPerception : MonoBehaviour
{
    public LayerMask enemyMask;
    public List<Transform> enemyList = new List<Transform>();
    public UnityEvent findEnemy;
    public UnityEvent lostEnemy;
    public Transform myTarget
    {
        get; private set;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            //Àû ¹ß°ß
            if (!enemyList.Contains(other.transform))
                enemyList.Add(other.transform);

            if (myTarget == null)
            {
                myTarget = other.transform;
                findEnemy?.Invoke();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            enemyList.Remove(other.transform);
            if (myTarget == other.transform)
            {
                myTarget = null;
                lostEnemy?.Invoke();
            }
        }
    }
}