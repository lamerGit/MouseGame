using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MonoBehaviour
{
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ChargeDamage= 10;

    public IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(1.6f);

        /*foreach (GameObject enemy in EnemyList)
        {
            enemy.GetComponent<Enemy>().Hit(ChargeDamage);
            //GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChargeWeapon] += ChargeDamage;
        }*/

        /*for (int i = 0; i < EnemyList.Count; i++)
        {
            
            EnemyList[i].GetComponent<Enemy>().Hit(ChargeDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChargeWeapon] += ChargeDamage;
        }*/

        for(int i=EnemyList.Count-1; i>=0; i--)
        {
            EnemyList[i].GetComponent<Enemy>().Hit(ChargeDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChargeWeapon] += ChargeDamage;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject);
            /*int number = EnemyList.FindIndex(x => x == collision.gameObject);
            if (number == -1)
            {
                EnemyList.Add(collision.gameObject);
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
