using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    //시작버튼에 할당해줄 스크립트
    public void Stage1()
    {
        SceneManager.LoadScene((int)StageEnum.Stage_01);
    }
}
