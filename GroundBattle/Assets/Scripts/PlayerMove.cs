using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//사용자의 입력에 따라 앞뒤좌우로 이동하고 싶다.
//점프를 뛰고 싶다. -> 중력, 점프뛰는힘, Y속도

//왼쪽 시프트 키를 누르는 중에 달리기를 하고 싶다.
public class PlayerMove : MonoBehaviour
{

    public float speed = 5f;
    public float runSpeed = 10f;
    public float gravity = -9.81f; //중력
    public float jumpPower = 10f; // 점프뛰는힘
    float yVelocity; //Y속도
    CharacterController cc;


    void Start()
    {
        cc = gameObject.GetComponent<CharacterController>();//자기꺼 쓸때는 gameObject생략가능.
    }


    public int maxJumpCount = 2;
    int jumpCount = 0;
    void Update()
    {
        //만약 땅에 닿았다면 점프카운트를 0으로 초기화 하고 싶다.
        //cc.collisionFlags == CollisionFlags.Sides//켰나 꺼졌나.
        //if ((cc.collisionFlags & CollisionFlags.Above) != 0)
        //{
        //        if (true == cc.isGrounded) 같은소리
        //}


        if (true == cc.isGrounded)
        {
            jumpCount = 0; //점프카운트 초기화
            yVelocity = 0; //중력 초기화

        }
        if (jumpCount < maxJumpCount && Input.GetButtonDown("Jump"))
        {
            //나. Y속도에 JumpPower를 대입하고 싶다.
            yVelocity = jumpPower;
            jumpCount++;
        }

        {
            float finalSpeed = speed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                finalSpeed = runSpeed;
            }

            // 1. 사용자의 입력에따라 
            // 2.앞뒤좌우로 방향을 만들고
            // 3. 그 방향으로 이동하고 싶다.
            float h = Input.GetAxisRaw("Horizontal"); //GetAxis는 float로 반환
            float v = Input.GetAxisRaw("Vertical");
            //플레이어 알뒤
            //플레이어 좌우
            //정보 넣고 거기에 스피드 곱하기.

            Vector3 dir = Vector3.right * h + Vector3.forward * v;
            //new Vector3(h,0,v);


            //여기
            //if (Input.GetButton("Fire2"))
            //{
            //    dir = transform.TransformDirection(dir);
            //}
            //else
            {
                dir = Camera.main.transform.TransformDirection(dir);

                dir.y = 0;
                dir.Normalize(); //벡터의 크기를 1로 줄인다. 대각선 속도일정.
                                 //Camera를 기준축으로 방향을 바꾸겠다.
                                 //dir.y = 0; //y축은 고정. 스페이스바 안눌렀을때는 0이다.
            }
            //다
            Vector3 velocity = dir * finalSpeed;

            velocity.y = yVelocity;  //다. dir.y에 Y속도를 대입하고 싶다.

            cc.Move(velocity * Time.deltaTime);
            //transform.position += (dir * speed) * Time.deltaTime;
        }

        {
            yVelocity += gravity * Time.deltaTime;//1초에 30번을 그리면 1/30을 곱한다.
                                                  //가속도 공식을 만든거닷.           //gravity/30을 30번 더한다는 소리
                                                  //즉 1초동안의 gravity적용값이 yVelocity에 들어간다.


            //가. Y속도에 중력을 대입하고 싶다. -9.81 m/s
            //나. 의 추가사항
            // - 만약 땅에 서 있다 그리고 점프버튼이 눌리면
            //다. dir.y에 Y속도를 대입하고 싶다.

            //만약 점프뛸때 현재카운트가 최대카운트보다 작다면 뛸 수 있다.
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * yVelocity);
    }


}






