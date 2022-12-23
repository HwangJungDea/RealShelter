using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //�����ð����� �����忡�� ���� ���� �� ��ġ�� ��ġ��Ű�� ,ȸ���� ��ġ��Ű�� �ʹ�.
    // Start is called before the first frame update

    float currentTime;
    public float createTime = 1f;
    public GameObject enemyFactory; //���� ������ �ʾƼ� �ַ� ���ϸ� �ȵȴ�. ��ü�� ����.
    void Start()
    {
        //StartCoroutine("IECreateEnemy");
        //Invoke("IECreateEnemy", createTime); //���ȣ��
    }
    //IEnumerator IECreateEnemy()
    //{
    //    while (true)  //while�� ������Ʈ���� ����? ������.      //for (; ; )   
    //    {
    //        //==================================================
    //        //���� �������� �ִ���������� �۴ٸ�.
    //        yield return new WaitForSeconds(createTime);
    //        //3. �����忡�� ���� ����
    //        GameObject enemy = Instantiate(enemyFactory);

    //        //4. ����ġ�� ��ġ��Ű��
    //        enemy.transform.position = transform.position;
    //        //5. ȸ���� ��ġ��Ű��
    //        enemy.transform.rotation = transform.rotation; //������� ���⸸ �Ȱ���.
    //        //================================================
    //        //6. ����ð��� �ʱ�ȭ �ϰ� �ʹ�.
    //        //Invoke("IECreateEnemy", createTime);
    //        currentTime = 0;
    //    }
    //}

    // Update is called once per frame
    void Update() //�̷��� ������Ʈ�ȵ�. �� ������Ʈ�� ���� �ڷ�ƾ���� �����ô�!
    {
        //1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime;
        //2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            //==================================================
            //���� �������� �ִ������(MaxCreateCount)���� �۴ٸ�.
            

            if (SpawnManager.instance.createCount < SpawnManager.instance.MaxCreateCount)
            {
                SpawnManager.instance.createCount++;
            //3. �����忡�� ���� ����
            GameObject enemy = Instantiate(enemyFactory);

            //4. ����ġ�� ��ġ��Ű��
            enemy.transform.position = transform.position;
            //5. ȸ���� ��ġ��Ű��
            enemy.transform.rotation = transform.rotation; //������� ���⸸ �Ȱ���.
            //==================================================
            }
            //6. ����ð��� �ʱ�ȭ �ϰ� �ʹ�.
            currentTime = 0;
        }

    }
}
