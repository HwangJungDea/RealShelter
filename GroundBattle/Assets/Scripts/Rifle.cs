using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Rifle : MonoBehaviour
{

    //ÅºÃ¢¼Ó¿¡ ÃÑ¾Ë °¹¼ö
    public int count;
    //ÃÖ´ë ÃÑ¾Ë °¹¼ö
    public int maxCount = 10;

    public Text textCount;
    public bool CanShoot()
    {
        return count > 0;
    }
    public void Shoot() //void¸¦ bool µÑ´Ù °¡´ÉÇÑ°ÅÀÓ. ÇÞ°¥¸®¸é ¿µ»óºÁ.
    {
        if (count > 0)
        {
            count--;
            textCount.text = count + "/" + maxCount;
           // return true;
        }
        
        //return false;
    }

    //ÅºÃ¢À» Ã¤¿ì°í ½Í´Ù.
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
