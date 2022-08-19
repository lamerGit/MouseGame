using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    //데미지 텍스트를 표시해주는 스크립트
    private float MaxDelay = 1.0f;
    private float curDelay = 0.0f;

 
    void Update()
    {
        transform.SetAsFirstSibling();
        if (Time.timeScale != 0)
        {
            transform.position += Vector3.up;
            curDelay += Time.deltaTime;
            if (curDelay > MaxDelay)
            {
                GameManager.INSTANCE.DAMAGETEXTQUEUE.Enqueue(gameObject);

                curDelay = 0.0f;
                gameObject.SetActive(false);

            }
        }
    }

}
