using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWeapon : MonoBehaviour
{
    //적을 서서히 추격하는 스크립트
    Rigidbody2D rb2d = null;

    GameObject Target = null;

    Vector3 moveVelocityX = Vector3.zero;
    Vector3 moveVelocityY = Vector3.zero;

    Queue<GameObject> TargetQueue = new Queue<GameObject>();

    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0) //시간이 멈추면 작동안하겠끔 설정
        {

            if (Target == null && TargetQueue.Count > 0) // 타겟큐에서 타겟을 받아옴
            {

                Target = TargetQueue.Dequeue();


            }





            if (Target != null) // 타겟이 있으면 타겟을 방향으로 날아간다.
            {
                if (Target.transform.position.x < transform.position.x)
                {
                    moveVelocityX = Vector3.left;
                }
                else if (Target.transform.position.x > transform.position.x)
                {
                    moveVelocityX = Vector3.right;
                }

                if (Target.transform.position.y < transform.position.y)
                {
                    moveVelocityY = Vector3.down;
                }
                else if (Target.transform.position.y > transform.position.y)
                {
                    moveVelocityY = Vector3.up;
                }
                //타겟을 바라보게 설정
                Vector3 dir = (Target.transform.position - transform.position).normalized;
                float angle = Vector2.SignedAngle(Vector2.down, dir);
                Quaternion qut = new Quaternion();
                qut.eulerAngles = new Vector3(0, 0, angle);
                transform.rotation = qut;


                rb2d.AddForce((moveVelocityX + moveVelocityY) * 0.1f, ForceMode2D.Impulse);
            }


            //속도가 너무 빨라지지 않게 조절
            if (rb2d.velocity.x > 5.0f)
            {
                rb2d.velocity = new Vector2(5.0f, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -5.0f)
            {
                rb2d.velocity = new Vector2(-5.0f, rb2d.velocity.y);
            }
            if (rb2d.velocity.y > 5.0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, 5.0f);
            }
            if (rb2d.velocity.y < -5.0f)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, -5.0f);
            }
  
            //타겟이 사라지면 더이상 추격하지않게 타겟을 null로 만듬
            if (Target != null)
            {
                if (!Target.activeSelf)
                {
                    Target = null;
                }
            }

            //플레이어가 공격을 끝네고 너무멀리떨어져 있으면 돌아오게하는 코드
            if (Input.GetMouseButtonUp(0))
            {
                Tele();
            }
        }
    }

    //플레이어와 너무멀리떨어져 있으면 돌아오게하는 코드
    void Tele()
    {
        if (Mathf.Abs(GameManager.INSTANCE.PLAYER.transform.position.x - transform.position.x) > 15 || (GameManager.INSTANCE.PLAYER.transform.position.y - transform.position.y) > 15)
        {
            transform.position = GameManager.INSTANCE.PLAYER.transform.position;
            rb2d.velocity = Vector2.zero;
            Target = null;
        }

    }
    //트리거에 들어온 적들을 queue에 넣는다.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") )
        {
            //Target = collision.gameObject;
            TargetQueue.Enqueue(collision.gameObject);
        }
    }
}
