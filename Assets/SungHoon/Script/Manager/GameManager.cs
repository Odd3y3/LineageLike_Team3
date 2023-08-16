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
        Initialized();
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    

}
