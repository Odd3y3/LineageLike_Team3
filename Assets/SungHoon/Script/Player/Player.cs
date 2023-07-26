using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Player : BattleSystem
{
    NavMeshPath path = null;
    public Item PickUpItem = null;
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        path = new NavMeshPath();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEquipItem(Item myItem)
    {
        if(myItem != null)
        {
            if (myItem.EquipmentType == Item.EQUIPMENTTYPE.Weapon)
            {
                curAttackPoint += myItem.StatPoint;
            }
            else
            {
                curDefensePoint += myItem.StatPoint;
            }
        }
    }

    public void MovePos(Vector3 pos)
    {
        if (NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            StopAllCoroutines();

            StartCoroutine(MoningByPath(path.corners));
        }
    }

    IEnumerator MoningByPath(Vector3[] list)
    {
        int i = 0;
        while (i < list.Length - 1)
        {

            yield return StartCoroutine(MovingToPos(list[i + 1], () => ++i));
        }
    }
}
