using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    
    Vector3 mousePos; //마우스 위치가져와서 저장하기위한 변수
    Vector3 transPos;

    private bool Click = false;
    private List<GameObject> collEnemys=new List<GameObject>(); // 선택된 적들을 넣을 리스트

    private Color EnemyColor = Color.white; //  바뀌기전에 색을 저장

    private int MouseDamage = 10;

    private bool PushW = false;
    public GameObject Weapon1 = null; //PushWeapon을 할당

    private bool BoardW = false;
    private List<GameObject> BoardWeaponList = new List<GameObject>();

    public Image CoolDownImage = null;
    private float DelayMax = 1.0f;
    private float Delay = 0.0f;

    private bool TargetW = false;
    public GameObject Weapon2 = null; // TargetWeapon을 할당

    private bool ThunderW = false;
    public GameObject Weapon3 = null; // ThunderWeapon을 할당

    private bool ChargeW = false;
    public Image ChageImage = null;
    private float ChargeMax = 2.0f;
    private float ChargeDelay = 0.0f;
    public GameObject Weapon4 = null; // ChargeWeapon을 할당

    private bool SloowW = false;

    private bool BloodW = false;

    public GameObject Weapon5 = null; // ClickStateWeapon을 할당
    private bool ClickStateW = false;

    Player PlayerCheck;

    //현재 가지고 있는 무기들을 확인하고 자유롭게 true로 만들어줄수 있게 해줄 프로퍼티ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public bool CLICKSTATEW
    {
        get { return ClickStateW; }
        set { ClickStateW = value; }
    }
    public bool BLOODW
    {
        get { return BloodW; }
        set { BloodW = value; }
    }
    public bool SLOOWW
    {
        get { return SloowW; }
        set { SloowW = value; }
    }
    public bool CHARGEW
    {
        get { return ChargeW; }
        set { ChargeW = value; }
    }
    public bool PUSHW
    {
        get { return PushW; }
        set { PushW = value; }
    }

    public bool BOARDW
    {
        get { return BoardW; }
        set { BoardW = value; }
    }

    public bool TARGETW
    {
        get { return TargetW; }
        set { TargetW = value; }
    }

    public bool THUNDERW
    {
        get { return ThunderW; }
        set { ThunderW = value; }
    }
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    private void Start()
    {
        PlayerCheck = GameManager.INSTANCE.PLAYER.GetComponent<Player>();
    }
    private void Update()
    {

        //시간이 멈추지않고 플레이어가 살아있을때만 행동
        if (Time.timeScale != 0 && PlayerCheck.ISLIVE )
        {
            MouseMove();

            CoolDownImage.fillAmount = Delay / DelayMax; //쿨타임 표시
            ChageImage.fillAmount = ChargeDelay / ChargeMax; // 차지이미지 표시
            if (Delay < DelayMax) //마우스 공격 딜레이 증가
            {
                Delay += Time.deltaTime;
            }

            if (Click && ChargeW) // ChargeW이 true일때만 ChargeDelay를 증가시킨다.
            {
                ChargeDelay += Time.deltaTime;
            }

            if (Click && ClickStateW) // ClickStateW이 true일때만 공격이 활성화
            {
                Weapon5.GetComponent<ClickStateWeapon>().CLICKSTATE = true;
                Weapon5.GetComponent<ClickStateWeapon>().FireOn();
            }


            if (Input.GetMouseButtonDown(0) && Delay > DelayMax) // 딜레이가 DelayMax를 넘고 마우스로 클릭하면 클릭상태가 된다.
            {
                Click = true;


                //Debug.Log("클릭");

            }

            if (Input.GetMouseButtonUp(0) && Click) // 마우스를 때고 클릭상태일때 마우스르 땠을때 행동이 발동한다.
            {
                GameManager.INSTANCE.MAP.GetComponent<PerlinNoiseMap>().PlayerBackGround(); // 플레이어주변에만 배경이 그려지게한다.
                Click = false;
                Delay = 0.0f;

                //플레이어 이동과 공격 애니메이션작동
                GameManager.INSTANCE.PLAYER.transform.position = transform.position;
                Animator PlayerAnimator = GameManager.INSTANCE.PLAYER.GetComponent<Animator>();
                PlayerAnimator.SetTrigger("Attack");


                //무기를 집어넣는 순간 공격이되게 코루틴을 만듬
                StartCoroutine(MouseAttack());

                //무기들이 true일때 발동한다
                if (PushW)
                {
                    StartCoroutine(Weapon1.GetComponent<PushWeapon>().PushAttack());
                }
                if (TargetW)
                {
                    StartCoroutine(Weapon2.GetComponent<TargetWeapon>().TargetAttack());
                }
                if (ThunderW)
                {
                    StartCoroutine(Weapon3.GetComponent<ThunderWeapon>().ThunderAttack());
                }

                if (ChargeW)
                {
                    ChargeWeaponCode();
                    ChargeDelay = 0.0f;
                }

                if (ClickStateW)
                {
                    Weapon5.GetComponent<ClickStateWeapon>().CLICKSTATE = false;
                    Weapon5.GetComponent<ClickStateWeapon>().FireOff();
                }



                //Debug.Log("클릭땜");
            }
            if (BoardW)
            {
                BoardWeaponCode();
            }
        }
        
    }

    //차지가 되면 공격되게 하는 함수
    void ChargeWeaponCode()
    {
        
        if (ChargeDelay>ChargeMax)
        {
            //Debug.Log("차지완료");
            StartCoroutine(Weapon4.GetComponent<ChargeWeapon>().ChargeAttack());
        }
    }
    // 장판공격을 생성하고 클릭을 때면 활성화하게 해주는 함수
    void BoardWeaponCode()
    {
        if(Click && GameManager.INSTANCE.BOARDWEAPONQUEUE.Count>0)
        {
            GameObject bw = GameManager.INSTANCE.BOARDWEAPONQUEUE.Dequeue();
            BoardWeaponList.Add(bw);
            bw.SetActive(true);
            StartCoroutine(bw.GetComponent<boardWeapon>().BA());
            bw.transform.position = transform.position;

        }
        
        if(!Click && BoardWeaponList.Count>0)
        {
            foreach(var bw in BoardWeaponList)
            {
                if(bw!=null && bw.activeSelf)
                {
                    bw.GetComponent<SpriteRenderer>().color = Color.white;
                    bw.GetComponent<boardWeapon>().AttackStart();
                }
            }
            BoardWeaponList.Clear();
        }
    }

    //마우스에 선택된 적들만 공격하게 해주는 함수
    IEnumerator MouseAttack()
    {
        yield return new WaitForSeconds(1.5f);
        foreach (var enemy in collEnemys)
        {
            enemy.GetComponent<SpriteRenderer>().color = EnemyColor;
            enemy.GetComponent<Enemy>().Hit(MouseDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.NomalWeapon] += MouseDamage;
            enemy.GetComponent<Enemy>().CHOICE = false;
            if (SloowW)
            {
                enemy.GetComponent<Enemy>().SPEED = 1.0f;
            }
            if(BloodW)
            {
                enemy.GetComponent<Enemy>().BOOLDSTATE = true;
            }
            
        }
        collEnemys.Clear();
    }

    //마우스를 쫓아다니는 오브젝트
    void MouseMove()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position= new Vector3(transPos.x, transPos.y, 0);
    }

    //콜라이더에 닿으면 리스트에 넣는다

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && Click)
        {
            // 오브젝트를 중복선택 못하게하는 코드
            int number = collEnemys.FindIndex(x => x == collision.gameObject);
            if(number==-1)
            {
                collEnemys.Add(collision.gameObject);
                SpriteRenderer sp=collision.gameObject.GetComponent<SpriteRenderer>();
                EnemyColor=sp.color;
                sp.color=new Color(sp.color.r / 2, sp.color.g / 2, sp.color.b / 2);
                collision.gameObject.GetComponent<Enemy>().CHOICE = true;
                if(SloowW)
                {
                    collision.GetComponent<Enemy>().SPEED = 0.5f;
                }

            }
        }
    }
}
