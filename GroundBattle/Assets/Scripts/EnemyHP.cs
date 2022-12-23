using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// 적의 체력의 변동과 UI를 처리하고 싶다.
// 태어날 때 최력을 최대 체력으로
// Player가 Enemy에게 데미지를 입히면 체력을 1 감소시키고 싶다.
//체력이 0이하가 되면 Enemy를 파괴하고 싶다.
public class EnemyHP : MonoBehaviour
{
    public int maxHP = 2;
    int curHP;
    public Slider sliderHP;

    public int HP
    {
        get { return curHP; }
        set
        {
            //curHP = value;
            //if (curHP < 0)
            //{
            //    curHP = 0;
            //}
            // curHP에 value를 대입하되 범위 안의 값으로 하고 싶다.
            curHP = Mathf.Clamp(value, 0, maxHP); //범위지정해주기. 0에서 maxHP사이
            sliderHP.value = curHP;
        }
    }
    void Start()
    {
        //태어날 떄 체력을 최대체력으로 하고싶다.
        sliderHP.value = maxHP;
        HP = maxHP;

    }
}
