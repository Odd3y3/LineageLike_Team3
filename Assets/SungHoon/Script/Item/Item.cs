using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ItemObject/Item", order = int.MaxValue)]
public class Item : ScriptableObject
{
   public enum EQUIPMENTTYPE
   {
        None, Weapon, Armor, Boots, Helmet, Etc
   }
    public enum ITEMTYPE
    {
        Equipment, Potion, Etc
    }
    public enum ITEMGRADE
    {
        Normal,Rare,Unique,Eqic,Legendary,Mythology
    }
    [SerializeField]
    public EQUIPMENTTYPE EquipmentType;
    [SerializeField]
    public ITEMTYPE ItemType;
    [SerializeField]
    public ITEMGRADE ItemGrade;
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
        set
        {
            _statPoint = value;
        }
    }
    [SerializeField]
    private Sprite _sprite;
    public Sprite Sprite
    {
        get { return _sprite; }
    }


    [SerializeField]
    public GameObject itemPrefab;
}
