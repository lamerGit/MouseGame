using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDamage_UI : MonoBehaviour
{
    //���ӿ����� �� �������� ������ ��ũ��Ʈ

    Image WeaponImage;
    Text WeaponDamageText;
    Image WeaponGauge;

    public float MaxDamage = 5000.0f;
    float startPoint = 0.0f;
    
    public void SetDamageUI(Sprite WeaponSprite,float Damage)
    {
        WeaponImage = transform.Find("WeaponImage").GetComponent<Image>();
        WeaponDamageText = transform.Find("DamageText").GetComponent<Text>();
        WeaponGauge = transform.Find("DamageGauge").GetComponent<Image>();

        WeaponImage.sprite = WeaponSprite;
        WeaponDamageText.text = $"{Damage:N0}";
        //WeaponGauge.fillAmount = Damage / MaxDamage;
        StartCoroutine(SlowUp(Damage));
    }

    IEnumerator SlowUp(float d)
    {
        while (startPoint<d)
        {
            WeaponGauge.fillAmount = startPoint / MaxDamage;
            startPoint+=Time.deltaTime*1000.0f;
            yield return new WaitForSeconds(0.001f);
        }
    }

    
}
