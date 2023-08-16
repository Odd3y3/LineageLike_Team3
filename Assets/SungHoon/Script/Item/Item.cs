using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Item", menuName = "ItemObject/Item", order = int.MaxValue)]
public class Item : ScriptableObject
{
    public enum ITEMTYPE
    {
        None, Equipment, Potion, BattlePotion, Etc
    }
    public enum POTIONTYPE
    {
        None,Hp,Mp,Etc
    }
    public enum EQUIPMENTTYPE
    {
        None,Weapon, Armor, Boots, Helmet, Etc
    }
    public enum EQUIPMENGRADE
    {
        None,Normal,Rare,Unique,Eqic,Legendary,Mythology
    }
    [SerializeField]
    public ITEMTYPE ItemType;
    [SerializeField]
    public POTIONTYPE PotionType;
    [SerializeField]
    public EQUIPMENTTYPE EquipmentType;
    [SerializeField]
    public EQUIPMENGRADE ItemGrade;
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
    private int _stack = 1;
    public int Stack
    {
        get
        {
            return _stack;
        }
        set
        {
            _stack = value;
        }
    }

    [SerializeField]
    public GameObject Effect;

}
