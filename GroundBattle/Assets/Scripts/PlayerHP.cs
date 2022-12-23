using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// �÷��̾��� ü���� ������ UI�� ó���ϰ� �ʹ�.
// �¾ �� �ַ��� �ִ� ü������
// Enemy�� Player�� Hit�ϸ� ü���� 1 ���ҽ�Ű�� �ʹ�.
//ü���� 0���ϰ� �Ǹ� ���ӿ��� ó���ϰ� �ʹ�.


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
        //�¾ �� ü���� �ִ�ü������ �ϰ�ʹ�.
        sliderHP.value = maxHP;
        HP = maxHP;
        
    }

    void Update()
    {

    }




}
