using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonster : MonoBehaviour
{
    protected Transform myTarget = null;
    public LayerMask enemyMask;
    NavMeshAgent agent;
    Animator anim;
    private Coroutine moveStop;



    public enum State
    {
        Idle, Chase, Attack, Killed
    }

    public State myState = State.Idle;

    
    void Start()
    {
        
        this.anim = this.GetComponent<Animator>();

        this.agent = this.GetComponent<NavMeshAgent>();
       

   
       // StartCoroutine(StateMachine());
    }

    /*IEnumerator StateMachine()
    {
        while (myState != State.Killed)
        {
            yield return StartCoroutine(myState.ToString());
        }

        yield return StartCoroutine(State.Killed.ToString());
    }
   
    IEnumerator IDle()
    {
        // 현재 animator 상태정보 얻기
        var curAnimStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        // 애니메이션 이름이 IdleNormal 이 아니면 Play
        if (curAnimStateInfo.IsName("Idle") == false)
            myAnim.Play("Idle", 0, 0);

        // 몬스터가 Idle 상태일 때 두리번 거리게 하는 코드
        // 50% 확률로 좌/우로 돌아 보기
        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;

        // 회전 속도 설정
        float lookSpeed = Random.Range(25f, 40f);

        // IdleNormal 재생 시간 동안 돌아보기
        for (float i = 0; i < curAnimStateInfo.length; i += Time.deltaTime)
        {
            if (myState == State.Killed) break;
            transform.localEulerAngles = new Vector3(0f, transform.localEulerAngles.y +
                (dir) * Time.deltaTime * lookSpeed, 0f);
            yield return null;
        }
    }
    IEnumerator Chase()
    {
        var curAnimStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        if (curAnimStateInfo.IsName("Walk") == false)
        {
            myAnim.Play("Walk", -1, 0);
            // SetDestination 을 위해 한 frame을 넘기기위한 코드
            yield return null;
        }

        // 목표까지의 남은 거리가 멈추는 지점보다 작거나 같으면
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // StateMachine 을 공격으로 변경
            ChangeState(State.Attack);
        }
        // 목표와의 거리가 멀어진 경우
        else if (agent.remainingDistance > 0)
        {
            myTarget = null;
            agent.SetDestination(transform.position);
            yield return null;
            // StateMachine 을 대기로 변경
            ChangeState(State.Idle);
        }
        else
        {
            // WalkFWD 애니메이션의 한 사이클 동안 대기
            yield return StartCoroutine(CancelableWait(0.5f));
        }
    }
    IEnumerator Attack()
    {
        var curAnimStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        // 공격 애니메이션은 공격 후 Idle Battle 로 이동하기 때문에 
        // 코드가 이 지점에 오면 무조건 Attack01 을 Play
        yield return new WaitUntil(() =>
        myAnim.GetCurrentAnimatorStateInfo(0).IsName("GetHit") == false);

        myAnim.Play("Attack1", -1, 0);

        // 거리가 멀어지면
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            // StateMachine을 추적으로 변경
            ChangeState(State.Chase);
        }
        else
        {
            // 공격 animation 의 두 배만큼 대기
            // 이 대기 시간을 이용해 공격 간격을 조절할 수 있음.
            yield return StartCoroutine(CancelableWait(curAnimStateInfo.length * 2f));
        }
    }
    IEnumerator Killed()
    {
        myAnim.Play("Die", -1, 0);
        yield return null;
    }
   
    void ChangeState(State newState)
    {
        myState = newState;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (myState == State.Killed) return;
        if (other.name != "Player") return;
        // Sphere Collider 가 Player 를 감지하면      
        myTarget = other.transform;
        // NavMeshAgent의 목표를 Player 로 설정
        agent.SetDestination(myTarget.position);
        // StateMachine을 추적으로 변경
        ChangeState(State.Chase);
    }*/
    void Update()
    {
        if (myTarget == null) return;

        if (myState == State.Attack)
            transform.LookAt(myTarget);
        agent.SetDestination(myTarget.position);
    }
   

}
