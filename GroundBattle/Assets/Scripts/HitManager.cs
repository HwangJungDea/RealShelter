using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�̹��� ��Ʈ�� ��½ �Ÿ��� �ϰ� �ʹ�.
//����?
//���ʹ̰� �÷��̾ �����Ҷ�.
public class HitManager : MonoBehaviour
{

    public static HitManager instance;
    private void Awake()
    {
        instance = this;    
    }


    public GameObject imageHit;
    public PlayerHP playerHP;
    // Start is called before the first frame update
    void Start()
    {
        imageHit.SetActive(false);

    }

    //��½�̰� �ʹ�.
    public delegate void Action();
    //������ ����� �������� �̰� ���ÿ�
    //System.Action  //void Ÿ���� �̰� �״�� �� �ؿ� ��������� ��.
    
    

    //delegate�ڷ����� �����
    //Action callback;
    //�װɷ� ������ �����.

    public void DoHit(int demage, Action callback)
    {
        StopCoroutine("IEHit");
        StartCoroutine("IEHit", callback);

        playerHP.HP-=demage;
        //public int damageAmount = a;
        //public void DoHit(int damageAmount)
        //playerHP.HP-=damageAmount;
        //���� ���� ��������� ��մ� ��� �����Ϸ�~��~.
        
        if(playerHP.HP <= 0)
        {
            GameManager.instance.gameOverUI.SetActive(true);
        }

    }
    IEnumerator IEHit(Action callback)
    {
        imageHit.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        imageHit.SetActive(false);
        callback();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
