using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;



//�¾�� GameOverUI�� ������ �ʰ� �ϰ� �ʹ�.
//�÷��̾ �׾�����(ü���� 0�϶�) GameOverUI�� ���̰� �ϰ� �ʹ�.
//����/ ����� ����� ����� �ʹ�.

//�÷��̾ �ı��ϸ� ī�޶� �������.
//�÷��̾��� ������ ������Ű��.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
    }
    //�ƴ¸�ŭ ���̴°ű���.����

    public GameObject gameOverUI;
    public Button buttonQuit;
    public Button buttonRestart;



    void Start()
    {
        buttonQuit.onClick.AddListener(OnclickQuit);
        buttonRestart.onClick.AddListener(OnClickRestart);
        //Listener �ڹٰ迭���� ���¿��
        //
        //On��¼����¼�� ���ξ On�̶�°� ��Լ��� ȣ���Ѵٴ� �����̴�.
        //�̰� ����Ƽ��׶��� �ٿ�
        //
        //��������Ʈ?delegate?��?
        //�����ε� �Լ�ó�� ����Ҽ��ִ� ����.
        //������Ƽ�ʹ� �ݴ��� ����.
        //HitManager�� ���ÿ�

        //�¾�� ������ �ʰ� �ϰ� �ʹ�.
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame

    public void OnclickQuit()
    {
        Application.Quit();
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);//�ڱ� �� �ҷ�����.
    }



    void Update()
    {
        
    }
}
