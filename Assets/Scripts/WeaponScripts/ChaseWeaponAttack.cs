using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWeaponAttack : MonoBehaviour
{
    //적을 추격해서 닿으면 공격되게하는 스크립트
    private int ChaseWeaponDamage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) //적과 닿으면 데미지를 주는 코드
        {
            collision.gameObject.GetComponent<Enemy>().Hit(ChaseWeaponDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChaseWeapon] += ChaseWeaponDamage;  // 데미지체크를 위해 게임메니저에 전달
        }
    }
}
