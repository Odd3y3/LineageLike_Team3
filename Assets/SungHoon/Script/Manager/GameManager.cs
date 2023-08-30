using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public UiManager UiManager;
    public InGameManager inGameManager;
    public SceneLoader sceneLoader;

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

    public void StartInGameScene()
    {
        //�� ������ ���� �� ��� �Ǵ� ��
        //UIManager ���ε� ����
        UiManager = FindObjectOfType<UiManager>();

        //�÷��̾� ����
        SpawnPlayer();

        //ī�޶� ���ε� ����
        FindObjectOfType<FollowCamera>().SetTarget(inGameManager.myPlayer.transform);

        //�÷��̾� Input Ȱ��ȭ
        inGameManager.myPlayer.CanMove = true;


        //�غ� ������ Fade In
        
    }

    public void SpawnPlayer()
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        inGameManager.myPlayer = player.GetComponent<Player>();

        inGameManager.myPlayer.transform.position = new Vector3(-28f, 1f, -52.5f);
    }

    /// <summary>
    /// ���� �����ϴ� �Լ�
    /// </summary>
    public void OnGameExit()
    {
        Application.Quit();
    }

    public void StartNewGame()
    {
        sceneLoader.LoadScene(2);
    }

    public void StartLoadGame()
    {

    }
}
