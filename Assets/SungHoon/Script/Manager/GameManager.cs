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

    public void StartInGameScene(int spawnPointNum)
    {
        //씬 변경후 설정 해 줘야 되는 것
        //UIManager 바인드 설정
        UiManager = FindObjectOfType<UiManager>();

        //플레이어 생성
        SpawnPlayer(spawnPointNum);

        //카메라 바인드 설정
        FindObjectOfType<FollowCamera>().SetTarget(inGameManager.myPlayer.transform);

        //플레이어 Input 활성화
        inGameManager.myPlayer.CanMove = true;


        //준비 끝나고 Fade In
        
    }

    public void SpawnPlayer(int spawnPointNum)
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        inGameManager.myPlayer = player.GetComponent<Player>();

        //ui hpBar 바인딩
        inGameManager.myPlayer.myHpBar = UiManager.myHpSlider;

        //플레이어 스폰 위치
        spawnPoints = FindObjectOfType<PlayerSpawnPoints>();
        if (spawnPoints != null)
        {
            inGameManager.myPlayer.transform.position = 
                spawnPoints.spawnPoint[spawnPointNum].transform.position;
        }
    }

    /// <summary>
    /// 게임 종료하는 함수
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
