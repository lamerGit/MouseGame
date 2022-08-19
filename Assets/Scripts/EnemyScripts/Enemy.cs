using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //õõ�� �ٰ����� ���� ��ũ��Ʈ
    Vector3 moveVelocityX=Vector3.zero;
    Vector3 moveVelocityY=Vector3.zero;
    protected Vector3 PlayerPos=Vector3.zero;

    float[] RandLocal = new float[2] { 10f, -10f };

    protected int MaxHp = 10;
    protected int hp = 10;

    protected float HitDelayMax=0.5f;
    protected float HitDelay = 0.0f;

    private float Speed = 1.0f;

    protected bool BooldState = false;
    protected float BooldDelayMax = 1.0f;
    protected float BooldDelay = 0.0f;
    protected int BooldDamage = 1;

    SpriteRenderer sp = null;

    private bool Choice = false;

    protected Rigidbody2D rb2d;
    private void Awake()
    {
        sp=GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    

    public int MAXHP
    {
        get { return MaxHp; }
    }
    public virtual int HP 
    {
        get { return hp; }
        set { hp = value;

            if (hp < 1) //�׾����� �ൿ
            {
                hp = MaxHp;
                GameManager.INSTANCE.ENEMYQUEUE.Enqueue(gameObject); // ENEMYQUEUE�� �����ֱ�
                if (GameManager.INSTANCE.EXPQUEUE.Count > 0) // EXPQUEUE���� ����ġ ������Ʈ�� ��������
                {
                    GameObject ExpDrop = GameManager.INSTANCE.EXPQUEUE.Dequeue();
                    ExpDrop.transform.position = transform.position;
                    ExpDrop.SetActive(true);
                }
  
                gameObject.SetActive(false);
            }


        }
    }

    public bool CHOICE // ���콺�� ���É��� ���� Ȯ���� ������Ƽ
    {
        set { Choice = value; }
    }

    public float SPEED // ���ǵ� ������ ������Ƽ
    {
        set { Speed = value; }
        
        get { return Speed; } 
    }

    public bool BOOLDSTATE  // �������� Ȯ�ο�
    { set { BooldState = value; } }

    protected virtual void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            searchPlayer();

            if (HitDelay < HitDelayMax) //���� ���ݿ� �������� �����������ʵ��� �ð�����
            {
                HitDelay += Time.fixedDeltaTime;
            }
            if (BooldDelay < BooldDelayMax && BooldState) // ���������϶� �����ð����� �������� �޵��� �ð�����
            {
                BooldDelay += Time.fixedDeltaTime;
            }

            if (BooldDelay > BooldDelayMax && BooldState) //�ð��� �Ǹ� ���������� �ޱ�
            {
                Hit(BooldDamage);
                GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.BloodWeapon] += BooldDamage; // �ѵ��������� üũ�ϱ����� ���ӸŴ����� ������ ����
                BooldDelay = 0.0f;
            }

            
            tele();
        }
    }

    protected virtual void searchPlayer() //�÷��̾ �߰��ϴ� �Լ� Enemy2�� �ٸ�������� �÷��̾ �߰��ؾ��ϱ� ������ virtual�� ����
    {
        PlayerPos = GameManager.INSTANCE.PLAYER.transform.position;
        if (PlayerPos.x < transform.position.x)
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);//�÷��̾� �����ʿ� ������ ������ ó�ٺ�
            moveVelocityX = Vector3.left;
        }else if(PlayerPos.x-0.5f> transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);//�÷��̾� ���ʿ� ������ �������� ó�ٺ�
            moveVelocityX = Vector3.right;
        }

        if(PlayerPos.y<transform.position.y)
        {
            moveVelocityY = Vector3.down;
        }else if( PlayerPos.y> transform.position.y)
        {
            moveVelocityY=Vector3.up;
        }

        transform.position += moveVelocityX * Speed * Time.deltaTime;
        transform.position+=moveVelocityY*Speed* Time.deltaTime;
    }
    
    public void Hit(int damage) //�������� ��������� �ൿ
    {
        damage = damage + (int)(damage * (GameManager.INSTANCE.ATTACKLEVEL * 0.5f)); //damage+����ȭ�� ���׷���Ʈ ȭ�鿡�� ���׷��̵��� �����ϱ�
        if (GameManager.INSTANCE.DAMAGETEXTQUEUE.Count > 0 && gameObject.activeSelf) // Ȱ��ȭ����+�������� ǥ���� �ؽ�Ʈ�� ������ ����
        {
            StartCoroutine(HitAnime()); // �������� ������ ������ �ڷ�ƾ
            GameObject DamageText = GameManager.INSTANCE.DAMAGETEXTQUEUE.Dequeue(); // �������ؽ�Ʈ ������Ʈ ��������
            DamageText.SetActive(true); //�������ؽ�Ʈ Ȱ��ȭ
            
            //������ �ؽ�Ʈ�� ��ũ�� �������� �����Ǻ���
            float y = Random.Range(0f, 1.5f);
            DamageText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 1.0f, transform.position.y+y, transform.position.z));

            DamageText.GetComponent<Text>().text = damage.ToString();
            HP -= damage;

            //������ ����� ����� ����ٸ� ������ �����.
            if(GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BLOODW && !BooldState)
            {
                BooldState = true;
            }
        }
    }

    public void BoardHit(int damage) // ���ǵ������� ��������� �ൿ Hit�� ���������� HitDelay������ �������� �������� ���� �� ����.
    {
        damage = damage + (int)(damage * (GameManager.INSTANCE.ATTACKLEVEL * 0.5f));
        if (GameManager.INSTANCE.DAMAGETEXTQUEUE.Count > 0 && gameObject.activeSelf && HitDelay>HitDelayMax)
        {
            StartCoroutine(HitAnime());
            GameObject DamageText = GameManager.INSTANCE.DAMAGETEXTQUEUE.Dequeue();
            DamageText.SetActive(true);
            //StartCoroutine(DamageText.GetComponent<DamageText>().reTurn());
            float y = Random.Range(0f, 1.5f);
            DamageText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 1.0f, transform.position.y + y, transform.position.z));

            DamageText.GetComponent<Text>().text = damage.ToString();
            HitDelay = 0.0f;
            HP -= damage;
            if (GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BLOODW && !BooldState)
            {
                BooldState = true;
            }
        }
    }

    IEnumerator HitAnime() //���ݴ������� ������ �ڷ�ƾ�Լ�
    {
        //CurrentColor = sp.color;
        sp.color = new Color32(0, 0, 0, 255); // ���������� ����
        yield return new WaitForSeconds(0.2f);
        if (Choice)
        {
            sp.color = Color.gray; // ���콺�� ���õ� ���¿��� ���ݴ��ϸ� gray������
        }
        else
        {
            sp.color = new Color32(255, 255, 255, 255); // ���õ��� ���� ���¿��� ���ݴ��ϸ� ����������
        }
    }

    void tele() // �÷��̾�� �ʹ��ָ� ������ ������ ��ó�� �ڷ���Ʈ�ϴ� �Լ�
    {
        int x = Random.Range(0, 2);
        int y = Random.Range(0, 2);
        if(Mathf.Abs(GameManager.INSTANCE.PLAYER.transform.position.x-transform.position.x)>15 || (GameManager.INSTANCE.PLAYER.transform.position.y - transform.position.y)> 15)
        {
            transform.position = new Vector3(GameManager.INSTANCE.PLAYER.transform.position.x + RandLocal[x], GameManager.INSTANCE.PLAYER.transform.position.y + RandLocal[y], GameManager.INSTANCE.PLAYER.transform.position.z);
        }
                     
                
    }
}
