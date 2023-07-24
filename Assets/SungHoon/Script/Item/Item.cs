using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = int.MaxValue)]
public class Item : ScriptableObject
{
   public enum EQUIPMENTTYPE
   {
        None, Weapon, Armor, Boots, Helmet, Etc
   }
    [SerializeField]
    public EQUIPMENTTYPE EquipmentType;
    public enum ITEMTYPE
   {
        Equipment,Potion,Etc
   }
    [SerializeField]
    public ITEMTYPE ItemType;
    [SerializeField]
    private string _name;
    public string Name
    {
        get { return _name; }
    }
    [SerializeField]
    private float _statPoint;
    public float StatPoint
    {
        get{ return _statPoint;}
    }
    [SerializeField]
    public Sprite _img;
    public Sprite Img
    {
        get { return _img; }
    }
}
