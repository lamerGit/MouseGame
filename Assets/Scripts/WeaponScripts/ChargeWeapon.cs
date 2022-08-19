using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeWeapon : MonoBehaviour
{
    //�������� ��Ƽ� �����ð� �� �����ϴ� ��ũ��Ʈ
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ChargeDamage= 20;

    public IEnumerator ChargeAttack() //�����ð��� ����
    {
        yield return new WaitForSeconds(1.6f);

        

        for(int i=EnemyList.Count-1; i>=0; i--)
        {
            EnemyList[i].GetComponent<Enemy>().Hit(ChargeDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ChargeWeapon] += ChargeDamage; // ������üũ�� ���� ���Ӹ޴����� ����
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))  // ���� �ݶ��̴��� ������ ����Ʈ���߰�
        {
            EnemyList.Add(collision.gameObject);
            
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) // ���� �ݶ��̴����� ������ ����Ʈ���� ����
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
