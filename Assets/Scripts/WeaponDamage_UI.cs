using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDamage_UI : MonoBehaviour
{
    Image WeaponImage;
    Text WeaponDamageText;
    Image WeaponGauge;

    float MaxDamage = 5000.0f;
    
    public void SetDamageUI(Sprite WeaponSprite,float Damage)
    {
        WeaponImage = transform.Find("WeaponImage").GetComponent<Image>();
        WeaponDamageText = transform.Find("DamageText").GetComponent<Text>();
        WeaponGauge = transform.Find("DamageGauge").GetComponent<Image>();

        WeaponImage.sprite = WeaponSprite;
        WeaponDamageText.text = $"{Damage:N0}";
        WeaponGauge.fillAmount = Damage / MaxDamage;

    }

    
}
