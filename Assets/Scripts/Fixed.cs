using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetFixed();
    }

    void SetFixed()
    {
        int setWidth = 1920;
        int setHeight=1080;

        Screen.SetResolution(setWidth, setHeight, true);
    }
}
