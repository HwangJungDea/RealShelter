using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�ٸ� ��ü�� �ε����� ���� �ݰ� 3M���� Enemy���� 3�������� �ְ�ʹ�.
//���߽ð�ȿ���� ǥ���ϰ� �ʹ�.
public class Grenade : MonoBehaviour
{
    public float radius = 3;
    public GameObject explosionFactory;

    private void OnCollisionEnter(Collision collision) //�ε������� �������� ���𰡸� �ϰڴٴ°�.
    {
        // �ݰ� 3M���� Enemy�鿡�� ������ 3�� �ְ�ʹ�.

        // 1. �ݰ� 3M���� Enemy����� ã�� �ʹ�.
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");// |;1<<<< LayerMask.NameToLayer("�̸�")
        Collider[] cols= Physics.OverlapSphere(transform.position, radius, layerMask); //�ݰ���� ���̾�� Enemy�� ã�°���.
        //cols ���ʹ����� ����Ȯ��.
        for (int i = 0; i < cols.Length; i++) //cols.Length cols�� ������ŭ �̰��� �ݺ��Ѵ�. cols������ ������ ã�Ƴ���.
        {
            //print(cols[i].gameObject.name);
            // 2. ��Ͼ��� Enemy���ӿ�����Ʈ���� Enemy������Ʈ�� �������� �ʹ�.
            Enemy enemy = cols[i].GetComponent<Enemy>();
            //���⼭ ���� ���� ������ ������� �ݺ��ϸ� �̰� �����ν� �ϳ��� ����.
            
            // 3. Enemy ������Ʈ���� 3�������� �ְ� �ʹ�.
            enemy.TakeDamage(3);

            //�ᱹ ������ �ϸ� �ݰ� 3���;��� ���ʹ̵鿡�� ���δ� 3������ ������.
        }

        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = transform.position;
        //������
        Destroy(gameObject);

    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
