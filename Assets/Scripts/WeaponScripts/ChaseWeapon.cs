using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWeapon : MonoBehaviour
{
    //���� ������ �߰��ϴ� ��ũ��Ʈ
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
        if (Time.timeScale != 0) //�ð��� ���߸� �۵����ϰڲ� ����
        {

            if (Target == null && TargetQueue.Count > 0) // Ÿ��ť���� Ÿ���� �޾ƿ�
            {

                Target = TargetQueue.Dequeue();


            }





            if (Target != null) // Ÿ���� ������ Ÿ���� �������� ���ư���.
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
                //Ÿ���� �ٶ󺸰� ����
                Vector3 dir = (Target.transform.position - transform.position).normalized;
                float angle = Vector2.SignedAngle(Vector2.down, dir);
                Quaternion qut = new Quaternion();
                qut.eulerAngles = new Vector3(0, 0, angle);
                transform.rotation = qut;


                rb2d.AddForce((moveVelocityX + moveVelocityY) * 0.1f, ForceMode2D.Impulse);
            }


            //�ӵ��� �ʹ� �������� �ʰ� ����
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
  
            //Ÿ���� ������� ���̻� �߰������ʰ� Ÿ���� null�� ����
            if (Target != null)
            {
                if (!Target.activeSelf)
                {
                    Target = null;
                }
            }

            //�÷��̾ ������ ���װ� �ʹ��ָ������� ������ ���ƿ����ϴ� �ڵ�
            if (Input.GetMouseButtonUp(0))
            {
                Tele();
            }
        }
    }

    //�÷��̾�� �ʹ��ָ������� ������ ���ƿ����ϴ� �ڵ�
    void Tele()
    {
        if (Mathf.Abs(GameManager.INSTANCE.PLAYER.transform.position.x - transform.position.x) > 15 || (GameManager.INSTANCE.PLAYER.transform.position.y - transform.position.y) > 15)
        {
            transform.position = GameManager.INSTANCE.PLAYER.transform.position;
            rb2d.velocity = Vector2.zero;
            Target = null;
        }

    }
    //Ʈ���ſ� ���� ������ queue�� �ִ´�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") )
        {
            //Target = collision.gameObject;
            TargetQueue.Enqueue(collision.gameObject);
        }
    }
}
