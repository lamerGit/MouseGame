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
    
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            MouseMove();

            CoolDownImage.fillAmount = Delay / DelayMax;
            ChageImage.fillAmount = ChargeDelay / ChargeMax;
            if (Delay < DelayMax)
            {
                Delay += Time.deltaTime;
            }

            if (Click && ChargeW)
            {
                ChargeDelay += Time.deltaTime;
            }

            if (Click && ClickStateW)
            {
                Weapon5.GetComponent<ClickStateWeapon>().CLICKSTATE = true;
                Weapon5.GetComponent<ClickStateWeapon>().FireOn();
            }


            if (Input.GetMouseButtonDown(0) && Delay > DelayMax)
            {
                Click = true;


                //Debug.Log("클릭");

            }

            if (Input.GetMouseButtonUp(0) && Click)
            {
                GameManager.INSTANCE.MAP.GetComponent<PerlinNoiseMap>().PlayerBackGround();
                Click = false;
                Delay = 0.0f;

                GameManager.INSTANCE.PLAYER.transform.position = transform.position;
                Animator PlayerAnimator = GameManager.INSTANCE.PLAYER.GetComponent<Animator>();
                PlayerAnimator.SetTrigger("Attack");

                StartCoroutine(MouseAttack());
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

    void ChargeWeaponCode()
    {
        
        if (ChargeDelay>ChargeMax)
        {
            //Debug.Log("차지완료");
            StartCoroutine(Weapon4.GetComponent<ChargeWeapon>().ChargeAttack());
        }
    }

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
    void MouseMove()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position= new Vector3(transPos.x, transPos.y, 0);
    }

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
