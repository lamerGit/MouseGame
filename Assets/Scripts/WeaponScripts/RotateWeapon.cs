using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWeapon : MonoBehaviour
{
    float Speed = 200.0f;
    private int RotateDamage = 3;
    

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void FixedUpdate()
    {
        if (Time.timeScale != 0)
        {
            transform.RotateAround(GameManager.INSTANCE.PLAYER.transform.position, Vector3.forward, Speed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Hit(RotateDamage);
        }
    }

    
}
