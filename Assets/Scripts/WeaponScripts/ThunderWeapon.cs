using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeapon : MonoBehaviour
{
    // 여러적을 순서대로 공격하게 계산한다음 ThunderWeaponAttack에 넘겨주는 스크립트

    private List<GameObject> EnemyList=new List<GameObject>();
    int CountMax = 6; // 변수에 입력되 숫자만큼 적들을 공격

    public IEnumerator ThunderAttack ()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject Thunder=GameManager.INSTANCE.THUNDERWEAPONQUEUE.Dequeue();
        Thunder.transform.position = GameManager.INSTANCE.PLAYER.transform.position;

        //CountMax 보다 큰값에 적들의 수가 있으면 CountMax만큼 공격하고 아니면 EnemyList에 있는 만큼만 공격한다.
        if (EnemyList.Count > CountMax)
        {
            Thunder.GetComponent<ThunderWeaponAttack>().COUNTMAX = CountMax;
            for(int i=0; i<CountMax; i++)
            {
                Thunder.GetComponent<ThunderWeaponAttack>().TARGETQUEUE.Enqueue(EnemyList[i]);
                
            }
        }else 
        {
            

            Thunder.GetComponent<ThunderWeaponAttack>().COUNTMAX = EnemyList.Count;
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Thunder.GetComponent<ThunderWeaponAttack>().TARGETQUEUE.Enqueue(EnemyList[i]);
                
            }
        }
        EnemyList.Clear();

        Thunder.SetActive(true);
    
    }

    //적들을 중복되지않게 리스트에 넣는 코드
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int number = EnemyList.FindIndex(x => x == collision.gameObject);
            if (number == -1)
            {
                EnemyList.Add(collision.gameObject);


            }
        }
    }
    //적들이 콜라이더에서 나가면 리스트에서 제거
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
