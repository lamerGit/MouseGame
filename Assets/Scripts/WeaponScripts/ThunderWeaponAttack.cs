using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeaponAttack : MonoBehaviour
{
    GameObject Target = null;
    // Start is called before the first frame update

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
            /*if(Target != null)
            {
                if(!Target.activeSelf)
                {
                    count = 0;
                    GameManager.INSTANCE.THUNDERWEAPONQUEUE.Enqueue(gameObject);
                    targetQueue.Clear();
                    gameObject.SetActive(false);
                }
            }*/


            if (Delay > DelayMax && targetQueue.Count > 0 && count < countMax)
            {
                Target = targetQueue.Dequeue();


                count++;
                Delay = 0.0f;
            }



            if (Target != null)
            {
                /*if (countMax == 0 || Target == null)
                {
                    count = 0;
                    GameManager.INSTANCE.THUNDERWEAPONQUEUE.Enqueue(gameObject);
                    Target = null;
                    targetQueue.Clear();
                    gameObject.SetActive(false);
                }*/

                transform.position = Vector3.MoveTowards(transform.position, Target.transform.position, 10.0f);
                Target.GetComponent<Enemy>().Hit(ThunderDamage);
                GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ThunderWeapon] += ThunderDamage;
                Target =null;
                /*if (Target.activeSelf)
                {

                    if (count == countMax)
                    {
                        
                    }


                }*/



            }

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
