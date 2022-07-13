using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeaponSprite : MonoBehaviour
{
    private float MaxDelay = 1.0f;
    private float curDelay = 0.0f;


    void Update()
    {
        if (Time.timeScale != 0)
        {
            curDelay += Time.deltaTime;
            if (curDelay > MaxDelay)
            {
                GameManager.INSTANCE.TARGETWEAPONQUEUE.Enqueue(gameObject);
                curDelay = 0.0f;
                gameObject.SetActive(false);
            }
        }
    }
}
