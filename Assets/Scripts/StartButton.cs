using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StartButton : MonoBehaviour
{
    public void Stage1()
    {
        SceneManager.LoadScene(0);
    }
}
