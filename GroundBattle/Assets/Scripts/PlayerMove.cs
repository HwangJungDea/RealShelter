using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//������� �Է¿� ���� �յ��¿�� �̵��ϰ� �ʹ�.
//������ �ٰ� �ʹ�. -> �߷�, �����ٴ���, Y�ӵ�

//���� ����Ʈ Ű�� ������ �߿� �޸��⸦ �ϰ� �ʹ�.
public class PlayerMove : MonoBehaviour
{

    public float speed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f; //�߷�
    public float jumpPower = 10f; // �����ٴ���
    float yVelocity; //Y�ӵ�
    CharacterController cc;


    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();//�ڱⲨ ������ gameObject��������.
    }


    public int maxJumpCount = 2;
    int jumpCount = 0;
    void Update()
    {
        //���� ���� ��Ҵٸ� ����ī��Ʈ�� 0���� �ʱ�ȭ �ϰ� �ʹ�.
        //cc.collisionFlags == CollisionFlags.Sides//�׳� ������.
        //if ((cc.collisionFlags & CollisionFlags.Above) != 0)
        //{
        //        if (true == cc.isGrounded) �����Ҹ�
        //}


        if (true == cc.isGrounded)
        {
            jumpCount = 0; //����ī��Ʈ �ʱ�ȭ
            yVelocity = 0; //�߷� �ʱ�ȭ

        }
        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            //��. Y�ӵ��� JumpPower�� �����ϰ� �ʹ�.
            yVelocity = jumpPower;
            jumpCount++;
        }

        {
            float finalSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                finalSpeed = runSpeed;
            }

            // 1. ������� �Է¿����� 
            // 2.�յ��¿�� ������ �����
            // 3. �� �������� �̵��ϰ� �ʹ�.
            float h = Input.GetAxisRaw("Horizontal"); //GetAxis�� float�� ��ȯ
            float v = Input.GetAxisRaw("Vertical");
            //�÷��̾� �˵�
            //�÷��̾� �¿�
            //���� �ְ� �ű⿡ ���ǵ� ���ϱ�.

            Vector3 dir = Vector3.right * h + Vector3.forward * v;
            //new Vector3(h,0,v);


            //����
            //if (Input.GetButton("Fire2"))
            //{
            //    dir = transform.TransformDirection(dir);
            //}
            //else
            {
                dir = Camera.main.transform.TransformDirection(dir);

                dir.y = 0;
                dir.Normalize(); //������ ũ�⸦ 1�� ���δ�. �밢�� �ӵ�����.
                                 //Camera�� ���������� ������ �ٲٰڴ�.
                                 //dir.y = 0; //y���� ����. �����̽��� �ȴ��������� 0�̴�.
            }
            //��
            Vector3 velocity = dir * finalSpeed;

            velocity.y = yVelocity;  //��. dir.y�� Y�ӵ��� �����ϰ� �ʹ�.

            cc.Move(velocity * Time.deltaTime);
            //transform.position += (dir * speed) * Time.deltaTime;
        }

        {
            yVelocity += gravity * Time.deltaTime;//1�ʿ� 30���� �׸��� 1/30�� ���Ѵ�.
                                                  //���ӵ� ������ ����Ŵ�.           //gravity/30�� 30�� ���Ѵٴ� �Ҹ�
                                                  //�� 1�ʵ����� gravity���밪�� yVelocity�� ����.


            //��. Y�ӵ��� �߷��� �����ϰ� �ʹ�. -9.81 m/s
            //��. �� �߰�����
            // - ���� ���� �� �ִ� �׸��� ������ư�� ������
            //��. dir.y�� Y�ӵ��� �����ϰ� �ʹ�.

            //���� �����۶� ����ī��Ʈ�� �ִ�ī��Ʈ���� �۴ٸ� �� �� �ִ�.
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * yVelocity);
    }


}






