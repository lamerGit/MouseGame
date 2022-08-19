using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스테이지 번호 Enum
public enum StageEnum
{
    Stage_01=0,
    Main,
    GameOver
}
//무기 번호 Enum
public enum WeaponEnum
{
    //string[] ButtonName = new string[11] { "PW", "BW", "TW","THW","CW","SW","BLW","CSW","CAW","RW","G" };
    BoardWeapon=0,
    TargetWeapon,
    ThunderWeapon,
    ChargeWeapon,
    BloodWeapon,
    ClickStateWeapon,
    ChaseWeapon,
    RotateWeapon,
    NomalWeapon


}
