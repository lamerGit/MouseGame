using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{

    GameObject target = null;
    float exper = 10.0f;

    private void Start()
    {
        target = GameManager.INSTANCE.PLAYER;
        exper = exper + (exper * (GameManager.INSTANCE.EXPLEVEL * 0.5f));
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
            collision.gameObject.GetComponent<Player>().CURRENTEXP += exper;
            GameManager.INSTANCE.EXPQUEUE.Enqueue(gameObject);
            gameObject.SetActive(false);
        }
    }

  
}
