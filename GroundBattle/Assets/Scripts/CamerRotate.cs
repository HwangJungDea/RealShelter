using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotate : MonoBehaviour
{
    //마우스를 움직여서 카메라를 회전하고 싶다.
    // Start is called before the first frame update
    void Start()
    {

    }
    float rx;
    float ry;
    public float rotSpeed = 200f;
    

    // Update is called once per frame
    void Update()
    {
        //마우스를 움직여서 (변화량)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //rx의 각도를 제한하고 싶다.
        rx = Mathf.Clamp(rx, -75, 75);
        //transform.Rotate(-my, mx, 0); 오류가 많으니 버려버려
        
            ry += mx * rotSpeed * Time.deltaTime; //마우스 좌/우 이동으로 카메라 y축 회전
            rx -= my * rotSpeed * Time.deltaTime; // 마우스 위/아래 이동으로 카메라 x축 회전


        //변화량을 누적해서 모두 더하면 결국 회전값이된다.
        

        


        //카메라를 회전하고 싶다.



        transform.eulerAngles = new Vector3(rx, ry, 0); 

    }




    // Mathf.Clamp(float value, float min, float max) 이게 밑에 꺼랑 같은 의미라는거.
    //float Clamp(float value, float min, float max)
    //{
    //    //만약에 value가 min보다 작다면 min을 반환하고 싶다
    //    if (value < min)
    //    {
    //        return min;
    //    }
    //    //만약에 value가 max보다 크다면 max를 반환하고 싶다.
    //    else if(value > max)
    //    {
    //        return max;
    //    }
    //    //이도 저도 아니라면 value를 반환하고 싶다.
    //    else
    //    {
    //        return value;
    //    }
}
