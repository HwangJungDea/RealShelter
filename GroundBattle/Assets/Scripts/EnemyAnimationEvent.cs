using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//이벤트만 감지해서 전달만 해주는 녀석
public class EnemyAnimationEvent : MonoBehaviour
{
    public Enemy enemy;

    //private void Awake()
    //{
    //    enemy = GetComponentInParent<Enemy>();
    //}
    //public Enemy를 private로 쓸려면 여기 생략된거 써야되겠지? 상식적으로....으음 뭐를 써야되는지는 아직 판단이 안선다 하는 법아는게 중요.


    //타격순간(Enemy -> Player)
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

    //공격동작종료순간
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
