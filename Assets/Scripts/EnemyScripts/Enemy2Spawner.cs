using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Spawner : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            if (GameManager.INSTANCE.ENEMY2QUEUE.Count > 20)
            {
                for (int i = 0; i < 20; i++)
                {
                    GameObject E = GameManager.INSTANCE.ENEMY2QUEUE.Dequeue();
                    Enemy EG = E.GetComponent<Enemy>();
                    E.GetComponent<SpriteRenderer>().color = Color.white;
                    EG.BOOLDSTATE = false;
                    EG.CHOICE = false;
                    EG.HP = EG.MAXHP + GameManager.INSTANCE.ENEMYEXTRAHP;


                    E.SetActive(true);
                    E.transform.SetParent(null);
                    float randX = Random.Range(-1.0f, 1.0f);
                    float randy = Random.Range(-1.0f, 1.0f);

                    E.transform.position = new Vector2(transform.position.x + randX, transform.position.y + randy);
                }
            }
            yield return new WaitForSeconds(15.0f);
        }
    }
}
