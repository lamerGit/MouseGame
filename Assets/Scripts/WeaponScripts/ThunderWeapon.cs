using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderWeapon : MonoBehaviour
{
    // �������� ������� �����ϰ� ����Ѵ��� ThunderWeaponAttack�� �Ѱ��ִ� ��ũ��Ʈ

    private List<GameObject> EnemyList=new List<GameObject>();
    int CountMax = 6; // ������ �Էµ� ���ڸ�ŭ ������ ����

    public IEnumerator ThunderAttack ()
    {
        yield return new WaitForSeconds(1.5f);
        GameObject Thunder=GameManager.INSTANCE.THUNDERWEAPONQUEUE.Dequeue();
        Thunder.transform.position = GameManager.INSTANCE.PLAYER.transform.position;

        //CountMax ���� ū���� ������ ���� ������ CountMax��ŭ �����ϰ� �ƴϸ� EnemyList�� �ִ� ��ŭ�� �����Ѵ�.
        if (EnemyList.Count > CountMax)
        {
            Thunder.GetComponent<ThunderWeaponAttack>().COUNTMAX = CountMax;
            for(int i=0; i<CountMax; i++)
            {
                Thunder.GetComponent<ThunderWeaponAttack>().TARGETQUEUE.Enqueue(EnemyList[i]);
                
            }
        }else 
        {
            

            Thunder.GetComponent<ThunderWeaponAttack>().COUNTMAX = EnemyList.Count;
            for (int i = 0; i < EnemyList.Count; i++)
            {
                Thunder.GetComponent<ThunderWeaponAttack>().TARGETQUEUE.Enqueue(EnemyList[i]);
                
            }
        }
        EnemyList.Clear();

        Thunder.SetActive(true);
    
    }

    //������ �ߺ������ʰ� ����Ʈ�� �ִ� �ڵ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            int number = EnemyList.FindIndex(x => x == collision.gameObject);
            if (number == -1)
            {
                EnemyList.Add(collision.gameObject);


            }
        }
    }
    //������ �ݶ��̴����� ������ ����Ʈ���� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }
}
