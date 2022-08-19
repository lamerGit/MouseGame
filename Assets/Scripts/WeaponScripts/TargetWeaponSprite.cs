using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetWeaponSprite : MonoBehaviour
{
    //X표시를 해주고 일정시간뒤 사라지는 스크립트
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
