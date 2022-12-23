using System;//�̳𿡰Ե� ������ �־ �ؿ� ������ ���⼭�� �����̶�� ��������.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Random = UnityEngine.Random; //���⼭�� ������ �� �����̶�� �������ִ°�.

//���������ϴ� �༮.

//������ �����ϰ� �ʹ�.
//���� �ִ� ���� ���� ���� ���Ϸ� �����ϰ� �ʹ�.
//���� �ı��ɶ� killCount�� ������Ű�ٰ� �����̻��̵Ǹ� ������ ó���� �ϰ� �ʹ�.
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
    public int createCount; //����� ����
    [HideInInspector]
    public int killCount;   //�ı��� ����


    int level;
    public Text textLevel;

    //������Ƽ
    public int Level //�Լ��ε� ����ó�� ���� �ִ�. �������� ���Ҷ� UI�� ���� ���ϰ� �ϰ� �ʹ�.
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
        //���ø����̼��� ����Ǿ��ٸ� ��� ��ȯ�ϰ� �ʹ�.
        if (false == Application.isPlaying)
        //if (gameObject == null)
        {
            return;
        }   //gameObject������ �����.
        StartCoroutine("IELevelUp");

    }

    IEnumerator IELevelUp()
    {
        //���� killCount�� NeedKillCount���� ���� ���ٸ�~
        while (killCount >= NeedKillCount)
        {
            //�������ϰ� �ʹ�.
            killCount -= NeedKillCount;
            createCount = 0;
            Level++;
            //TODO : �ð�ȿ���� ǥ���ϰ� �ʹ�.
            //3�� ������ => R = (���� ? A : B);    //������ true�� A�� ������ false�� B�� ���´�.

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
            luvfx.transform.parent = player; //null�ϸ� �ֻ����� �� �θ��ڽİ��踦 ���������.

            yield return new WaitForSeconds(0.2f);
        }
    }

    //�ִ� ������
    public int MaxCreateCount
    {
        get { return level; } //*level; }
    }

    //�������� �ʿ��� killCount
    public int NeedKillCount
    {
        get { return level; }

    }


    // Start is called before the first frame update
    void Start()
    {
        Level = 1;

        //�θ�� ���� �ڽĿ��Ը� �ִ� ������Ʈ�� �������� �ѵ� �ű��ִ� Ʈ����������(�ָ���)�� ������ ��������.
        MeshRenderer[] rs = GetComponentsInChildren<MeshRenderer>();
        spawnList = new Transform[rs.Length];
        for (int i = 0; i < spawnList.Length; i++)
        {
            spawnList[i] = rs[i].transform;
        }
    }

    public enum SpawnType
    {
        //���� �ʼ�.
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

    //���� �ð����� ���� ���� �ϰ� �ʹ�.

    private void UpdateArea()
    {
        //1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime; //�׳� ����ð�.

        //2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            //3. ���� �ð��� �ʱ�ȭ �ϰ� �ʹ�.
            currentTime = 0;
            //4. ������������ �� ������ �������� �����ϰ� �ʹ�.
            Vector3 pos = GetRandomPosition();

            //5. �װ��� ���� �����ϰ� �ʹ�.


            GameObject enemy = Instantiate(enemyFactory);

            //int index = Random.Range(0, spawnList.Length); //������ ������ �ּҰ� ���� �ִ밪 ��������.

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

        //�Ʒ� �������� Ray�� ���� �ε������� Floor���

        Vector3 origin = new Vector3(x, y, z);
        Vector3 dir = Vector3.down;

        //�����Ҷ� ���� �ݺ��ϱ�.
        Ray ray = new Ray(origin, dir);// ��ġüũ, ����üũ
        RaycastHit hitInto;
        // int layerMask = ~(1 << LayerMask.NameToLayer("Floor"));
        //if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layerMask))
        //while (true) // ���� �ݺ� �����ϴ�.
        for (int i = 0; i < 100; i++)
        {

            if (Physics.Raycast(ray, out hitInto))
            {
                //���� �ε������� Floor���
                if (hitInto.transform.name.Contains("aaa"))
                //�� �ε��� ��ġ�� ��ȯ�ϰ� �ʹ�.
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



    //���� �ð����� ���� ���� �ϰ� �ʹ�.
    //������ġ ����� �ϳ��� ��ġ�� �����ϰ� ��ġ��Ű�� �ʹ�.

    //- ����ð�
    private float currentTime;
    //- �����ð�
    public float createTime = 1;

    //- �� ����
    public GameObject enemyFactory;
    //- ��ġ���
    public Transform[] spawnList;

    //�������� ��������Ʈ �ȿ� ������ ��ġ��Ű��?

    void UpdateNormal()
    {
        //1. �ð��� �帣�ٰ�
        currentTime += Time.deltaTime; //�׳� ����ð�.

        //2. ���� ����ð��� �����ð��� �ʰ��ϸ�
        if (currentTime > createTime)
        {
            //3. ���� �ð��� �ʱ�ȭ �ϰ� �ʹ�.
            currentTime = 0;
            //4. �� ���忡�� ���� �����ϰ�
            GameObject enemy = Instantiate(enemyFactory);

            //5. ��ġ����� �ϳ��� ��ġ�� �����ϰ� ��ġ��Ű�� �ʹ�.
            int index = Random.Range(0, spawnList.Length); //������ ������ �ּҰ� ���� �ִ밪 ��������.
            Vector3 pos = spawnList[index].position; // ��������Ʈ�� ��ġ���ϱ�.

            enemy.transform.position = pos;
        }

    }
    private void OnApplicationQuit()
    {
        // ���������ѻ��� �����ְ� ������ ���³�.
        // ���⳪���̴�.
        instance = null;
    }
}
