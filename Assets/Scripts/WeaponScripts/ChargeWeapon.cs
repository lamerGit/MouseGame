using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MonoBehaviour
{
    //에너지를 모아서 일정시간 뒤 공격하는 스크립트
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ChargeDamage= 20;

    public IEnumerator ChargeAttack() //일정시간뒤 공격
    {
        yield return new WaitForSeconds(1.6f);

        

        for(int i=EnemyList.Count-1; i>=0; i--)
        {
            EnemyList[i].GetComponent<Enemy>().Hit(ChargeDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChargeWeapon] += ChargeDamage; // 데미지체크를 위해 게임메니저에 전달
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // 적이 콜라이더에 들어오면 리스트에추가
        {
            EnemyList.Add(collision.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // 적이 콜라이더에서 나가면 리스트에서 제거
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
