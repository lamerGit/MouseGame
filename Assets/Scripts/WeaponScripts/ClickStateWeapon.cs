using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStateWeapon : MonoBehaviour
{
    //Ŭ�����¸� �����ϸ� �����ϴ� ��ũ��Ʈ
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ClickStateDamage = 2;
    private bool ClickState = false;

    private float HitDelayMax = 1.0f;
    private float HitDelay = 0.0f;

    public GameObject[] FireObject;

    public bool CLICKSTATE // ClickStateWeapon�� Ŭ���ÿ��� Ȱ��ȭ �Ϸ��� ���� ������Ƽ
    {
        set { ClickState = value; }
    }
    private void Update()
    {
        if (Time.timeScale != 0)
        {
            if (HitDelay < HitDelayMax && ClickState) 
            {
                HitDelay += Time.deltaTime;
            }

            if (HitDelay > HitDelayMax && ClickState) // Mouse��ũ��Ʈ���� Ȱ���ϰ� �ð��̵Ǹ� �ߵ�
            {
                
                for (int i = EnemyList.Count - 1; i >= 0; i--)
                {
                    EnemyList[i].GetComponent<Enemy>().Hit(ClickStateDamage);
                    GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ClickStateWeapon] += ClickStateDamage;  // ������üũ�� ���� ���Ӹ޴����� ����
                }

                HitDelay = 0.0f;

            }
        }
    }

    // ���� �ݶ��̴��� ������ ����Ʈ���߰�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject);
        }
    }
    // ���� �ݶ��̴����� ������ ����Ʈ���� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }

    public void FireOn() // �ֺ��� ��Ÿ�� ��ũ��Ʈ�� Ű������ �Լ�
    {
        foreach(GameObject f in FireObject)
        {
            f.SetActive(true);
        }
    }

    public void FireOff()// �ֺ��� ��Ÿ�� ��ũ��Ʈ�� �������� �Լ�
    {
        foreach (GameObject f in FireObject)
        {
            f.SetActive(false);
        }
    }

}

