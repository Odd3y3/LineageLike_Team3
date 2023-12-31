using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Monster : AImovement
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    //Roaming을 하는지
    public bool isRoaming = true;
    //리스폰을 하는지
    public bool isRespawn = true;
    //캐릭터가 완전히 사라지고 나서 리스폰까지의 시간.
    public float respawnTime = 3.0f;

    EnemySpawner spawner = null;

    public State myState = State.Create;

    Vector3 startPos = Vector3.zero;

    public Transform barPoint = null;
    Transform uiHpBars = null;

    GameObject hpBarObj = null;
    MiniMapIcon miniMapIcon = null;

    //MonsterStatBar myStatUI = null;

    //protected Transform myTarget = null;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                if (isRoaming)
                {
                    Vector3 rndDir = Vector3.forward;
                    Quaternion rndRot = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
                    float dist = Random.Range(0.0f, 5.0f);
                    rndDir = rndRot * rndDir * dist;
                    Vector3 rndPos = startPos + rndDir;

                    MoveToPos(rndPos, () => StartCoroutine(Waiting(Random.Range(1.0f, 3.0f))));
                    ChangeState(State.Roaming);
                }
                break;
            case State.Battle:
                AttackTarget(myTarget);
                break;
            case State.Dead:
                myCol.enabled = false;
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                DropExp(BattleStat.MaxExp);
                DropItem();
                DisAppear();
                miniMapIcon.gameObject.SetActive(false);

                //퀘스트 진행
                GameManager.Inst.questManager.ProcessQuest(QuestType.DestroyEnemy, ID);
                break;
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                break;
        }
    }
    IEnumerator Waiting(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Normal);
    }

    void Awake()
    {
        startPos = transform.position;
        
        if(transform.parent != null)
            transform.parent.TryGetComponent<EnemySpawner>(out spawner);

        //미니맵 아이콘 설정
        miniMapIcon = Instantiate(Resources.Load<GameObject>("UI\\MiniMapIcon"),
            FindObjectOfType<UiManager>().myMiniMapIcons).GetComponent<MiniMapIcon>();
        miniMapIcon.SetTarget(transform, Color.red);
    }

    void OnEnable()
    {
        Initialize();


        hpBarObj = Instantiate(Resources.Load("UI\\EnemyHPBar") as GameObject,
            GameObject.Find("DynamicCanvas").transform.GetChild(0));
        myHpBar = hpBarObj.GetComponent<Slider>();
        hpBarObj.GetComponent<EnemyHPBar>().SetTarget(transform);
        //myStatUI.Initialize(barPoint);

        //ChangeState(State.Normal);
        StartCoroutine(StartDelaying(2.0f));

        //미니맵 아이콘 켜기
        miniMapIcon.gameObject.SetActive(true);
    }
    IEnumerator StartDelaying(float t)
    {
        yield return new WaitForSeconds(t);
        ChangeState(State.Normal);
        myPerception.gameObject.SetActive(true);
    }

    void Update()
    {
        StateProcess();
    }

    public void DropItem()
    {
        GameObject obj = Instantiate(Resources.Load("DropItem") as GameObject);
        obj.GetComponent<ItemDrop>().OnSetItem(this.gameObject.transform);
    }

    public void FindEnemy()
    {
        if (myState == State.Dead) return;
        myTarget = myPerception.myTarget;
        ChangeState(State.Battle);
    }

    public override void OnDamage(float dmg, Vector3 attackVec, float knockBackDist, bool isDown)
    {
        if (myTarget == null) myTarget = GameManager.Inst.inGameManager.myPlayer.transform;
        ChangeState(State.Battle);

        base.OnDamage(dmg, attackVec, knockBackDist, isDown);

        GameManager.Inst.SoundManager.OnAttackSound();

        if (!IsLive)
        {
            ChangeState(State.Dead);
        }
    }

    //protected override void OnDead()
    //{
    //    ChangeState(State.Dead);
    //}

    void DisAppear()
    {
        Destroy(hpBarObj);
        StartCoroutine(DisAppearing(0.2f, 7.0f));
       // QuestManager.Instance.ProcessQuest(QuestType.DestroyEnemy, 0);
    }
    IEnumerator DisAppearing(float speed, float t)
    {
        yield return new WaitForSeconds(t);
        //Destroy(myStatUI.gameObject);

        float dist = 2.0f;
        while (dist > 0.0f)
        {
            float delta = speed * Time.deltaTime;
            dist -= delta;
            transform.Translate(Vector3.down * delta, Space.World);
            yield return null;
        }

        //리스폰 할지 안할지
        if (isRespawn && spawner != null)
        {
            //리스폰
            spawner.Respawn(this, respawnTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Respawn()
    {
        transform.position = startPos;
        myCol.enabled = true;
        myPerception.gameObject.SetActive(false);
        ChangeState(State.Create);
    }
}