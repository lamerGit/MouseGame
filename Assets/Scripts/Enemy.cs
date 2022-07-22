using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    Vector3 moveVelocityX=Vector3.zero;
    Vector3 moveVelocityY=Vector3.zero;
    Vector3 PlayerPos=Vector3.zero;

    float[] RandLocal = new float[2] { 10f, -10f };

    private int MaxHp = 10;
    private int hp = 10;

    private float HitDelayMax=0.5f;
    private float HitDelay = 0.0f;

    private float Speed = 1.0f;

    private bool BooldState = false;
    private float BooldDelayMax = 1.0f;
    private float BooldDelay = 0.0f;
    private int BooldDamage = 1;

    SpriteRenderer sp = null;

    private bool Choice = false;
    private void Awake()
    {
        sp=GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        hp = 10;
    }

    private int HP
    {
        get { return hp; }
        set { hp = value; }
    }

    public bool CHOICE
    {
        set { Choice = value; }
    }

    public float SPEED
    {
        set { Speed = value; }
        
        get { return Speed; } 
    }

    public bool BOOLDSTATE
    { set { BooldState = value; } }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            searchPlayer();
            if (HitDelay < HitDelayMax)
            {
                HitDelay += Time.fixedDeltaTime;
            }
            if (BooldDelay < BooldDelayMax && BooldState)
            {
                BooldDelay += Time.fixedDeltaTime;
            }

            if (BooldDelay > BooldDelayMax && BooldState)
            {
                Hit(BooldDamage);
                GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.BloodWeapon] += BooldDamage;
                BooldDelay = 0.0f;
            }

            if (hp < 1)
            {
                hp = MaxHp;
                GameManager.INSTANCE.ENEMYQUEUE.Enqueue(gameObject);
                if (GameManager.INSTANCE.EXPQUEUE.Count > 0)
                {
                    GameObject ExpDrop = GameManager.INSTANCE.EXPQUEUE.Dequeue();
                    ExpDrop.transform.position = transform.position;
                    ExpDrop.SetActive(true);
                }
                //BooldState = false;
                //Choice = false;
                //sp.color = Color.red;
                gameObject.SetActive(false);
            }
            tele();
        }
    }

    void searchPlayer()
    {
        PlayerPos = GameManager.INSTANCE.PLAYER.transform.position;
        if (PlayerPos.x < transform.position.x)
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);
            moveVelocityX = Vector3.left;
        }else if(PlayerPos.x-0.5f> transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);
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
    
    public void Hit(int damage)
    {
        damage = damage + (int)(damage * (GameManager.INSTANCE.ATTACKLEVEL * 0.5f));
        if (GameManager.INSTANCE.DAMAGETEXTQUEUE.Count > 0 && gameObject.activeSelf)
        {
            StartCoroutine(HitAnime());
            GameObject DamageText = GameManager.INSTANCE.DAMAGETEXTQUEUE.Dequeue();
            DamageText.SetActive(true);
            //StartCoroutine(DamageText.GetComponent<DamageText>().reTurn());
            float y = Random.Range(0f, 1.5f);
            DamageText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 1.0f, transform.position.y+y, transform.position.z));

            DamageText.GetComponent<Text>().text = damage.ToString();
            hp -= damage;
            if(GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BLOODW && !BooldState)
            {
                BooldState = true;
            }
        }
    }

    public void BoardHit(int damage)
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
            hp -= damage;
            if (GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BLOODW && !BooldState)
            {
                BooldState = true;
            }
        }
    }

    IEnumerator HitAnime()
    {
        //CurrentColor = sp.color;
        sp.color = new Color32(0, 0, 0, 255);
        yield return new WaitForSeconds(0.2f);
        if (Choice)
        {
            sp.color = Color.gray;
        }
        else
        {
            sp.color = new Color32(255, 255, 255, 255);
        }
    }

    void tele()
    {
        int x = Random.Range(0, 2);
        int y = Random.Range(0, 2);
        if(Mathf.Abs(GameManager.INSTANCE.PLAYER.transform.position.x-transform.position.x)>15 || (GameManager.INSTANCE.PLAYER.transform.position.y - transform.position.y)> 15)
        {
            transform.position = new Vector3(GameManager.INSTANCE.PLAYER.transform.position.x + RandLocal[x], GameManager.INSTANCE.PLAYER.transform.position.y + RandLocal[y], GameManager.INSTANCE.PLAYER.transform.position.z);
        }
                     
                
    }
}
