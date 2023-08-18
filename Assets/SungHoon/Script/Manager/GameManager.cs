using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player myPlayer;
    public ItemManager ItemManager;
    public UiManager UiManager;


    private void Awake()
    {
        base.Initialize();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(ItemManager==null || UiManager == null)
        {
            ItemManager = FindObjectOfType<ItemManager>();
            UiManager = FindObjectOfType<UiManager>();
        }
    }

}
