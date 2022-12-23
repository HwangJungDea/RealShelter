using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//총알을 제한하고 싶다.
public class Gun : MonoBehaviour
{
    public Rifle rifle;

    public GameObject bulletlmpactFactory;
    void Start()
    {
        gunTarget = zoomOutPosition.localPosition;
    }

    private void Update()
    {
        UpdateZoom();
        UpdateShoot();
        UpdateThrowGrenade();
    }

    public float zoomInValue = 10f;
    public float zoomOutValue = 60f;
    public float zoomInSpeed = 10f;
    float zoomOutSpeed = 5f;
    float fovTarget = 60f;
    float zoomTargetSpeed = 5f;

    public Transform gun;
    public Transform zoomInPosition;
    public Transform zoomOutPosition;
    Vector3 gunTarget;

    private void UpdateZoom()
    {
        //만약 마우스 오른쪽 버튼을 누르고 있으면 ZoomIn
        if (Input.GetButton("Fire2"))
        {

            Camera.main.fieldOfView = fovTarget = zoomInValue;
            zoomTargetSpeed = zoomInSpeed;
            gunTarget = zoomInPosition.localPosition;


            //계속 해야될 놈들이 누르는 동안만 호출된다.
            //Camera.main.fieldOfView = Mathf.Lerp
            //    (Camera.main.fieldOfView, zoomInValue, Time.deltaTime*5);

        }
        //그렇지 않고 마우스 오른쪽 버튼을 떼면 ZoomOut
        else if (Input.GetButtonUp("Fire2"))
        {
            fovTarget = zoomOutValue;
            zoomTargetSpeed = zoomOutSpeed;
            gunTarget = zoomOutPosition.localPosition;

        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovTarget, Time.deltaTime * zoomTargetSpeed); ;

        //오른쪽버튼을 누르고 있으면 줌을 하고 안하면 줌을 안한다.
        gun.localPosition = Vector3.Lerp(gun.localPosition, gunTarget, Time.deltaTime * zoomTargetSpeed);




    }

    public GameObject grenadeFactory;
    public Transform grenadePosition;
    public float throwPower = 10;
    private void UpdateThrowGrenade()
    {

        //리지드 바디를 가져와서 거기다가 힘을 가하면 된다.
        //컴포넌트를 가져올때는 누구의 즉 주어가 무조건 있어야된다.
        if (Input.GetKeyDown(KeyCode.G))
        // 던질 방향 : 카메라의 앞방향
        //1. 폭탄공장에서 폭탄을 만들고
        {
            GameObject grenade = Instantiate(grenadeFactory);
            //2. 폭탄을 총구위치에 가져다놓고
            grenade.transform.position = grenadePosition.transform.position;
            //3. 폭탄에게 Rigidbody컴포넌트를 가져와서
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            //4. Rigidbody에 카메라의 앞방향으로 힘을 가하고 싶다.
            //5. 미션 내 앞쪽 45도 방향으로 힘을 가하고 싶다.

            //Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up; //45도로 던지기 
            //위아래는 같은거임.                              //이건 나를 기준으로 처음 만드는거.

            Vector3 dir = Vector3.forward + Vector3.up; //만든다음
            dir = Camera.main.transform.TransformDirection(dir);    //나를 기준으로 다시 만든다.
            dir.Normalize();

            rb.AddForce(dir * throwPower, ForceMode.Impulse);
            // rb.angularVelocity = transform.forward * 50;//right;//forward등
            rb.AddTorque(-transform.right * 50, ForceMode.Impulse); //이런 느낌.

            //디렉션을 만들어서 각도를 주고싶다?


        }

    }



    // Update is called once per frame
    void UpdateShoot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rifle.Reload();
        }

        //1. 사용자가 마우스 왼쪽버튼을 누르면
        if (Input.GetButtonDown("Fire1") && rifle.CanShoot())
        {
            rifle.Shoot();
            //2. 카메라위치에서 카메라앞방향으로 Ray를 만들고
            Ray ray = new Ray(Camera.main.transform.position,
                                Camera.main.transform.forward);// 위치체크, 방향체크
            //3. 만약 바라본 후 부딪혔다면
            RaycastHit hitInfo;
            int layerMask = ~(1 << LayerMask.NameToLayer("EnemyDeath")); // (안에 먼저한다.) EnemyDeath 레이어는 무시한다.
            //int layer = 1 << LayerMask.NameToLayer("Enemy");
            //         |1 << LayerMask.NameToLayer("Enemy"); 둘을 한번에 관리하고 싶다. 앞의 |는 더하기 느낌.
            //    int layer = 1 << ~(LayerMask.NameToLayer("Enemy")); 반대로 하겠다는 뜻. 즉 에너미 빼고 다 받아들인다.

            //if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))

            {
                //4. 부딪힌 곳에 총알자국공장에서 총알자국을 만들어서
                GameObject bi = Instantiate(bulletlmpactFactory);
                //5. 그 위치에 배치하고 싶다.
                bi.transform.position = hitInfo.point;
                //point : 실제로 부딪힌 부분

                bi.transform.forward = hitInfo.normal;
                //잘보이게 할려고 넣은거.

                //Player가 Enemy에게 Damage를 입히면
                //만약 부딪힌것이 Enemy라면 EnemyHP컴포넌트를 가져와서
                if (hitInfo.transform.name.Contains("Enemy"))// 안에 Enemy를 포함하고 있는가 검증하기.
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

                    enemy.TakeDamage(1); //밑에넘이 에너미로 넘어간거임.


                    //EnemyHP ehp = hitInfo.transform.GetComponent<EnemyHP>();
                    ////체력을 1 감소시키고 싶다.
                    //if (ehp != null) //좀더 안전하게 만드는 검증방법. if로 검증하기.
                    //{
                    //    ehp.HP--;
                    //    //체력이 0이하가되면 Enemy를 파괴ㅏ고 싶다.
                    //    if (ehp.HP <= 0)
                    //    {
                    //        Destroy(hitInfo.transform.gameObject);
                    //    }
                    //}
                }
            }

        }
    }

}


