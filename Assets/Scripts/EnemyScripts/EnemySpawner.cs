using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float startTime = 200.0f; //���� �ð����� �۵��ϰԲ� �����ϱ� ���� ����

    float spawnRange = 2.0f; // �����Ѱ� ���� �ֺ����� ��ȯ�ǰ� �ϴ� ����
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

                //��ȯ�ɶ� �����ʱ�ȭ
                E.GetComponent<SpriteRenderer>().color = Color.white;
                EG.BOOLDSTATE = false;
                EG.CHOICE = false;
                EG.HP = EG.MAXHP + GameManager.INSTANCE.ENEMYEXTRAHP;
               


                E.SetActive(true);
                E.transform.SetParent(null);

                //������ �ֺ��� ��ȯ�ǰԲ� ����
                Vector2 randPos = Random.insideUnitCircle * spawnRange;
                E.transform.position = transform.position+(Vector3)randPos;
            }
            yield return new WaitForSeconds(3.0f);
        }
    }
}
