using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//세이브용 클래스
[Serializable]
public class SaveData 
{
    public int HealthLevel;
    public int AttackLevel;
    public int ExpLevel;
    public int Token;
    public int[] WeaponName;
    public int[] WeaponDamage;
}
