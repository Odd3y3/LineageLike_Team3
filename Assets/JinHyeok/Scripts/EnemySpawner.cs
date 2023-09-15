using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public void Respawn(Monster enemy, float time)
    {
        StartCoroutine(Respawning(enemy, time));
    }
    IEnumerator Respawning(Monster enemy, float time)
    {
        enemy.gameObject.SetActive(false);

        yield return new WaitForSeconds(time);

        enemy.Respawn();
        enemy.gameObject.SetActive(true);
    }
}
