using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushWeapon : MonoBehaviour
{
    //적들을 밀어내는 스크립트
    List<GameObject> EnemyList = new List<GameObject>();
    private float PushPower = 1.5f;
    Animator anime;

    private void Awake()
    {
        anime = GetComponent<Animator>();
    }

    public IEnumerator PushAttack()
    {
        //적들이 플레이어 기준 어디에 있는지 확인해서 그방향으로 밀어낸다.
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
            EnemyList.Add(collision.gameObject); // 적이 콜라이더에 들어오면 리스트에추가
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) // 적이 콜라이더에서 나가면 리스트에서 제거
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
