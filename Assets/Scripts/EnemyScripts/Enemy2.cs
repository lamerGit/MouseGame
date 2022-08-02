using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{
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
                //BooldState = false;
                //Choice = false;
                //sp.color = Color.red;
                gameObject.SetActive(false);
            }
        }
    }

    private void Start()
    {
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

    protected override void searchPlayer()
    {
        PlayerPos = GameManager.INSTANCE.PLAYER.transform.position;
        transform.Translate(Vector3.right*Time.deltaTime*SPEED*6.0f, Space.Self);
        if ((PlayerPos - transform.position).sqrMagnitude > 200.0f)
        {
            Vector3 dir = PlayerPos - transform.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

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
