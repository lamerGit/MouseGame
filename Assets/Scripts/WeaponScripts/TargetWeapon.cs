using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeapon : MonoBehaviour
{
    //�ֺ��� 3������ ���� �����ϴ� ��ũ��Ʈ
    private int TargetDamage = 10;

    private List<GameObject> EnemyList=new List<GameObject> ();

    private int MaxCount = 3;


    public IEnumerator TargetAttack()
    {
        yield return new WaitForSeconds(1.5f);
        if (EnemyList.Count > 2) //�ֺ��� 3�����̻��� ���� ������ �ߵ�
        {
            int cnt = 0;
            

            for (int i = EnemyList.Count - 1; i >= 0; i--)
            {
                if (cnt < MaxCount)
                {
                    GameObject targetSprite = GameManager.INSTANCE.TARGETWEAPONQUEUE.Dequeue(); //Xǥ�ø� �ٿ������ GameManager���� �޾ƿ�
                    targetSprite.SetActive(true);
                    targetSprite.transform.position = EnemyList[i].transform.position;
                    EnemyList[i].GetComponent<Enemy>().Hit(TargetDamage);
                    GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.TargetWeapon] += TargetDamage; // ������üũ�� ���� ���Ӹ޴����� ����
                }
                cnt++;
            }
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject); // ���� �ݶ��̴��� ������ ����Ʈ���߰�

        }
    }
        

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject); // ���� �ݶ��̴����� ������ ����Ʈ���� ����
        }
    }
}
