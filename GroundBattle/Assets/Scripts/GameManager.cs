using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;



//태어날때 GameOverUI를 보이지 않게 하고 싶다.
//플레이어가 죽었을때(체력이 0일때) GameOverUI를 보이게 하고 싶다.
//종료/ 재시작 기능을 만들고 싶다.

//플레이어를 파괴하면 카메라가 사라진다.
//플레이어의 동작을 정지시키자.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    //아는만큼 보이는거구먼.ㅋㅋ

    public GameObject gameOverUI;
    public Button buttonQuit;
    public Button buttonRestart;



    void Start()
    {
        buttonQuit.onClick.AddListener(OnclickQuit);
        buttonRestart.onClick.AddListener(OnClickRestart);
        //Listener 자바계열에서 쓰는용어
        //
        //On어쩌구저쩌구 접두어가 On이라는건 어떤함수를 호출한다는 형태이다.
        //이걸 유니티백그라운드 다운
        //
        //델리게이트?delegate?ㅅ?
        //변수인데 함수처럼 사용할수있는 변수.
        //프로퍼티와는 반대의 개념.
        //HitManager로 가시오

        //태어날때 보이지 않게 하고 싶다.
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame

    public void OnclickQuit()
    {
        Application.Quit();
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//자기 씬 불러오기.
    }



    void Update()
    {
        
    }
}
