using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// ���� ü���� ������ UI�� ó���ϰ� �ʹ�.
// �¾ �� �ַ��� �ִ� ü������
// Player�� Enemy���� �������� ������ ü���� 1 ���ҽ�Ű�� �ʹ�.
//ü���� 0���ϰ� �Ǹ� Enemy�� �ı��ϰ� �ʹ�.
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
            // curHP�� value�� �����ϵ� ���� ���� ������ �ϰ� �ʹ�.
            curHP = Mathf.Clamp(value, 0, maxHP); //�����������ֱ�. 0���� maxHP����
            sliderHP.value = curHP;
        }
    }
    void Start()
    {
        //�¾ �� ü���� �ִ�ü������ �ϰ�ʹ�.
        sliderHP.value = maxHP;
        HP = maxHP;

    }
}
