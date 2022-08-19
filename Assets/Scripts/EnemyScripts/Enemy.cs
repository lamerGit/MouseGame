using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    //천천히 다가오는 적의 스크립트
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

            if (hp < 1) //죽었을때 행동
            {
                hp = MaxHp;
                GameManager.INSTANCE.ENEMYQUEUE.Enqueue(gameObject); // ENEMYQUEUE에 돌려주기
                if (GameManager.INSTANCE.EXPQUEUE.Count > 0) // EXPQUEUE에서 경험치 오브젝트를 가져오기
                {
                    GameObject ExpDrop = GameManager.INSTANCE.EXPQUEUE.Dequeue();
                    ExpDrop.transform.position = transform.position;
                    ExpDrop.SetActive(true);
                }
  
                gameObject.SetActive(false);
            }


        }
    }

    public bool CHOICE // 마우스로 선택됬을 때를 확인할 프로퍼티
    {
        set { Choice = value; }
    }

    public float SPEED // 스피드 조절용 프로퍼티
    {
        set { Speed = value; }
        
        get { return Speed; } 
    }

    public bool BOOLDSTATE  // 출혈상태 확인용
    { set { BooldState = value; } }

    protected virtual void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            searchPlayer();

            if (HitDelay < HitDelayMax) //장판 공격에 연속으로 데미지입지않도록 시간제기
            {
                HitDelay += Time.fixedDeltaTime;
            }
            if (BooldDelay < BooldDelayMax && BooldState) // 출혈상태일때 일정시간마다 데미지를 받도록 시간제기
            {
                BooldDelay += Time.fixedDeltaTime;
            }

            if (BooldDelay > BooldDelayMax && BooldState) //시간이 되면 출혈데미지 받기
            {
                Hit(BooldDamage);
                GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.BloodWeapon] += BooldDamage; // 총데미지양을 체크하기위해 게임매니저에 데미지 전달
                BooldDelay = 0.0f;
            }

            
            tele();
        }
    }

    protected virtual void searchPlayer() //플레이어를 추격하는 함수 Enemy2는 다른방식으로 플레이어를 추격해야하기 때문에 virtual로 설정
    {
        PlayerPos = GameManager.INSTANCE.PLAYER.transform.position;
        if (PlayerPos.x < transform.position.x)
        {
            transform.localScale = new Vector3(0.5f, transform.localScale.y, transform.localScale.z);//플레이어 오른쪽에 있으면 왼쪽을 처다봄
            moveVelocityX = Vector3.left;
        }else if(PlayerPos.x-0.5f> transform.position.x)
        {
            transform.localScale = new Vector3(-0.5f, transform.localScale.y, transform.localScale.z);//플레이어 왼쪽에 있으면 오른쪽을 처다봄
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
    
    public void Hit(int damage) //데미지를 받을경우의 행동
    {
        damage = damage + (int)(damage * (GameManager.INSTANCE.ATTACKLEVEL * 0.5f)); //damage+메인화면 업그레이트 화면에서 업그레이드한 값더하기
        if (GameManager.INSTANCE.DAMAGETEXTQUEUE.Count > 0 && gameObject.activeSelf) // 활성화상태+데미지를 표시할 텍스트가 있으면 실행
        {
            StartCoroutine(HitAnime()); // 데미지를 받을때 깜빡임 코루틴
            GameObject DamageText = GameManager.INSTANCE.DAMAGETEXTQUEUE.Dequeue(); // 데미지텍스트 오브젝트 가져오기
            DamageText.SetActive(true); //데미지텍스트 활성화
            
            //데미지 텍스트를 스크린 기준으로 포지션변경
            float y = Random.Range(0f, 1.5f);
            DamageText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x + 1.0f, transform.position.y+y, transform.position.z));

            DamageText.GetComponent<Text>().text = damage.ToString();
            HP -= damage;

            //출혈을 만드는 기술을 얻었다면 출혈로 만든다.
            if(GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BLOODW && !BooldState)
            {
                BooldState = true;
            }
        }
    }

    public void BoardHit(int damage) // 장판데미지를 받을경우의 행동 Hit와 유사하지만 HitDelay때문에 연속으로 데미지를 입을 수 없다.
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

    IEnumerator HitAnime() //공격당했을때 실행할 코루틴함수
    {
        //CurrentColor = sp.color;
        sp.color = new Color32(0, 0, 0, 255); // 검정색으로 변경
        yield return new WaitForSeconds(0.2f);
        if (Choice)
        {
            sp.color = Color.gray; // 마우스로 선택된 상태에서 공격당하면 gray색으로
        }
        else
        {
            sp.color = new Color32(255, 255, 255, 255); // 선택되지 않은 상태에서 공격당하면 원래색으로
        }
    }

    void tele() // 플레이어와 너무멀리 떨어져 있으면 근처로 텔레포트하는 함수
    {
        int x = Random.Range(0, 2);
        int y = Random.Range(0, 2);
        if(Mathf.Abs(GameManager.INSTANCE.PLAYER.transform.position.x-transform.position.x)>15 || (GameManager.INSTANCE.PLAYER.transform.position.y - transform.position.y)> 15)
        {
            transform.position = new Vector3(GameManager.INSTANCE.PLAYER.transform.position.x + RandLocal[x], GameManager.INSTANCE.PLAYER.transform.position.y + RandLocal[y], GameManager.INSTANCE.PLAYER.transform.position.z);
        }
                     
                
    }
}
