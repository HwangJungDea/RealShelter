using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rifle : MonoBehaviour
{

    //źâ�ӿ� �Ѿ� ����
    public int count;
    //�ִ� �Ѿ� ����
    public int maxCount = 10;

    public Text textCount;
    public bool CanShoot()
    {
        return count > 0;
    }
    public void Shoot() //void�� bool �Ѵ� �����Ѱ���. �ް����� �����.
    {
        if (count > 0)
        {
            count--;
            textCount.text = count + "/" + maxCount;
           // return true;
        }
        
        //return false;
    }

    //źâ�� ä��� �ʹ�.
    public void Reload()
    {
        count = maxCount;
        textCount.text = count + "/" + maxCount;

    }
    void Start()
    {
        Reload();
    }


    void Update()
    {
        
    }
}
