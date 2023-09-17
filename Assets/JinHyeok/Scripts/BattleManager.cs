using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// 공격 타입, 맞았을 때, Normal인지 경직인지 다운 공격인지
/// </summary>
//public enum AttackType
//{
//    Normal,     //일반 공격(데미지만, 거리x)
//    Stagger,    //경직 (거리 o)
//    Down        //다운, 맞으면 날아감 (거리 o)
//}

public class BattleManager : MonoBehaviour
{
    static Vector3 originPos = Vector3.zero;
    static float originSize = 0f;

    public static void AttackDirCircle(Vector3 pos, float size, LayerMask enemyMask, float dmg,
        Vector3 attackVec, bool isDown = false, float knockBackDist = 0)
    {
        Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        foreach (Collider col in myCols)
        {   
            IDamage damage = col.GetComponent<IDamage>();
            if (damage != null) damage.OnDamage(dmg, attackVec, knockBackDist, isDown);
        }

        //Gizmo test용
        originPos = pos;
        originSize = size;
    }

    public static void AttackCircle(Vector3 pos, float size, LayerMask enemyMask, float dmg,
        bool isDown = false, float knockBackDist = 0)
    {
        Collider[] myCols = Physics.OverlapSphere(pos, size, enemyMask);
        foreach (Collider col in myCols)
        {
            IDamage damage = col.GetComponent<IDamage>();
            Vector3 attackVec = col.transform.position - pos;
            attackVec.y = 0f;
            attackVec.Normalize();
            if (damage != null) damage.OnDamage(dmg, attackVec, knockBackDist, isDown);
        }

        //Gizmo test용
        originPos = pos;
        originSize = size;
    }

    //Test용 Gizmo
    private void OnDrawGizmos()
    {
        Color color = Color.blue;
        color.a = 0.5f;
        Gizmos.color = color;
        Gizmos.DrawSphere(originPos, originSize);
    }


    //데미지 표시
    static GameObject dmgPopupPrefab = null;
    public static GameObject DmgPopupPrefab
    {
        get
        {
            if (dmgPopupPrefab == null)
                dmgPopupPrefab = Resources.Load<GameObject>("UI\\DmgPopup");
            return dmgPopupPrefab;
        }
    }
    static GameObject dynamicCanvas = null;
    public static GameObject DynamicCanvas
    {
        get
        {
            if (dynamicCanvas == null)
                dynamicCanvas = GameObject.Find("DynamicCanvas");
            return dynamicCanvas;
        }
    }
    public static void DamagePopup(Transform transform, float dmg)
    {
        GameObject obj = Instantiate(DmgPopupPrefab, DynamicCanvas.transform.GetChild(1));
        obj.GetComponent<TextMeshProUGUI>().text = dmg.ToString();
        obj.GetComponent<DamagePopup>().SetPos(transform);
    }
}
