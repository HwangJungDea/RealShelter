using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerRotate : MonoBehaviour
{
    //���콺�� �������� ī�޶� ȸ���ϰ� �ʹ�.
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
        //���콺�� �������� (��ȭ��)
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //rx�� ������ �����ϰ� �ʹ�.
        rx = Mathf.Clamp(rx, -75, 75);
        //transform.Rotate(-my, mx, 0); ������ ������ ��������
        
            ry += mx * rotSpeed * Time.deltaTime; //���콺 ��/�� �̵����� ī�޶� y�� ȸ��
            rx -= my * rotSpeed * Time.deltaTime; // ���콺 ��/�Ʒ� �̵����� ī�޶� x�� ȸ��


        //��ȭ���� �����ؼ� ��� ���ϸ� �ᱹ ȸ�����̵ȴ�.
        

        


        //ī�޶� ȸ���ϰ� �ʹ�.



        transform.eulerAngles = new Vector3(rx, ry, 0); 

    }




    // Mathf.Clamp(float value, float min, float max) �̰� �ؿ� ���� ���� �ǹ̶�°�.
    //float Clamp(float value, float min, float max)
    //{
    //    //���࿡ value�� min���� �۴ٸ� min�� ��ȯ�ϰ� �ʹ�
    //    if (value < min)
    //    {
    //        return min;
    //    }
    //    //���࿡ value�� max���� ũ�ٸ� max�� ��ȯ�ϰ� �ʹ�.
    //    else if(value > max)
    //    {
    //        return max;
    //    }
    //    //�̵� ���� �ƴ϶�� value�� ��ȯ�ϰ� �ʹ�.
    //    else
    //    {
    //        return value;
    //    }
}
