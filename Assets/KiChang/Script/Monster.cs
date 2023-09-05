using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : AImovement
{
    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }
    public bool isRoaming = true;

    public State myState = State.Create;

    Vector3 startPos = Vector3.zero;

    public Transform barPoint = null;
    Transform uiHpBars = null;

    GameObject hpBarObj = null;

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
                GetComponent<Collider>().enabled = false;
                StopAllCoroutines();
                myAnim.SetTrigger("Die");
                DisAppear();
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

    // Start is called before the first frame update
    void Start()
    {
        Initialize();


        hpBarObj = Instantiate(Resources.Load("UI\\EnemyHPBar") as GameObject,
            GameObject.Find("DynamicCanvas").transform);
        myHpBar = hpBarObj.GetComponent<Slider>();
        hpBarObj.GetComponent<EnemyHPBar>().SetTarget(transform);
        //myStatUI.Initialize(barPoint);

        startPos = transform.position;
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
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

        if(!IsLive)
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
        Destroy(gameObject);
    }
}