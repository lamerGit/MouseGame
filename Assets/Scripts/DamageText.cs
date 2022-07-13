using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // Start is called before the first frame update
    private float MaxDelay = 1.0f;
    private float curDelay = 0.0f;

    // Update is called once per frame
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
                //Debug.Log(GameManager.INSTANCE.DAMAGETEXTQUEUE.Count);
                curDelay = 0.0f;
                gameObject.SetActive(false);

            }
        }
    }

}
