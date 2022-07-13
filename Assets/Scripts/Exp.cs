using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{

    GameObject target = null;

    private void Start()
    {
        target = GameManager.INSTANCE.PLAYER;
    }

    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            if (target != null)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 0.5f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().CURRENTEXP += 10.0f;
            GameManager.INSTANCE.EXPQUEUE.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

  
}
