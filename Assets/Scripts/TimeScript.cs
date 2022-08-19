using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeScript : MonoBehaviour
{
    //�ð�ǥ�ÿ� ��ũ��Ʈ
    Text text;
    float timer = 300.0f;

    string TimeText = "";

    float GameLevelTimer = 0.0f;

    public float TIMER
    {
        get { return timer; }
    }

    private void Awake()
    {
        text = GetComponent<Text>();
    }
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            GameLevelTimer+=Time.deltaTime;
            TimeText = timer.ToString("00.00");
            TimeText = TimeText.Replace(".", ":");
            text.text = TimeText;

            if(GameLevelTimer>50.0f)//�����ð��� ������ ����ü������
            {
                GameLevelTimer = 0;
                GameManager.INSTANCE.ENEMYEXTRAHP += 10;
            }

            if(GameLevelTimer>299.9f) // �ð��� �ٵǸ� �÷��̾ �ڵ����� ����
            {
                GameManager.INSTANCE.PLAYER.GetComponent<Player>().CURRENTHP -= 999;
            }
        }

    }
}
