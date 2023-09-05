using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public ItemManager ItemManager;
    public UiManager UiManager;
    public InGameManager inGameManager;
    public SceneLoader sceneLoader;

    public PlayerSpawnPoints spawnPoints;

    [SerializeField]
    private Image FadeInOutImage;

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


        //�غ� ������ Fade In
        FadeIn();

        //�÷��̾� Input Ȱ��ȭ
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

        //FadeOut �Ŀ� ����
        done?.Invoke();
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
        FadeOut(() => sceneLoader.LoadScene(2, 0));
        //sceneLoader.LoadScene(2, 0);
    }

    public void StartLoadGame()
    {

    }

    public void MapChange(int sceneNum, int spawnPointNum)
    {
        FadeOut(() => sceneLoader.LoadScene(sceneNum, spawnPointNum));
        //sceneLoader.LoadScene(sceneNum, spawnPointNum);
    }
}
