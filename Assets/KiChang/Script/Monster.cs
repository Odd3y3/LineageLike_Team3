using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : AIMovement
{

    public enum State
    {
        Create, Normal, Roaming, Battle, Dead
    }

    public State myState = State.Create;

    Vector3 startPos = Vector3.zero;

    public Transform barPoint = null;
    public Transform uiHpBars = null;

    MonsterStatBar myStatUI = null;

    protected Transform myTarget = null;

    void ChangeState(State s)
    {
        if (myState == s) return;
        myState = s;
        switch (myState)
        {
            case State.Normal:
                Vector3 rndDir = Vector3.forward;
                Quaternion rndRot = Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0);
                float dist = Random.Range(0.0f, 5.0f);
                rndDir = rndRot * rndDir * dist;
                Vector3 rndPos = startPos + rndDir;

                MoveToPos(rndPos, () => StartCoroutine(Waiting(Random.Range(1.0f, 3.0f))));
                ChangeState(State.Roaming);
                break;
            case State.Battle:
                AttackTarget(myPerception.myTarget);
                break;
            case State.Dead:
                GetComponent<Collider>().enabled = false;
                StopAllCoroutines();
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

       
        myStatUI = Instantiate(Resources.Load("UI\\HpBar") as GameObject,
            SceneData.Inst.HpBars).GetComponent<MonsterStatBar>();
        myStatUI.Initialize(barPoint);
        myHpBar = myStatUI.mySlider;

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
    protected override void OnDead()
    {
        ChangeState(State.Dead);
    }
   
    public void DisAppear()
    {
        StartCoroutine(DisAppearing(0.5f, 2.0f));
    }
    IEnumerator DisAppearing(float speed, float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(myStatUI.gameObject);
        float dist = 1.0f;
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
