using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeapon : MonoBehaviour
{
    //주변에 3마리의 적을 공격하는 스크립트
    private int TargetDamage = 10;

    private List<GameObject> EnemyList=new List<GameObject> ();

    private int MaxCount = 3;


    public IEnumerator TargetAttack()
    {
        yield return new WaitForSeconds(1.5f);
        if (EnemyList.Count > 2) //주변에 3마리이상의 적이 있으때 발동
        {
            int cnt = 0;
            

            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                if (cnt < MaxCount)
                {
                    GameObject targetSprite = GameManager.INSTANCE.TARGETWEAPONQUEUE.Dequeue(); //X표시를 뛰우기위해 GameManager에서 받아옴
                    targetSprite.SetActive(true);
                    targetSprite.transform.position = EnemyList[i].transform.position;
                    EnemyList[i].GetComponent<Enemy>().Hit(TargetDamage);
                    GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.TargetWeapon] += TargetDamage; // 데미지체크를 위해 게임메니저에 전달
                }
                cnt++;
            }
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject); // 적이 콜라이더에 들어오면 리스트에추가

        }
    }
        

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject); // 적이 콜라이더에서 나가면 리스트에서 제거
        }
    }
}
