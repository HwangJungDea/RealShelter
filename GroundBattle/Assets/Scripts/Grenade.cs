using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다른 물체와 부딪히는 순간 반경 3M내의 Enemy에게 3데미지를 주고싶다.
//폭발시각효과도 표현하고 싶다.
public class Grenade : MonoBehaviour
{
    public float radius = 3;
    public GameObject explosionFactory;

    private void OnCollisionEnter(Collision collision) //부딪힌것을 기준으로 무언가를 하겠다는것.
    {
        // 반경 3M내의 Enemy들에게 데미지 3을 주고싶다.

        // 1. 반경 3M내의 Enemy목록을 찾고 싶다.
        int layerMask = 1 << LayerMask.NameToLayer("Enemy");// |;1<<<< LayerMask.NameToLayer("이름")
        Collider[] cols= Physics.OverlapSphere(transform.position, radius, layerMask); //반경안의 레이어에서 Enemy를 찾는과정.
        //cols 에너미정보 복습확정.
        for (int i = 0; i < cols.Length; i++) //cols.Length cols의 갯수만큼 이것을 반복한다. cols정보는 위에서 찾아놨다.
        {
            //print(cols[i].gameObject.name);
            // 2. 목록안의 Enemy게임오브젝트에서 Enemy컴포넌트를 가져오고 싶다.
            Enemy enemy = cols[i].GetComponent<Enemy>();
            //여기서 위에 받은 정보를 기반으로 반복하며 이걸 함으로써 하나씩 증가.
            
            // 3. Enemy 컴포넌트에게 3데미지를 주고 싶다.
            enemy.TakeDamage(3);

            //결국 위에걸 하면 반경 3미터안의 에너미들에게 전부다 3데미지 입힌다.
        }

        GameObject exp = Instantiate(explosionFactory);
        exp.transform.position = transform.position;
        //나죽자
        Destroy(gameObject);

    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
