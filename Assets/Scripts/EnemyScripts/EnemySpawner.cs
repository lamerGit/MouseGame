using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float startTime = 200.0f; //일정 시간마다 작동하게끔 설정하기 위한 변수

    float spawnRange = 2.0f; // 일정한곳 기준 주변에서 소환되게 하는 변수
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

                //소환될때 상태초기화
                E.GetComponent<SpriteRenderer>().color = Color.white;
                EG.BOOLDSTATE = false;
                EG.CHOICE = false;
                EG.HP = EG.MAXHP + GameManager.INSTANCE.ENEMYEXTRAHP;
               


                E.SetActive(true);
                E.transform.SetParent(null);

                //스포너 주변에 소환되게끔 설정
                Vector2 randPos = Random.insideUnitCircle * spawnRange;
                E.transform.position = transform.position+(Vector3)randPos;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
