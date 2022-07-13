using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MonoBehaviour
{
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ChargeDamage=10;
    
    public IEnumerator ChargeAttack()
    {
        yield return new WaitForSeconds(1.6f);
        foreach (GameObject enemy in EnemyList)
        {
            enemy.GetComponent<Enemy>().Hit(ChargeDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject);
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
