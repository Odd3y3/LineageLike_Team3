using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public UiManager UiManager;
    public InGameManager inGameManager;
    public SceneLoader sceneLoader;
    public QuestManager questManager;
    public SoundManager SoundManager;
    public ItemDataBase itemDataBase;

    public PlayerSpawnPoints spawnPoints;

    [SerializeField]
    private Image FadeInOutImage;

    [HideInInspector]
    public int curSceneNum = 1;

    private void Awake()
    {
        base.Initialize();
    }

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void StartInGameScene(int spawnPointNum)
    {
        //씬 변경후 설정 해 줘야 되는 것
        //UIManager 바인드 설정
        UiManager = FindObjectOfType<UiManager>();

        ItemManager = FindObjectOfType<ItemManager>();

        //플레이어 생성
        SpawnPlayer(spawnPointNum);

        //UI설정
        UiManager.DefalutSetting();

        //카메라 바인드 설정
        FindObjectOfType<FollowCamera>().SetTarget(inGameManager.myPlayer.transform);
        //미니맵카메라 바인드 설정
        FindObjectOfType<MiniMap>().SetTarget();


        //준비 끝나고 Fade In
        FadeIn();

        //플레이어 Input 활성화
        inGameManager.myPlayer.CanMove = true;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCo(1.0f));
    }
    IEnumerator FadeInCo(float t)
    {
        float curtime = 0f;
        while(curtime <= t)
        {
            curtime += Time.deltaTime;
            FadeInOutImage.color = new Vector4(0, 0, 0, 1f - curtime / t);
            yield return null;
        }
        FadeInOutImage.raycastTarget = false;
    }

    public void FadeOut(UnityAction done = null)
    {
        StartCoroutine(FadeOutCo(1.0f, done));
    }
    IEnumerator FadeOutCo(float t, UnityAction done)
    {
        FadeInOutImage.raycastTarget = true;
        float curtime = 0f;
        while (curtime <= t)
        {
            curtime += Time.deltaTime;
            FadeInOutImage.color = new Vector4(0, 0, 0, curtime / t);
            yield return null;
        }

        //FadeOut 후에 실행
        done?.Invoke();
    }

    public void SpawnPlayer(int spawnPointNum)
    {
        GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
        inGameManager.myPlayer = player.GetComponent<Player>();

        //ui hpBar 바인딩
        inGameManager.myPlayer.myHpBar = UiManager.myHpSlider;
        inGameManager.myPlayer.myExpBar = UiManager.myExpSlider;

        //플레이어 정보 받기 (SaveData)
        inGameManager.Load(inGameManager.myPlayer);
        
        //-1이 아니면 해당 스폰포인트로 이동
        if (spawnPointNum != -1)
        {
            //스폰 포인트로 플레이어 이동
            spawnPoints = FindObjectOfType<PlayerSpawnPoints>();
            if (spawnPoints != null)
            {
                inGameManager.myPlayer.transform.position = 
                    spawnPoints.spawnPoint[spawnPointNum].transform.position;
            }

        }
        inGameManager.Save();
    }

    /// <summary>
    /// 게임 종료하는 함수
    /// </summary>
    public void OnGameExit()
    {
        Application.Quit();
    }

    public void StartNewGame(int sceneNum)
    {
        //test
        inGameManager.curSaveSlotNum = 0;

        curSceneNum = sceneNum;
        FadeOut(() => sceneLoader.LoadScene(sceneNum, 0));
        //sceneLoader.LoadScene(2, 0);
    }

    public void StartLoadGame(int saveSlotNum)
    {
        SaveData saveData = inGameManager.saveDatas[saveSlotNum];

        inGameManager.curSaveSlotNum = saveSlotNum;
        if (saveData.IsEmpty)
        {
            curSceneNum = 2;
            FadeOut(() => sceneLoader.LoadScene(curSceneNum, 0));

            //퀘스트 초기화
            questManager.InitQuestDatabase(saveSlotNum);
            questManager.ResetQuestData();
        }
        else
        {
            curSceneNum = saveData.playerInfo.SceneNum;
            FadeOut(() => sceneLoader.LoadScene(curSceneNum, -1));

            //퀘스트 초기화
            questManager.InitQuestDatabase(saveSlotNum);
        }
    }

    public void MapChange(int sceneNum, int spawnPointNum)
    {
        inGameManager.Save();
        curSceneNum = sceneNum;
        FadeOut(() => sceneLoader.LoadScene(sceneNum, spawnPointNum));
        
    }

    public void GotoTitle()
    {
        inGameManager.Save();
        curSceneNum = 1;
        FadeOut(() => SceneManager.LoadScene(0));
    }

    public void GameOver()
    {
        UiManager.myGameOverWindow.SetActive(true);
    }
}
