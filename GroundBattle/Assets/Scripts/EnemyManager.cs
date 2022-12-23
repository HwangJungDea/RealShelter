using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //일정시간마다 적공장에서 적을 만들어서 내 위치에 위치시키고 ,회전도 일치시키고 싶다.
    // Start is called before the first frame update

    float currentTime;
    public float createTime = 1f;
    public GameObject enemyFactory; //씬에 있지도 않아서 애로 멀하면 안된다. 실체가 없다.
    void Start()
    {
        //StartCoroutine("IECreateEnemy");
        //Invoke("IECreateEnemy", createTime); //재기호출
    }
    //IEnumerator IECreateEnemy()
    //{
    //    while (true)  //while을 업데이트에서 쓴다? 뒤진다.      //for (; ; )   
    //    {
    //        //==================================================
    //        //만약 생성수가 최대생성수보다 작다면.
    //        yield return new WaitForSeconds(createTime);
    //        //3. 적공장에서 적을 만들어서
    //        GameObject enemy = Instantiate(enemyFactory);

    //        //4. 내위치에 위치시키고
    //        enemy.transform.position = transform.position;
    //        //5. 회전도 일치시키고
    //        enemy.transform.rotation = transform.rotation; //뭐가됬든 방향만 똑같이.
    //        //================================================
    //        //6. 현재시간을 초기화 하고 싶다.
    //        //Invoke("IECreateEnemy", createTime);
    //        currentTime = 0;
    //    }
    //}

    // Update is called once per frame
    void Update() //이러면 업데이트안됨. 이 업데이트를 위에 코루틴으로 만들어봅시다!
    {
        //1. 시간이 흐르다가
        currentTime += Time.deltaTime;
        //2. 만약 현재시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            //==================================================
            //만약 생성수가 최대생성수(MaxCreateCount)보다 작다면.
            

            if (SpawnManager.instance.createCount < SpawnManager.instance.MaxCreateCount)
            {
                SpawnManager.instance.createCount++;
            //3. 적공장에서 적을 만들어서
            GameObject enemy = Instantiate(enemyFactory);

            //4. 내위치에 위치시키고
            enemy.transform.position = transform.position;
            //5. 회전도 일치시키고
            enemy.transform.rotation = transform.rotation; //뭐가됬든 방향만 똑같이.
            //==================================================
            }
            //6. 현재시간을 초기화 하고 싶다.
            currentTime = 0;
        }

    }
}
