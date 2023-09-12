using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public float spwanTime = 3f;
    public float curTime;
    public Transform[] spwanPoints;
    public GameObject enemy;
    // Start is called before the first frame update
    private void Update()
    {
        if(curTime >= spwanTime)
        {
            int x = Random.Range(0, spwanPoints.Length);
            SpwanEnemy(x);
        }
        curTime += Time.deltaTime;
    }
    public void SpwanEnemy(int rndNum)
    {
        curTime = 0;
        Instantiate(enemy, spwanPoints[rndNum]);
    }

    // Update is called once per frame
    
   
}
