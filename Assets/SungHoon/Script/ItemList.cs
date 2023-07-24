using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemList", menuName = "ItemList", order = int.MaxValue)]
public class ItemList : ScriptableObject
{
    [SerializeField]
    List<Item> Items = new List<Item>();
}