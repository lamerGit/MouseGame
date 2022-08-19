using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mouse : MonoBehaviour
{
    
    Vector3 mousePos; //���콺 ��ġ�����ͼ� �����ϱ����� ����
    Vector3 transPos;

    private bool Click = false;
    private List<GameObject> collEnemys=new List<GameObject>(); // ���õ� ������ ���� ����Ʈ

    private Color EnemyColor = Color.white; //  �ٲ������ ���� ����

    private int MouseDamage = 10;

    private bool PushW = false;
    public GameObject Weapon1 = null; //PushWeapon�� �Ҵ�

    private bool BoardW = false;
    private List<GameObject> BoardWeaponList = new List<GameObject>();

    public Image CoolDownImage = null;
    private float DelayMax = 1.0f;
    private float Delay = 0.0f;

    private bool TargetW = false;
    public GameObject Weapon2 = null; // TargetWeapon�� �Ҵ�

    private bool ThunderW = false;
    public GameObject Weapon3 = null; // ThunderWeapon�� �Ҵ�

    private bool ChargeW = false;
    public Image ChageImage = null;
    private float ChargeMax = 2.0f;
    private float ChargeDelay = 0.0f;
    public GameObject Weapon4 = null; // ChargeWeapon�� �Ҵ�

    private bool SloowW = false;

    private bool BloodW = false;

    public GameObject Weapon5 = null; // ClickStateWeapon�� �Ҵ�
    private bool ClickStateW = false;

    Player PlayerCheck;

    //���� ������ �ִ� ������� Ȯ���ϰ� �����Ӱ� true�� ������ټ� �ְ� ���� ������Ƽ�ѤѤѤѤѤѤѤѤѤѤѤ�
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
    //�ѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤѤ�
    private void Start()
    {
        PlayerCheck = GameManager.INSTANCE.PLAYER.GetComponent<Player>();
    }
    private void Update()
    {

        //�ð��� �������ʰ� �÷��̾ ����������� �ൿ
        if (Time.timeScale != 0 && PlayerCheck.ISLIVE )
        {
            MouseMove();

            CoolDownImage.fillAmount = Delay / DelayMax; //��Ÿ�� ǥ��
            ChageImage.fillAmount = ChargeDelay / ChargeMax; // �����̹��� ǥ��
            if (Delay < DelayMax) //���콺 ���� ������ ����
            {
                Delay += Time.deltaTime;
            }

            if (Click && ChargeW) // ChargeW�� true�϶��� ChargeDelay�� ������Ų��.
            {
                ChargeDelay += Time.deltaTime;
            }

            if (Click && ClickStateW) // ClickStateW�� true�϶��� ������ Ȱ��ȭ
            {
                Weapon5.GetComponent<ClickStateWeapon>().CLICKSTATE = true;
                Weapon5.GetComponent<ClickStateWeapon>().FireOn();
            }


            if (Input.GetMouseButtonDown(0) && Delay > DelayMax) // �����̰� DelayMax�� �Ѱ� ���콺�� Ŭ���ϸ� Ŭ�����°� �ȴ�.
            {
                Click = true;


                //Debug.Log("Ŭ��");

            }

            if (Input.GetMouseButtonUp(0) && Click) // ���콺�� ���� Ŭ�������϶� ���콺�� ������ �ൿ�� �ߵ��Ѵ�.
            {
                GameManager.INSTANCE.MAP.GetComponent<PerlinNoiseMap>().PlayerBackGround(); // �÷��̾��ֺ����� ����� �׷������Ѵ�.
                Click = false;
                Delay = 0.0f;

                //�÷��̾� �̵��� ���� �ִϸ��̼��۵�
                GameManager.INSTANCE.PLAYER.transform.position = transform.position;
                Animator PlayerAnimator = GameManager.INSTANCE.PLAYER.GetComponent<Animator>();
                PlayerAnimator.SetTrigger("Attack");


                //���⸦ ����ִ� ���� �����̵ǰ� �ڷ�ƾ�� ����
                StartCoroutine(MouseAttack());

                //������� true�϶� �ߵ��Ѵ�
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



                //Debug.Log("Ŭ����");
            }
            if (BoardW)
            {
                BoardWeaponCode();
            }
        }
        
    }

    //������ �Ǹ� ���ݵǰ� �ϴ� �Լ�
    void ChargeWeaponCode()
    {
        
        if (ChargeDelay>ChargeMax)
        {
            //Debug.Log("�����Ϸ�");
            StartCoroutine(Weapon4.GetComponent<ChargeWeapon>().ChargeAttack());
        }
    }
    // ���ǰ����� �����ϰ� Ŭ���� ���� Ȱ��ȭ�ϰ� ���ִ� �Լ�
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

    //���콺�� ���õ� ���鸸 �����ϰ� ���ִ� �Լ�
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

    //���콺�� �Ѿƴٴϴ� ������Ʈ
    void MouseMove()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position= new Vector3(transPos.x, transPos.y, 0);
    }

    //�ݶ��̴��� ������ ����Ʈ�� �ִ´�

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && Click)
        {
            // ������Ʈ�� �ߺ����� ���ϰ��ϴ� �ڵ�
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
