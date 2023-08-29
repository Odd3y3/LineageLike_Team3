using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public UiManager UiManager;
    public InGameManager inGameManager;

    private void Awake()
    {
        base.Initialize();
        
        //로드 할 수 있는 정보 불러오기(Load Slot)
    }

    void Start()
    {
        
    }

    void Update()
    {
        //if(ItemManager==null || UiManager == null)
        //{
        //    ItemManager = FindObjectOfType<ItemManager>();
        //    UiManager = FindObjectOfType<UiManager>();
        //}
    }




    /// <summary>
    /// 게임 종료하는 함수
    /// </summary>
    public void OnGameExit()
    {
        Application.Quit();
    }
}
