using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 플레이어의 체력의 변동과 UI를 처리하고 싶다.
// 태어날 때 최력을 최대 체력으로
// Enemy가 Player를 Hit하면 체력을 1 감소시키고 싶다.
//체력이 0이하가 되면 게임오버 처리하고 싶다.


public class PlayerHP : MonoBehaviour
{
    int maxHP = 10;
    int curHP;
    public Slider sliderHP;

    public int HP
    {
        get { return curHP; }
        set
        {
            curHP = value;
            sliderHP.value = curHP;
        }
    }
    void Start()
    {
        //태어날 떄 체력을 최대체력으로 하고싶다.
        sliderHP.value = maxHP;
        HP = maxHP;
        
    }

    void Update()
    {

    }




}
