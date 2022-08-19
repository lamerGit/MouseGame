using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardWeapon : MonoBehaviour
{
    //장판공격을 하는 스크립트
    private int BoardDamage = 1;
    private float MaxDelay = 0.5f;
    private float curDelay = 0.0f;

    private List<GameObject> EnemyList=new List<GameObject>();
    private bool Stt = false; //시작되면 true로 변할 변수
    SpriteRenderer sp = null;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.timeScale != 0) // 시간이 멈추면 작동이 안되게 설정
        {
            curDelay += Time.deltaTime;
            if (curDelay > MaxDelay && Stt)
            {
                Attack();
            }
        }
    }
    public IEnumerator BA() // 일정시간후 게임매니저에 돌려주고 비활성화
    {
        yield return new WaitForSeconds(1.5f);
        
        GameManager.INSTANCE.BOARDWEAPONQUEUE.Enqueue(gameObject);
        Stt = false;
        sp.color = Color.gray;
        gameObject.SetActive(false);
    }

    public void AttackStart()
    {
        Stt = true;
    }

    void Attack() //공격이 시작되면 리스트에 있는 모든적을 공격한다.
    {

        for (int i = EnemyList.Count - 1; i >= 0; i--)
        {
            EnemyList[i].gameObject.GetComponent<Enemy>().BoardHit(BoardDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.BoardWeapon] += BoardDamage; // 데미지체크를 위해 게임메니저에 전달
        }
        curDelay = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) // 적이 콜라이더에 들어오면 리스트에추가
        {
            EnemyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // 적이 콜라이더에서 나가면 리스트에서 제거
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
