using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    //���۹�ư�� �Ҵ����� ��ũ��Ʈ
    public void Stage1()
    {
        SceneManager.LoadScene((int)StageEnum.Stage_01);
    }
}
