using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWeaponAttack : MonoBehaviour
{
    private int ChaseWeaponDamage = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(ChaseWeaponDamage);
        }
    }
}
