using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float startTime = 200.0f;
    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while(true)
        {
            if(GameManager.INSTANCE.ENEMYQUEUE.Count>0 && GameManager.INSTANCE.TIMESCRIPT.TIMER<startTime)
            {
                GameObject E=GameManager.INSTANCE.ENEMYQUEUE.Dequeue();
                Enemy EG=E.GetComponent<Enemy>();
                //Debug.Log($"{GameManager.INSTANCE.ENEMYQUEUE.Count}");
                //Debug.Log($"{GameManager.INSTANCE.DAMAGETEXTQUEUE.Count}");
                E.GetComponent<SpriteRenderer>().color = Color.white;
                EG.BOOLDSTATE = false;
                EG.CHOICE = false;
                EG.HP = EG.MAXHP + GameManager.INSTANCE.ENEMYEXTRAHP;
               // E.GetComponent<Enemy>().BOOLDSTATE = false;
               // E.GetComponent<Enemy>().CHOICE = false;
               // E.GetComponent<Enemy>().HP = E.GetComponent<Enemy>().MAXHP + GameManager.INSTANCE.ENEMYEXTRAHP;



                E.SetActive(true);
                E.transform.SetParent(null);
                E.transform.position = transform.position;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
