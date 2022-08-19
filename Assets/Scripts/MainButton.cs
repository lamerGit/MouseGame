using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour
{
    //메인화면 버튼 할당용 함수들

    public GameObject LevelUpPanel;
    public void Stage1()
    {
        SceneManager.LoadScene((int)StageEnum.Stage_01);
    }

    public void OnLevelUpPanel()
    {
        LevelUpPanel.SetActive(true);
    }
    
}
