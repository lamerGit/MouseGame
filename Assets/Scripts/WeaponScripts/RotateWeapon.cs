using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    //플레이어 주변을 빙글빙글 돌면서 공격하는 스크립트
    float Speed = 200.0f;
    private int RotateDamage = 3;
    

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            //플레이어 주변을 공전하는 코드
            transform.RotateAround(GameManager.INSTANCE.PLAYER.transform.position, Vector3.forward, Speed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);//플레이어를 처다보지 않게 고정
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) //적과 닿으면 데미지
        {
            collision.gameObject.GetComponent<Enemy>().Hit(RotateDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.RotateWeapon] += RotateDamage;  // 데미지체크를 위해 게임메니저에 전달
        }
    }

    
}
