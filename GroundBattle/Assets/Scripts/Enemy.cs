using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//agent를 이용해서 플레이어를 향해서 이동하고 싶다.
public class Enemy : MonoBehaviour
{
    public enum State   //열거형
    {
        Search,
        Move,
        Attack,
        Damage,
        Death,
        //억지로 넣지않는 이상 절대로 같은 값이 안된다.
    }

    //Death 애니메이션이 완료되면 Enemy를 파괴하고 싶다.
    internal void OnDeathFinished() //internal void 애니메이션에 구간을 채크해서 실행한다는 것
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (SpawnManager.instance != null)
        {
            //Enemy가 파괴될 때 killCount를 하나 증가시키고 싶다.
            SpawnManager.instance.killCount++;
            //만약 killCount가 NeedKillCount이상이라면
            SpawnManager.instance.CheckLevelUp();
        }


        //레벨업하고 싶다.
    }

    public float DamageDistance = 2f;
    internal void OmDamageFinished() //internal은 public나 비슷하다. 좀더 보안?ㅅ?
    {

        //만약 state가 Death라면
        if (state == State.Death)
        {

            //state는 Death로  바뀌었지만 애니메이션이 씹힌상태이다.
            //Death 애니메이션을 재생하고 싶다.
            anim.SetTrigger("Death");
            //함수를 바로 종료하고 싶다.
            return;
        }



        //나의 상태를 이동 혹은 공격상태로 전이하고 싶다.
        //만약 타겟과 나의 거리가 공격 가능 거리라면

        //agent.destination = player.transform.position;  // agent.SetDestination(player.transform.position); 동일

        //적공격
        //만약 타겟과의 거리가 공격가능 거리보다 작다면
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < DamageDistance)
        {
            //공격상태로 전이하고 싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
        else
        {
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false; //멈추는 기능아 사라져!
            //agent야 다시 움직여!
        }

        //공격상태로 전이하고
        //그렇지 않다면 이동상태로 전이하고 싶다.
        print("OmDamageFinished");
    }

    public State state;

    GameObject player;
    public float speed = 5f;
    NavMeshAgent agent;
    public Animator anim;
    public int demage = 1;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //태어나면서 플레이어를 찾고싶다.
        state = State.Search;

        agent.Warp(transform.position);

        anim = GetComponent<Animator>();

    }




    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Search:
                UpdateSearch();
                break; //위기탈출넘버원 이곳을 빠져나오겠다는 의미임.
            case State.Move:
                UpdateMove();
                break;
            case State.Attack:
                UpdateAttack();
                break;
                // default://위의 내용이 없으면 이놈까지 오는거임.
                //굳이 없다면 최적화에 더 도움됨.
                //   break;
                //같은 내용이 중복되면 에러가 나온다. 
        }

        //if (state == State.Search)
        //{
        //    UpdateSearch();
        //    //만약 적을 찾았다면
        //    //이동상태로 전이하고 싶다.
        //}
        //else if (state == State.Move)
        //{
        //    UpdateMove();
        //}
        //else if (state == State.Attack)
        //{
        //    UpdateAttack();

        //}
        ////이동




    }

    EnemyHP ehp; //효율업.
    internal void TakeDamage(int dmgAmount)
    {
        if (state == State.Death)
        {
            return;
        }
        //만약 state가 Death라면 함수를 바로 종료하고 싶다.

        if (ehp == null)
        {
            ehp = gameObject.GetComponent<EnemyHP>(); //요놈시키가 널일때만 다시 체크해주기.
        }
        //체력을 1 감소시키고 싶다.
        if (ehp != null) //좀더 안전하게 만드는 검증방법. if로 검증하기.
        {
            agent.isStopped = true; //멈추라는 기능.
                                    //agent야 멈춰하고 싶다.

            //체력을 dmgAmount만큼 감소 시키고 싶다.
            //ehp.HP = ehp.HP - dmgAmount;
            ehp.HP -= dmgAmount;
            //체력이 0이하가되면 Enemy를 파괴하고 싶다.
            if (ehp.HP <= 0)
            {

                //죽음상태로 전이하고 싶다.
                state = State.Death;
                anim.SetTrigger("Death");
                gameObject.layer = LayerMask.NameToLayer("EnemyDeath"); //게임 오브젝트 레이어를 EnemyDeath로 바꾼다.

            }
            else
            {
                //데미지 상태로 전이하고 싶다.
                state = State.Damage;
                anim.SetTrigger("Damage");
            }
        }
    }

    private void UpdateAttack()
    {
        //타겟을 바라보고 싶다.
        //즉각 회전
        //transform.LookAt(new Vector3 (player.transform.position.x, transform.position.y,player.transform.position.z));
        //타겟을 부드럽게 바라보고 싶다.
        Vector3 dir = player.transform.position - transform.position;//플레이어-내위치
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir); //이쪽 방향으로 회전하겠다.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);



        //타겟에게 데미지를 입히고 싶다.
        //state = State.Search;
        //anim.SetTrigger("Move");
        //애니메이션으로 해결해서 애는 필요없어짐.
    }
    //공격가능거리
    public float attackDistance = 2f;
    private void UpdateMove()
    {
        agent.destination = player.transform.position;  // agent.SetDestination(player.transform.position); 동일
        //적공격
        //만약 타겟과의 거리가 공격가능 거리보다 작다면
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            //공격상태로 전이하고 싶다.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }

    }

    private void UpdateSearch()
    {
        //적을 찾고싶다.
        player = GameObject.Find("Player");
        if (player != null)
        {
            state = State.Move;
            anim.SetTrigger("Move");
        }

    }

    //타격순간(Enemy -> Player)
    public void OnAttackHit()
    {
        print("OnAttackHit");
        //만약 타겟이 공격 사정거리 안에 있다면
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            HitManager.instance.DoHit(demage, Action);
          //  HitManager.instance.DoHit(demage, () =>{
          //        번쩍이 끝나면 들어오게 된다. 즉 시간이 다르다 앞에 끝나고 시작한다.
          //        요밖공간의 시간과 여기의 시간은 다르게 흘러간다.
          //  }); 람다식? 이걸 쭉쭉쭉 1,2,3대 떄리고때리고때린다 

        }
        //Hit를 하고 싶다.

        void Action()
        {

        }

    }

    //공격동작종료순간
    public void OnAttackFinished()
    {
        print("OnAttackFinished");
        // 만약 타겟이 공격 사정거리 밖에 있다면
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= attackDistance)
        {
            state = State.Move;
            //anim.SetTrigger("Move");
            //anim.Play("Move", 0, 0);//Layer : 하체는 이동 상체는 공격같은것도 가능하다.
            //조합이 가능하니 여러가지를 할 수 있다.
            //타임.

            anim.CrossFade("Move", 0.1f, 0); //줄이면 자연스러워진다. 둘이 썩어서 사용한다는 뜻.
                                             // 이동 상태로 전이하고 싶다. 
        }
    }
}
