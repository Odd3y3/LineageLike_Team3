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
        
        //�ε� �� �� �ִ� ���� �ҷ�����(Load Slot)
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
    /// ���� �����ϴ� �Լ�
    /// </summary>
    public void OnGameExit()
    {
        Application.Quit();
    }
}
