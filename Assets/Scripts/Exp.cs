using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    //경험치 스크립트
    GameObject target = null;
    float exper = 10.0f;

    private void Start()
    {
        target = GameManager.INSTANCE.PLAYER;
        exper = exper + (exper * (GameManager.INSTANCE.EXPLEVEL * 0.5f));
    }

    //자동을 프레이어를 향해 다가온다
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
    // 플레이어와 닿으면 경험치를 올려주고 비활성화
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
