using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�̺�Ʈ�� �����ؼ� ���޸� ���ִ� �༮
public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;

    //private void Awake()
    //{
    //    enemy = GetComponentInParent<Enemy>();
    //}
    //public Enemy�� private�� ������ ���� �����Ȱ� ��ߵǰ���? ���������....���� ���� ��ߵǴ����� ���� �Ǵ��� �ȼ��� �ϴ� ���ƴ°� �߿�.


    //Ÿ�ݼ���(Enemy -> Player)
    public void OnDeathFinished()
    {
        enemy.OnDeathFinished();
    }
    public void OmDamageFinished()
    {
        enemy.OmDamageFinished();
    }

    public void OnAttackHit()
    {
        enemy.OnAttackHit();
    }

    //���ݵ����������
    public void OnAttackFinished()
    {
        enemy.OnAttackFinished();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
