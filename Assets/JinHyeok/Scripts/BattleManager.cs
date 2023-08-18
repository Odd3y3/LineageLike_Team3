using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    static Vector3 originPos = Vector3.zero;
    static float originSize = 0f;

    public static void AttackCircle(Vector3 pos, float size, LayerMask enemyMask)
    {
        Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        foreach (Collider col in myCols)
        {
            Debug.Log("Attack Hit !");
            //IDamage damage = col.GetComponent<IDamage>();
            //if (damage != null) damage.OnDamage(curAttackPoint);
        }

        //Gizmo test¿ë
        originPos = pos;
        originSize = size;
    }

    //Test¿ë Gizmo
    private void OnDrawGizmos()
    {
        Color color = Color.blue;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawSphere(originPos, originSize);
    }
}
