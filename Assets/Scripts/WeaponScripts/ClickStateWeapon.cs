using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStateWeapon : MonoBehaviour
{
    //클릭상태를 유지하면 공격하는 스크립트
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ClickStateDamage = 2;
    private bool ClickState = false;

    private float HitDelayMax = 1.0f;
    private float HitDelay = 0.0f;

    public GameObject[] FireObject;

    public bool CLICKSTATE // ClickStateWeapon을 클릭시에만 활성화 하려고 만든 프로퍼티
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

            if (HitDelay > HitDelayMax && ClickState) // Mouse스크립트에서 활성하고 시간이되면 발동
            {
                
                for (int i = EnemyList.Count - 1; i >= 0; i--)
                {
                    EnemyList[i].GetComponent<Enemy>().Hit(ClickStateDamage);
                    GameManager.INSTANCE.WEAPONDAMAGES[WeaponEnum.ClickStateWeapon] += ClickStateDamage;  // 데미지체크를 위해 게임메니저에 전달
                }

                HitDelay = 0.0f;

            }
        }
    }

    // 적이 콜라이더에 들어오면 리스트에추가
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject);
        }
    }
    // 적이 콜라이더에서 나가면 리스트에서 제거
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }

    public void FireOn() // 주변에 불타는 스크립트를 키기위한 함수
    {
        foreach(GameObject f in FireObject)
        {
            f.SetActive(true);
        }
    }

    public void FireOff()// 주변에 불타는 스크립트를 끄기위한 함수
    {
        foreach (GameObject f in FireObject)
        {
            f.SetActive(false);
        }
    }

}

