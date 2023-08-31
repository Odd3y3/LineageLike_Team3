using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public UiManager UiManager;
    public InGameManager inGameManager;
    public SceneLoader sceneLoader;

    public PlayerSpawnPoints spawnPoints;
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

    public void StartInGameScene(int spawnPointNum)
    {
        //�� ������ ���� �� ��� �Ǵ� ��
        //UIManager ���ε� ����
        UiManager = FindObjectOfType<UiManager>();

        //�÷��̾� ����
        SpawnPlayer(spawnPointNum);

        //ī�޶� ���ε� ����
        FindObjectOfType<FollowCamera>().SetTarget(inGameManager.myPlayer.transform);

        //�÷��̾� Input Ȱ��ȭ
        inGameManager.myPlayer.CanMove = true;


        //�غ� ������ Fade In
        
    }

    public void SpawnPlayer(int spawnPointNum)
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        inGameManager.myPlayer = player.GetComponent<Player>();

        //ui hpBar ���ε�
        inGameManager.myPlayer.myHpBar = UiManager.myHpSlider;

        //�÷��̾� ���� ��ġ
        spawnPoints = FindObjectOfType<PlayerSpawnPoints>();
        if (spawnPoints != null)
        {
            inGameManager.myPlayer.transform.position = 
                spawnPoints.spawnPoint[spawnPointNum].transform.position;
        }
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
        sceneLoader.LoadScene(2, 0);
    }

    public void StartLoadGame()
    {

    }

    public void MapChange(int sceneNum, int spawnPointNum)
    {
        sceneLoader.LoadScene(sceneNum, spawnPointNum);
    }
}
