using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    static Vector3 originPos = Vector3.zero;
    static float originSize = 0f;

    public static void AttackCircle(Vector3 pos, float size, LayerMask enemyMask)
    {
        //Gizmos.DrawSphere(pos, size);
        originPos = pos;
        originSize = size;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(originPos, originSize);
    }
}
