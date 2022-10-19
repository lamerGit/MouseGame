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


    //���ӿ��������� ������ ��ư�� �Ҵ����� �Լ���
    public void Restart()
    {
        SceneManager.LoadScene((int)StageEnum.Stage_01);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene((int)StageEnum.Main);
    }
}
