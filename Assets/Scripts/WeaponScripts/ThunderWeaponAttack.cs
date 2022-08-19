using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeaponAttack : MonoBehaviour
{
    //ThunderWeapon에서 받아온 정보로 적을 공격하는 스크립트

    GameObject Target = null;

    Queue<GameObject> targetQueue = new Queue<GameObject>();

    float DelayMax = 0.05f;
    float Delay = 0.0f;
    float DelDelay = 0.0f;

    int countMax = 6;
    int count = 0;

    int ThunderDamage = 5;

    public int COUNTMAX
    {
        set { countMax = value; }
    }
    public Queue<GameObject> TARGETQUEUE
    {
        get { return targetQueue; }
        set { targetQueue = value; }
    }
        
    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            Delay += Time.fixedDeltaTime;
            DelDelay += Time.fixedDeltaTime;

            //타겟큐가 존재하고 countMax만큼 공격안했으면 타겟을 갱신하고 카운트를 올린다.
            if (Delay > DelayMax && targetQueue.Count > 0 && count < countMax) 
            {
                Target = targetQueue.Dequeue();


                count++;
                Delay = 0.0f;
            }


            //타겟이 존재하면 빠른속도로 타겟에게 이동하고 데미지를 주고 다음타겟을 찾기위해 Target을 null로 만듬
            if (Target != null)
            {
               

                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 10.0f);
                Target.GetComponent<Enemy>().Hit(ThunderDamage);
                GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ThunderWeapon] += ThunderDamage;
                Target =null;
                



            }
            //일정시간이 지나면 사라지게 만듬
            if(DelDelay>DelayMax*countMax)
            {
                count = 0;
                GameManager.INSTANCE.THUNDERWEAPONQUEUE.Enqueue(gameObject);
                Target = null;
                DelDelay = 0.0f;
                targetQueue.Clear();
                gameObject.SetActive(false);
            }

            

            
            

        }

    }
    
}
