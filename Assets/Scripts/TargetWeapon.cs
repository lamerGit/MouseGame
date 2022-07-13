using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeapon : MonoBehaviour
{
    private int TargetDamage = 10;

    private List<GameObject> EnemyList=new List<GameObject> ();

    private int MaxCount = 3;


    public IEnumerator TargetAttack()
    {
        yield return new WaitForSeconds(1.5f);
        int cnt = 0;
        foreach (GameObject enemy in EnemyList)
        {
            if (cnt < MaxCount)
            {
                GameObject targetSprite = GameManager.INSTANCE.TARGETWEAPONQUEUE.Dequeue();
                targetSprite.SetActive(true);
                targetSprite.transform.position = enemy.transform.position;
                enemy.GetComponent<Enemy>().Hit(TargetDamage);
            }
            cnt++;
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
