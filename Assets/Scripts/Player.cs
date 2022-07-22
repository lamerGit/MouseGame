using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Image hpimage;
    public float maxHp;
    public float currentHp;

    public Image ExpImage;
    private float maxExp=50.0f;
    private float currentExp=0.0f;

    //public Button SkilButton1 = null;
    public GameObject ButtonGruop = null;
    LevelButton[] LevelButton;
    //string[] ButtonName = new string[11] { "PW", "BW", "TW","THW","CW","SW","BLW","CSW","CAW","RW","G" };
    public Sprite[] ButtonSprite;
    private bool[] ButtonBool=new bool[11] {false,false,false,false, false, false, false, false ,false,false,true};

    private int Level = 0;
    private List<int> RandomChoice = new List<int>();
    
    public GameObject LevelButtonEx=null;


    bool isLive = true;

    private float CURRENTHP
    {
        get { return currentHp; }
        set {
            currentHp = Mathf.Clamp(value, 0, maxHp);
            hpimage.fillAmount = currentHp / maxHp;
            if(currentHp<1)
            {
                
                if (isLive)
                {
                    isLive = false;
                    GameManager.INSTANCE.SaveDamges();
                    SceneManager.LoadScene((int)StageEnum.GameOver);
                }
            }

        }
    }
    public bool[] BUTTONBOOL
    {
        get { return ButtonBool; }
        set { ButtonBool = value; }
    }
    public float CURRENTEXP
    {
        set { currentExp = value;
            //Debug.Log(currentExp);
            ExpImage.fillAmount = currentExp / maxExp;
            LevelUp();
        }
        get { return currentExp; }
    }
    private void Awake()
    {
        LevelButton = new LevelButton[ButtonGruop.transform.childCount];
        for(int i=0; i< ButtonGruop.transform.childCount; i++)
        {
            LevelButton[i] = ButtonGruop.transform.GetChild(i).GetComponent<LevelButton>();
        }
        
        
       // LevelButton1=SkilButton1.GetComponent<LevelButton>();
    }
    private void Start()
    {
        maxHp = maxHp + (maxHp*(GameManager.INSTANCE.HEALTHLEVEL*0.5f));
        currentHp = maxHp;
        CURRENTEXP = 0;
        //Debug.Log(currentHp);
    }

    void LevelUp()
    {
        if (currentExp > maxExp)
        {
            Level++;
            // Debug.Log("·¹º§¾÷");
            currentExp = 0.0f;
            maxExp *= 1.5f;
            ButtonGruop.gameObject.SetActive(true);
            LevelButtonEx.SetActive(true);
            if (Level < ButtonSprite.Length - 3)
            {
                while (RandomChoice.Count < 3)
                {
                    int R = Random.Range(0, 10);
                    if (ButtonBool[R])
                    {
                        R = -1;
                    }
                    for (int i = 0; i < RandomChoice.Count; i++)
                    {
                        if (RandomChoice[i] == R)
                        {
                            R = -1;
                        }
                    }

                    if (R != -1)
                    {
                        RandomChoice.Add(R);
                    }
                }
            }
            else if (Level > ButtonSprite.Length - 4)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (ButtonBool[i] == false)
                    {
                        RandomChoice.Add(i);
                    }
                }
                while (RandomChoice.Count < 3)
                {
                    RandomChoice.Add(ButtonSprite.Length - 1);
                }
            }
            //Debug.Log(RandomChoice.Count);
            //LevelButton1.BUTTONNUMBER = 0;
            //LevelButton1.t.text = "PW";
            for (int i = 0; i < ButtonGruop.transform.childCount; i++)
            {

                LevelButton[i].BUTTONNUMBER = RandomChoice[i];
                LevelButton[i].GetComponent<Image>().sprite = ButtonSprite[RandomChoice[i]];
            }
            RandomChoice.Clear();
            Time.timeScale = 0f;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            CURRENTHP -= 1.0f;
            //Debug.Log(CURRENTHP);
        }
    }

}
