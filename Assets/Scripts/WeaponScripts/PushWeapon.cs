using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWeapon : MonoBehaviour
{
    List<GameObject> EnemyList = new List<GameObject>();
    private float PushPower = 1.5f;
    Animator anime;

    private void Awake()
    {
        anime = GetComponent<Animator>();
    }

    public IEnumerator PushAttack()
    {
        yield return new WaitForSeconds(1.5f);
        anime.SetTrigger("Attack");
        foreach(var enemy in EnemyList)
        {
            if(GameManager.INSTANCE.PLAYER.transform.position.x-enemy.transform.position.x>0)
            {
                enemy.transform.position= new Vector3(enemy.transform.position.x-PushPower,enemy.transform.position.y,enemy.transform.position.z); 
            }
            if (GameManager.INSTANCE.PLAYER.transform.position.x - enemy.transform.position.x < 0)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x + PushPower, enemy.transform.position.y, enemy.transform.position.z);
            }
            if (GameManager.INSTANCE.PLAYER.transform.position.y - enemy.transform.position.y > 0)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y - PushPower, enemy.transform.position.z);
            }
            if (GameManager.INSTANCE.PLAYER.transform.position.y - enemy.transform.position.y < 0)
            {
                enemy.transform.position = new Vector3(enemy.transform.position.x, enemy.transform.position.y + PushPower, enemy.transform.position.z);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject);
            //Debug.Log("ÀûÀÌ µé¾î¿È");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
