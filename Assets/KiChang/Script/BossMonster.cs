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
        // ���� animator �������� ���
        var curAnimStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        // �ִϸ��̼� �̸��� IdleNormal �� �ƴϸ� Play
        if (curAnimStateInfo.IsName("Idle") == false)
            myAnim.Play("Idle", 0, 0);

        // ���Ͱ� Idle ������ �� �θ��� �Ÿ��� �ϴ� �ڵ�
        // 50% Ȯ���� ��/��� ���� ����
        int dir = Random.Range(0f, 1f) > 0.5f ? 1 : -1;

        // ȸ�� �ӵ� ����
        float lookSpeed = Random.Range(25f, 40f);

        // IdleNormal ��� �ð� ���� ���ƺ���
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
            // SetDestination �� ���� �� frame�� �ѱ������ �ڵ�
            yield return null;
        }

        // ��ǥ������ ���� �Ÿ��� ���ߴ� �������� �۰ų� ������
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // StateMachine �� �������� ����
            ChangeState(State.Attack);
        }
        // ��ǥ���� �Ÿ��� �־��� ���
        else if (agent.remainingDistance > 0)
        {
            myTarget = null;
            agent.SetDestination(transform.position);
            yield return null;
            // StateMachine �� ���� ����
            ChangeState(State.Idle);
        }
        else
        {
            // WalkFWD �ִϸ��̼��� �� ����Ŭ ���� ���
            yield return StartCoroutine(CancelableWait(0.5f));
        }
    }
    IEnumerator Attack()
    {
        var curAnimStateInfo = myAnim.GetCurrentAnimatorStateInfo(0);

        // ���� �ִϸ��̼��� ���� �� Idle Battle �� �̵��ϱ� ������ 
        // �ڵ尡 �� ������ ���� ������ Attack01 �� Play
        yield return new WaitUntil(() =>
        myAnim.GetCurrentAnimatorStateInfo(0).IsName("GetHit") == false);

        myAnim.Play("Attack1", -1, 0);

        // �Ÿ��� �־�����
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            // StateMachine�� �������� ����
            ChangeState(State.Chase);
        }
        else
        {
            // ���� animation �� �� �踸ŭ ���
            // �� ��� �ð��� �̿��� ���� ������ ������ �� ����.
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
        // Sphere Collider �� Player �� �����ϸ�      
        myTarget = other.transform;
        // NavMeshAgent�� ��ǥ�� Player �� ����
        agent.SetDestination(myTarget.position);
        // StateMachine�� �������� ����
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
