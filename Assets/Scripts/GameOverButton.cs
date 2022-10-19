using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }


    //게임오버됬을때 나오는 버튼에 할당해줄 함수들
    public void Restart()
    {
        SceneManager.LoadScene((int)StageEnum.Stage_01);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene((int)StageEnum.Main);
    }
}
