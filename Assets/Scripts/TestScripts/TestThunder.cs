using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThunder : MonoBehaviour
{
    GameObject Target = null;
    // Start is called before the first frame update
    Vector3 moveVelocityX= Vector3.zero;
    Vector3 moveVelocityY = Vector3.zero;


    
    Queue<GameObject> targetQueue = new Queue<GameObject>();

    float DelayMax = 0.15f;
    float Delay = 0.0f;

    int countMax = 3;
    int count = 0;
    private void FixedUpdate()
    {
        Delay += Time.deltaTime;
        if(Delay > DelayMax && targetQueue.Count>0 && count<countMax)
        {
            Target = targetQueue.Dequeue();
            Target.GetComponent<Enemy>().Hit(5);
            count++;
            Delay = 0.0f;
        }

        if (Target != null)
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

            //transform.position += moveVelocityX * 5f * Time.deltaTime;
            //transform.position += moveVelocityY * 5f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 1.5f);
            //rb2d.AddForce((moveVelocityX + moveVelocityY) * 0.1f, ForceMode2D.Impulse);
            /*if (!Target.activeSelf)
            {
                Destroy(gameObject);
            }*/
        }
        
        if(count == countMax)
        {
            count = 0;
            gameObject.SetActive(false);
        }
       
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") )
        {
            targetQueue.Enqueue(collision.gameObject);
        }
   
    }
}
