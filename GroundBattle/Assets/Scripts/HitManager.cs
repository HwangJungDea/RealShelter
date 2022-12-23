using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//이미지 히트를 번쩍 거리게 하고 싶다.
//언제?
//에너미가 플레이어를 공격할때.
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

    //번쩍이고 싶다.
    public delegate void Action();
    //일일이 만들기 귀찮으니 이걸 쓰시오
    //System.Action  //void 타입임 이걸 그대로 저 밑에 집어넣으면 됨.
    
    

    //delegate자료형을 만들고
    //Action callback;
    //그걸로 변수를 만든다.

    public void DoHit(int demage, Action callback)
    {
        StopCoroutine("IEHit");
        StartCoroutine("IEHit", callback);

        playerHP.HP-=demage;
        //public int damageAmount = a;
        //public void DoHit(int damageAmount)
        //playerHP.HP-=damageAmount;
        //공방 계산식 집어넣으면 재밌는 기능 구현완료~ㅅ~.
        
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
