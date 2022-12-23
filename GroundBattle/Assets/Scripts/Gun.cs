using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�Ѿ��� �����ϰ� �ʹ�.
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
        //���� ���콺 ������ ��ư�� ������ ������ ZoomIn
        if (Input.GetButton("Fire2"))
        {

            Camera.main.fieldOfView = fovTarget = zoomInValue;
            zoomTargetSpeed = zoomInSpeed;
            gunTarget = zoomInPosition.localPosition;


            //��� �ؾߵ� ����� ������ ���ȸ� ȣ��ȴ�.
            //Camera.main.fieldOfView = Mathf.Lerp
            //    (Camera.main.fieldOfView, zoomInValue, Time.deltaTime*5);

        }
        //�׷��� �ʰ� ���콺 ������ ��ư�� ���� ZoomOut
        else if (Input.GetButtonUp("Fire2"))
        {
            fovTarget = zoomOutValue;
            zoomTargetSpeed = zoomOutSpeed;
            gunTarget = zoomOutPosition.localPosition;

        }

        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fovTarget, Time.deltaTime * zoomTargetSpeed); ;

        //�����ʹ�ư�� ������ ������ ���� �ϰ� ���ϸ� ���� ���Ѵ�.
        gun.localPosition = Vector3.Lerp(gun.localPosition, gunTarget, Time.deltaTime * zoomTargetSpeed);




    }

    public GameObject grenadeFactory;
    public Transform grenadePosition;
    public float throwPower = 10;
    private void UpdateThrowGrenade()
    {

        //������ �ٵ� �����ͼ� �ű�ٰ� ���� ���ϸ� �ȴ�.
        //������Ʈ�� �����ö��� ������ �� �־ ������ �־�ߵȴ�.
        if (Input.GetKeyDown(KeyCode.G))
        // ���� ���� : ī�޶��� �չ���
        //1. ��ź���忡�� ��ź�� �����
        {
            GameObject grenade = Instantiate(grenadeFactory);
            //2. ��ź�� �ѱ���ġ�� �����ٳ���
            grenade.transform.position = grenadePosition.transform.position;
            //3. ��ź���� Rigidbody������Ʈ�� �����ͼ�
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            //4. Rigidbody�� ī�޶��� �չ������� ���� ���ϰ� �ʹ�.
            //5. �̼� �� ���� 45�� �������� ���� ���ϰ� �ʹ�.

            //Vector3 dir = Camera.main.transform.forward + Camera.main.transform.up; //45���� ������ 
            //���Ʒ��� ��������.                              //�̰� ���� �������� ó�� ����°�.

            Vector3 dir = Vector3.forward + Vector3.up; //�������
            dir = Camera.main.transform.TransformDirection(dir);    //���� �������� �ٽ� �����.
            dir.Normalize();

            rb.AddForce(dir * throwPower, ForceMode.Impulse);
            // rb.angularVelocity = transform.forward * 50;//right;//forward��
            rb.AddTorque(-transform.right * 50, ForceMode.Impulse); //�̷� ����.

            //�𷺼��� ���� ������ �ְ�ʹ�?


        }

    }



    // Update is called once per frame
    void UpdateShoot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            rifle.Reload();
        }

        //1. ����ڰ� ���콺 ���ʹ�ư�� ������
        if (Input.GetButtonDown("Fire1") && rifle.CanShoot())
        {
            rifle.Shoot();
            //2. ī�޶���ġ���� ī�޶�չ������� Ray�� �����
            Ray ray = new Ray(Camera.main.transform.position,
                                Camera.main.transform.forward);// ��ġüũ, ����üũ
            //3. ���� �ٶ� �� �ε����ٸ�
            RaycastHit hitInfo;
            int layerMask = ~(1 << LayerMask.NameToLayer("EnemyDeath")); // (�ȿ� �����Ѵ�.) EnemyDeath ���̾�� �����Ѵ�.
            //int layer = 1 << LayerMask.NameToLayer("Enemy");
            //         |1 << LayerMask.NameToLayer("Enemy"); ���� �ѹ��� �����ϰ� �ʹ�. ���� |�� ���ϱ� ����.
            //    int layer = 1 << ~(LayerMask.NameToLayer("Enemy")); �ݴ�� �ϰڴٴ� ��. �� ���ʹ� ���� �� �޾Ƶ��δ�.

            //if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))

            {
                //4. �ε��� ���� �Ѿ��ڱ����忡�� �Ѿ��ڱ��� ����
                GameObject bi = Instantiate(bulletlmpactFactory);
                //5. �� ��ġ�� ��ġ�ϰ� �ʹ�.
                bi.transform.position = hitInfo.point;
                //point : ������ �ε��� �κ�

                bi.transform.forward = hitInfo.normal;
                //�ߺ��̰� �ҷ��� ������.

                //Player�� Enemy���� Damage�� ������
                //���� �ε������� Enemy��� EnemyHP������Ʈ�� �����ͼ�
                if (hitInfo.transform.name.Contains("Enemy"))// �ȿ� Enemy�� �����ϰ� �ִ°� �����ϱ�.
                {
                    Enemy enemy = hitInfo.transform.GetComponent<Enemy>();

                    enemy.TakeDamage(1); //�ؿ����� ���ʹ̷� �Ѿ����.


                    //EnemyHP ehp = hitInfo.transform.GetComponent<EnemyHP>();
                    ////ü���� 1 ���ҽ�Ű�� �ʹ�.
                    //if (ehp != null) //���� �����ϰ� ����� �������. if�� �����ϱ�.
                    //{
                    //    ehp.HP--;
                    //    //ü���� 0���ϰ��Ǹ� Enemy�� �ı����� �ʹ�.
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


