using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//agent�� �̿��ؼ� �÷��̾ ���ؼ� �̵��ϰ� �ʹ�.
public class Enemy : MonoBehaviour
{
    public enum State   //������
    {
        Search,
        Move,
        Attack,
        Damage,
        Death,
        //������ �����ʴ� �̻� ����� ���� ���� �ȵȴ�.
    }

    //Death �ִϸ��̼��� �Ϸ�Ǹ� Enemy�� �ı��ϰ� �ʹ�.
    internal void OnDeathFinished() //internal void �ִϸ��̼ǿ� ������ äũ�ؼ� �����Ѵٴ� ��
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (SpawnManager.instance != null)
        {
            //Enemy�� �ı��� �� killCount�� �ϳ� ������Ű�� �ʹ�.
            SpawnManager.instance.killCount++;
            //���� killCount�� NeedKillCount�̻��̶��
            SpawnManager.instance.CheckLevelUp();
        }


        //�������ϰ� �ʹ�.
    }

    public float DamageDistance = 2f;
    internal void OmDamageFinished() //internal�� public�� ����ϴ�. ���� ����?��?
    {

        //���� state�� Death���
        if (state == State.Death)
        {

            //state�� Death��  �ٲ������ �ִϸ��̼��� ���������̴�.
            //Death �ִϸ��̼��� ����ϰ� �ʹ�.
            anim.SetTrigger("Death");
            //�Լ��� �ٷ� �����ϰ� �ʹ�.
            return;
        }



        //���� ���¸� �̵� Ȥ�� ���ݻ��·� �����ϰ� �ʹ�.
        //���� Ÿ�ٰ� ���� �Ÿ��� ���� ���� �Ÿ����

        //agent.destination = player.transform.position;  // agent.SetDestination(player.transform.position); ����

        //������
        //���� Ÿ�ٰ��� �Ÿ��� ���ݰ��� �Ÿ����� �۴ٸ�
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < DamageDistance)
        {
            //���ݻ��·� �����ϰ� �ʹ�.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }
        else
        {
            state = State.Move;
            anim.SetTrigger("Move");
            agent.isStopped = false; //���ߴ� ��ɾ� �����!
            //agent�� �ٽ� ������!
        }

        //���ݻ��·� �����ϰ�
        //�׷��� �ʴٸ� �̵����·� �����ϰ� �ʹ�.
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
        //�¾�鼭 �÷��̾ ã��ʹ�.
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
                break; //����Ż��ѹ��� �̰��� ���������ڴٴ� �ǹ���.
            case State.Move:
                UpdateMove();
                break;
            case State.Attack:
                UpdateAttack();
                break;
                // default://���� ������ ������ �̳���� ���°���.
                //���� ���ٸ� ����ȭ�� �� �����.
                //   break;
                //���� ������ �ߺ��Ǹ� ������ ���´�. 
        }

        //if (state == State.Search)
        //{
        //    UpdateSearch();
        //    //���� ���� ã�Ҵٸ�
        //    //�̵����·� �����ϰ� �ʹ�.
        //}
        //else if (state == State.Move)
        //{
        //    UpdateMove();
        //}
        //else if (state == State.Attack)
        //{
        //    UpdateAttack();

        //}
        ////�̵�




    }

    EnemyHP ehp; //ȿ����.
    internal void TakeDamage(int dmgAmount)
    {
        if (state == State.Death)
        {
            return;
        }
        //���� state�� Death��� �Լ��� �ٷ� �����ϰ� �ʹ�.

        if (ehp == null)
        {
            ehp = gameObject.GetComponent<EnemyHP>(); //����Ű�� ���϶��� �ٽ� üũ���ֱ�.
        }
        //ü���� 1 ���ҽ�Ű�� �ʹ�.
        if (ehp != null) //���� �����ϰ� ����� �������. if�� �����ϱ�.
        {
            agent.isStopped = true; //���߶�� ���.
                                    //agent�� �����ϰ� �ʹ�.

            //ü���� dmgAmount��ŭ ���� ��Ű�� �ʹ�.
            //ehp.HP = ehp.HP - dmgAmount;
            ehp.HP -= dmgAmount;
            //ü���� 0���ϰ��Ǹ� Enemy�� �ı��ϰ� �ʹ�.
            if (ehp.HP <= 0)
            {

                //�������·� �����ϰ� �ʹ�.
                state = State.Death;
                anim.SetTrigger("Death");
                gameObject.layer = LayerMask.NameToLayer("EnemyDeath"); //���� ������Ʈ ���̾ EnemyDeath�� �ٲ۴�.

            }
            else
            {
                //������ ���·� �����ϰ� �ʹ�.
                state = State.Damage;
                anim.SetTrigger("Damage");
            }
        }
    }

    private void UpdateAttack()
    {
        //Ÿ���� �ٶ󺸰� �ʹ�.
        //�ﰢ ȸ��
        //transform.LookAt(new Vector3 (player.transform.position.x, transform.position.y,player.transform.position.z));
        //Ÿ���� �ε巴�� �ٶ󺸰� �ʹ�.
        Vector3 dir = player.transform.position - transform.position;//�÷��̾�-����ġ
        dir.Normalize();
        Quaternion targetRotation = Quaternion.LookRotation(dir); //���� �������� ȸ���ϰڴ�.
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * 5);



        //Ÿ�ٿ��� �������� ������ �ʹ�.
        //state = State.Search;
        //anim.SetTrigger("Move");
        //�ִϸ��̼����� �ذ��ؼ� �ִ� �ʿ������.
    }
    //���ݰ��ɰŸ�
    public float attackDistance = 2f;
    private void UpdateMove()
    {
        agent.destination = player.transform.position;  // agent.SetDestination(player.transform.position); ����
        //������
        //���� Ÿ�ٰ��� �Ÿ��� ���ݰ��� �Ÿ����� �۴ٸ�
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            //���ݻ��·� �����ϰ� �ʹ�.
            state = State.Attack;
            anim.SetTrigger("Attack");
        }

    }

    private void UpdateSearch()
    {
        //���� ã��ʹ�.
        player = GameObject.Find("Player");
        if (player != null)
        {
            state = State.Move;
            anim.SetTrigger("Move");
        }

    }

    //Ÿ�ݼ���(Enemy -> Player)
    public void OnAttackHit()
    {
        print("OnAttackHit");
        //���� Ÿ���� ���� �����Ÿ� �ȿ� �ִٸ�
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < attackDistance)
        {
            HitManager.instance.DoHit(demage, Action);
          //  HitManager.instance.DoHit(demage, () =>{
          //        ��½�� ������ ������ �ȴ�. �� �ð��� �ٸ��� �տ� ������ �����Ѵ�.
          //        ��۰����� �ð��� ������ �ð��� �ٸ��� �귯����.
          //  }); ���ٽ�? �̰� ������ 1,2,3�� �������������� 

        }
        //Hit�� �ϰ� �ʹ�.

        void Action()
        {

        }

    }

    //���ݵ����������
    public void OnAttackFinished()
    {
        print("OnAttackFinished");
        // ���� Ÿ���� ���� �����Ÿ� �ۿ� �ִٸ�
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist >= attackDistance)
        {
            state = State.Move;
            //anim.SetTrigger("Move");
            //anim.Play("Move", 0, 0);//Layer : ��ü�� �̵� ��ü�� ���ݰ����͵� �����ϴ�.
            //������ �����ϴ� ���������� �� �� �ִ�.
            //Ÿ��.

            anim.CrossFade("Move", 0.1f, 0); //���̸� �ڿ�����������. ���� �� ����Ѵٴ� ��.
                                             // �̵� ���·� �����ϰ� �ʹ�. 
        }
    }
}
