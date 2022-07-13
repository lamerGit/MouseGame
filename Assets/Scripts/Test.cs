using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Rigidbody2D rb2d = null;

    GameObject Target = null;

    Vector3 moveVelocityX= Vector3.zero;
    Vector3 moveVelocityY = Vector3.zero;
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(Target!=null)
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

            //transform.position += moveVelocityX * 0.1f * Time.deltaTime;
            //transform.position += moveVelocityY * 0.1f * Time.deltaTime;
            rb2d.AddForce((moveVelocityX + moveVelocityY)*0.1f, ForceMode2D.Impulse);
        }

        if(rb2d.velocity.x>5.0f)
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
        //Mathf.Clamp(rb2d.velocity.x, -5.0f, 5.0f);
        //Mathf.Clamp(rb2d.velocity.y, -5.0f, 5.0f);

        if (Target != null)
        {
            if (!Target.activeSelf)
            {
                Target = null;
            }
        }
        

        Tele();
    }

    void Tele()
    {
        if (Mathf.Abs(GameManager.INSTANCE.PLAYER.transform.position.x - transform.position.x) > 15 || (GameManager.INSTANCE.PLAYER.transform.position.y - transform.position.y) > 15)
        {
            transform.position = GameManager.INSTANCE.PLAYER.transform.position;
            rb2d.velocity = Vector2.zero;
            Target = null;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && Target==null)
        {
            Target = collision.gameObject;
        }
    }
}
