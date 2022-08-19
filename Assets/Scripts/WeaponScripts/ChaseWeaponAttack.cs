using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseWeaponAttack : MonoBehaviour
{
    //���� �߰��ؼ� ������ ���ݵǰ��ϴ� ��ũ��Ʈ
    private int ChaseWeaponDamage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) //���� ������ �������� �ִ� �ڵ�
        {
            collision.gameObject.GetComponent<Enemy>().Hit(ChaseWeaponDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChaseWeapon] += ChaseWeaponDamage;  // ������üũ�� ���� ���Ӹ޴����� ����
        }
    }
}
