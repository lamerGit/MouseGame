using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardWeapon : MonoBehaviour
{
    //���ǰ����� �ϴ� ��ũ��Ʈ
    private int BoardDamage = 1;
    private float MaxDelay = 0.5f;
    private float curDelay = 0.0f;

    private List<GameObject> EnemyList=new List<GameObject>();
    private bool Stt = false; //���۵Ǹ� true�� ���� ����
    SpriteRenderer sp = null;

    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Time.timeScale != 0) // �ð��� ���߸� �۵��� �ȵǰ� ����
        {
            curDelay += Time.deltaTime;
            if (curDelay > MaxDelay && Stt)
            {
                Attack();
            }
        }
    }
    public IEnumerator BA() // �����ð��� ���ӸŴ����� �����ְ� ��Ȱ��ȭ
    {
        yield return new WaitForSeconds(1.5f);
        
        GameManager.INSTANCE.BOARDWEAPONQUEUE.Enqueue(gameObject);
        Stt = false;
        sp.color = Color.gray;
        gameObject.SetActive(false);
    }

    public void AttackStart()
    {
        Stt = true;
    }

    void Attack() //������ ���۵Ǹ� ����Ʈ�� �ִ� ������� �����Ѵ�.
    {

        for (int i = EnemyList.Count - 1; i >= 0; i--)
        {
            EnemyList[i].gameObject.GetComponent<Enemy>().BoardHit(BoardDamage);
            GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.BoardWeapon] += BoardDamage; // ������üũ�� ���� ���Ӹ޴����� ����
        }
        curDelay = 0.0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy")) // ���� �ݶ��̴��� ������ ����Ʈ���߰�
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
