using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while(true)
        {
            if(GameManager.INSTANCE.ENEMYQUEUE.Count>0)
            {
                GameObject E=GameManager.INSTANCE.ENEMYQUEUE.Dequeue();
                //Debug.Log($"{GameManager.INSTANCE.ENEMYQUEUE.Count}");
                //Debug.Log($"{GameManager.INSTANCE.DAMAGETEXTQUEUE.Count}");
                E.GetComponent<SpriteRenderer>().color = Color.white;
                E.GetComponent<Enemy>().BOOLDSTATE = false;
                E.GetComponent<Enemy>().CHOICE = false;
                E.SetActive(true);
                E.transform.SetParent(null);
                E.transform.position = transform.position;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
