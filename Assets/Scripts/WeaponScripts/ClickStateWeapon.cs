using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickStateWeapon : MonoBehaviour
{
    private List<GameObject> EnemyList = new List<GameObject>();
    private int ClickStateDamage = 2;
    private bool ClickState = false;

    private float HitDelayMax = 1.0f;
    private float HitDelay = 0.0f;

    public GameObject[] FireObject;

    public bool CLICKSTATE
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

            if (HitDelay > HitDelayMax && ClickState)
            {
                foreach (GameObject enemy in EnemyList)
                {
                    enemy.GetComponent<Enemy>().Hit(ClickStateDamage);

                }
                HitDelay = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyList.Remove(collision.gameObject);
        }
    }

    public void FireOn()
    {
        foreach(GameObject f in FireObject)
        {
            f.SetActive(true);
        }
    }

    public void FireOff()
    {
        foreach (GameObject f in FireObject)
        {
            f.SetActive(false);
        }
    }

}

