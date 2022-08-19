using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
    //한쪽방향으로 날라오는 적의 스크립트

    //기존적인 행동은 Enemy와 비슷하기 때문에 Enemy를 상속

    //돌려줘야하는 queue가 다르기때문에 oveeride

    public override int HP { get { return hp; }
        set
        {
            hp = value;

            if (hp < 1)
            {
                hp = MaxHp;
                GameManager.INSTANCE.ENEMY2QUEUE.Enqueue(gameObject);
                if (GameManager.INSTANCE.EXPQUEUE.Count > 0)
                {
                    GameObject ExpDrop = GameManager.INSTANCE.EXPQUEUE.Dequeue();
                    ExpDrop.transform.position = transform.position;
                    ExpDrop.SetActive(true);
                }
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
        //등장시 플레이어를 처다봄

        PlayerPos = GameManager.INSTANCE.PLAYER.transform.position;
        Vector3 dir = PlayerPos - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected override void FixedUpdate()
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

        }
    }

    //일정거리이상 플레이어와 떨어지면 다시 플레이어를 처다보는 코드 + 오른쪽으로만 날라감
    protected override void searchPlayer()
    {
        PlayerPos = GameManager.INSTANCE.PLAYER.transform.position;
        transform.Translate(Vector3.right*Time.deltaTime*SPEED*6.0f, Space.Self);
        if ((PlayerPos - transform.position).sqrMagnitude > 200.0f)
        {
            Vector3 dir = PlayerPos - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);



            //플레이어왼쪽 오르쪽에 있을때 플레이어를 바라보게끔 변경
            if (PlayerPos.x < transform.position.x)
            {
                transform.localScale = new Vector3(transform.localScale.x, 0.5f, transform.localScale.z);
            }
            else if (PlayerPos.x - 0.5f > transform.position.x)
            {
                transform.localScale = new Vector3(transform.localScale.x, -0.5f, transform.localScale.z);
            }

        }
    }
    
}
