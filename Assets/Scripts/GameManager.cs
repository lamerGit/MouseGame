using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private GameObject Player = null; //플레이어 미리 찾아놓기위한 변수
    private GameObject Mouse = null; // 마우스 미리 찾아놓기위한 변수

    public GameObject Enemy = null; 
    private Queue<GameObject> EnemyQueue= new Queue<GameObject>();

    private Canvas canvas; // 캔버스 찾아놓기 위한 변수
    public GameObject DamageText = null;
    private Queue<GameObject> DamageTextQueue= new Queue<GameObject>();

    public GameObject BoardWeapon = null;
    private Queue<GameObject> BoardWeaponQueue = new Queue<GameObject>();

    public GameObject TargetWeapon = null;
    private Queue<GameObject> TargetWeaponQueue = new Queue<GameObject>();

    public GameObject ChaseWeapon = null;
    private Queue<GameObject> ChaseWeaponQueue = new Queue<GameObject>();

    public GameObject ThunderWeapon = null;
    private Queue<GameObject> ThunderWeaponQueue=new Queue<GameObject>();

    public GameObject Exp = null;
    private Queue<GameObject> ExpQueue = new Queue<GameObject>();

    private GameObject[] RotateWeapon = new GameObject[2];

    private bool GamePause = false;

    private GameObject Map = null;
    public static GameManager INSTANCE
    {
        get
        {
            return instance;
        }
    }

    public GameObject MAP
    {
        get { return Map; }
    }

    public GameObject[] ROTATEWEAPON
    {
        get { return RotateWeapon; }
        set { RotateWeapon = value; }
    }

    public bool GAMEPAUSE
    {
        get { return GamePause; }
        set { GamePause = value; }

    }

    public GameObject PLAYER
    {
        get { return Player; }
    }

    public Queue<GameObject> ENEMYQUEUE
    {
        get { return EnemyQueue; }
    }

    public Queue<GameObject> DAMAGETEXTQUEUE
    {
        get { return DamageTextQueue; }
    }

    public Queue<GameObject> BOARDWEAPONQUEUE
    {
        get { return BoardWeaponQueue; }
    }

    public Queue<GameObject> TARGETWEAPONQUEUE
    {
        get { return TargetWeaponQueue; }
    }

    public Queue<GameObject> CHASEWEAPONQUEUE
    {
        get { return ChaseWeaponQueue; }
    }

    public Queue<GameObject> THUNDERWEAPONQUEUE
    {
        get { return ThunderWeaponQueue; }
    }

    public Queue<GameObject> EXPQUEUE
    {
        get { return ExpQueue; }
    }
    public Canvas CANBAS
    {
        get { return canvas; }
    }

    public GameObject MOUSE
    {
        get { return Mouse; }
    }

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if(instance!=this)
            {
                Destroy(gameObject);
            }
        }

        Player = GameObject.FindGameObjectWithTag("Player");
        canvas=GameObject.FindGameObjectWithTag("Canvas").gameObject.GetComponent<Canvas>();
        Mouse = GameObject.FindGameObjectWithTag("Mouse");
        RotateWeapon = GameObject.FindGameObjectsWithTag("RotateWeapon");
        Map = GameObject.FindGameObjectWithTag("Map");

        for(int i=0; i<1000; i++)
        {
            GameObject E=Instantiate(Enemy);
            E.SetActive(false);
            EnemyQueue.Enqueue(E);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject T = Instantiate(DamageText);
            T.SetActive(false);
            T.transform.SetParent(CANBAS.transform);
            DamageTextQueue.Enqueue(T);
        }

        for(int i=0; i<1000; i++)
        {
            GameObject B = Instantiate(BoardWeapon);
            B.SetActive(false);
            BoardWeaponQueue.Enqueue(B);
        }

        for (int i = 0; i < 100; i++)
        {
            GameObject TW = Instantiate(TargetWeapon);
            TW.SetActive(false);
            TargetWeaponQueue.Enqueue(TW);
        }

        for(int i=0; i<3; i++)
        {
            GameObject CW = Instantiate(ChaseWeapon);
            CW.SetActive(false);
            ChaseWeaponQueue.Enqueue(CW);
        }

        for(int i=0; i<10; i++)
        {
            GameObject THW = Instantiate(ThunderWeapon);
            THW.SetActive(false);
            ThunderWeaponQueue.Enqueue(THW);
        }

        for(int i=0; i<100; i++)
        {
            GameObject E = Instantiate(Exp);
            E.SetActive(false);
            ExpQueue.Enqueue(E);
        }

        
    }

    private void Update()
    {
        if(Time.timeScale==0)
        {
            //Debug.Log("게임 정지");
            
            if(Input.GetKeyDown(KeyCode.A))
            {
                Time.timeScale = 1.0f;
            }
        }
    }


}
