using System;//이놈에게도 랜덤이 있어서 밑에 랜덤이 저기서의 랜덤이라고 지정해줌.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random; //여기서의 랜덤은 이 랜덤이라고 지정해주는거.

//레벨관리하는 녀석.

//레벨을 관리하고 싶다.
//적의 최대 생성 수를 레벨 이하로 제한하고 싶다.
//적이 파괴될때 killCount를 증가시키다가 레벨이상이되면 레벨업 처리를 하고 싶다.
public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject bluelevelUpVFXFactoru;
    public GameObject purplelevelUpVFXFactoru;
    public Transform player;


    [HideInInspector]
    public int createCount; //만드는 입장
    [HideInInspector]
    public int killCount;   //파괴자 입장


    int level;
    public Text textLevel;

    //프로퍼티
    public int Level //함수인데 변수처럼 쓸수 있다. 레벨값이 변할때 UI도 같이 변하게 하고 싶다.
    {
        get { return level; }
        set
        {
            level = value;
            textLevel.text = "Lv " + level;
        }
    }

    internal void CheckLevelUp()
    {
        //어플리케이션이 종료되었다면 즉시 반환하고 싶다.
        if (false == Application.isPlaying)
        //if (gameObject == null)
        {
            return;
        }   //gameObject없으면 샤라랑.
        StartCoroutine("IELevelUp");

    }

    IEnumerator IELevelUp()
    {
        //만약 killCount가 NeedKillCount보다 높아 진다면~
        while (killCount >= NeedKillCount)
        {
            //레벨업하고 싶다.
            killCount -= NeedKillCount;
            createCount = 0;
            Level++;
            //TODO : 시각효과를 표현하고 싶다.
            //3항 연산자 => R = (조건 ? A : B);    //조건이 true면 A가 나오고 false면 B가 나온다.

            GameObject factory = Level % 2 == 0 ? purplelevelUpVFXFactoru : bluelevelUpVFXFactoru;

            //GameObject factory;
            //if (Level % 2 == 0)
            //{
            //    factory = purplelevelUpVFXFactoru;
            //}
            //else
            //{
            //    factory = bluelevelUpVFXFactoru;
            //}

            GameObject luvfx = Instantiate(factory);
            luvfx.transform.position = player.position + player.forward * 3f;
            luvfx.transform.parent = player; //null하면 최상위로 즉 부모자식관계를 끊어버린다.

            yield return new WaitForSeconds(0.2f);
        }
    }

    //최대 생성수
    public int MaxCreateCount
    {
        get { return level; } //*level; }
    }

    //레벨업에 필요한 killCount
    public int NeedKillCount
    {
        get { return level; }

    }


    // Start is called before the first frame update
    void Start()
    {
        Level = 1;

        //부모는 없고 자식에게만 있는 컴포넌트들 가져오게 한뒤 거기있는 트렌스폼정보(주목적)를 덤으로 가져오기.
        MeshRenderer[] rs = GetComponentsInChildren<MeshRenderer>();
        spawnList = new Transform[rs.Length];
        for (int i = 0; i < spawnList.Length; i++)
        {
            spawnList[i] = rs[i].transform;
        }
    }

    public enum SpawnType
    {
        //이해 필수.
        Normal,
        Area,
    }
    public SpawnType spawnType = SpawnType.Normal;

    private void Update()
    {
        switch (spawnType)
        {
            case SpawnType.Normal: UpdateNormal(); break;
            case SpawnType.Area: UpdateArea(); break;

        }
    }

    //일정 시간마다 적을 생서 하고 싶다.

    private void UpdateArea()
    {
        //1. 시간이 흐르다가
        currentTime += Time.deltaTime; //그냥 절대시간.

        //2. 만약 현재시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            //3. 현재 시간을 초기화 하고 싶다.
            currentTime = 0;
            //4. 생성범위내의 한 지점을 랜덤으로 설정하고 싶다.
            Vector3 pos = GetRandomPosition();

            //5. 그곳에 적을 생성하고 싶다.


            GameObject enemy = Instantiate(enemyFactory);

            //int index = Random.Range(0, spawnList.Length); //정수형 랜덤은 최소값 포함 최대값 미포함임.

            enemy.transform.position = pos;
        }
    }

    public Collider spawnAreaCube;
    private Vector3 GetRandomPosition()
    {
        Vector3 min = spawnAreaCube.bounds.min;
        Vector3 max = spawnAreaCube.bounds.max;

        float x = Random.Range(min.x, max.x);
        float y = spawnAreaCube.bounds.size.y;
        float z = Random.Range(min.z, max.z);

        //아래 방향으로 Ray를 쏴서 부딪힌것이 Floor라면

        Vector3 origin = new Vector3(x, y, z);
        Vector3 dir = Vector3.down;

        //성공할때 까지 반복하기.
        Ray ray = new Ray(origin, dir);// 위치체크, 방향체크
        RaycastHit hitInto;
        // int layerMask = ~(1 << LayerMask.NameToLayer("Floor"));
        //if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        //while (true) // 무한 반복 위험하다.
        for (int i = 0; i < 100; i++)
        {

            if (Physics.Raycast(ray, out hitInto))
            {
                //만약 부딪힌것이 Floor라면
                if (hitInto.transform.name.Contains("aaa"))
                //그 부딪힌 위치를 반환하고 싶다.
                { 
                    return hitInto.point;
                }
            }

        }
        return Vector3.zero;
    }




    //private Vector3 GetRandomPosition(Vector3 origin, float radius)
    //{
    //    return Vector3.zero;
    //}

    //private Vector3 GetRandomPosition(Vector3 origin, Bounds bounds)
    //{
    //    return Vector3.zero;
    //}

    //public Collider spawnAreaCube;



    //일정 시간마다 적을 생서 하고 싶다.
    //생성위치 목록중 하나의 위치에 랜덤하게 위치시키고 싶다.

    //- 현재시간
    private float currentTime;
    //- 생성시간
    public float createTime = 1;

    //- 적 공장
    public GameObject enemyFactory;
    //- 위치목록
    public Transform[] spawnList;

    //랜덤으로 스폰리스트 안에 적공장 위치시키기?

    void UpdateNormal()
    {
        //1. 시간이 흐르다가
        currentTime += Time.deltaTime; //그냥 절대시간.

        //2. 만약 현재시간이 생성시간을 초과하면
        if (currentTime > createTime)
        {
            //3. 현재 시간을 초기화 하고 싶다.
            currentTime = 0;
            //4. 적 공장에서 적을 생성하고
            GameObject enemy = Instantiate(enemyFactory);

            //5. 위치목록중 하나의 위치에 랜덤하게 위치시키고 싶다.
            int index = Random.Range(0, spawnList.Length); //정수형 랜덤은 최소값 포함 최대값 미포함임.
            Vector3 pos = spawnList[index].position; // 스폰리스트의 위치구하기.

            enemy.transform.position = pos;
        }

    }
    private void OnApplicationQuit()
    {
        // 강제종료한사이 보상주고 싶을때 쓰는넘.
        // 쓰기나름이다.
        instance = null;
    }
}
