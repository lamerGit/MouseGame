using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //플레이어 스크립트

    public Image hpimage;
    public float maxHp;
    public float currentHp;

    public Image ExpImage;
    private float maxExp=70.0f;
    private float currentExp=0.0f;

    public GameObject ButtonGruop = null;
    LevelButton[] LevelButton;
    public Sprite[] ButtonSprite;
    private bool[] ButtonBool=new bool[11] {false,false,false,false, false, false, false, false ,false,false,true};

    private int Level = 0;
    private List<int> RandomChoice = new List<int>();
    
    public GameObject LevelButtonEx=null;


    bool isLive = true;

    Animator anim;

    public bool ISLIVE //생존확인용 프로퍼티
    {
        get { return isLive; }
    }
    public float CURRENTHP //체력확인용 프로퍼티 
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
                    anim.SetTrigger("Die");
                    StartCoroutine(Die());
                    
                }
            }

        }
    }

    IEnumerator Die() // 죽게되면 1.5초뒤 게임오버화면으로 이동
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene((int)StageEnum.GameOver);
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
    private void Awake() //시작할때 레벨업했을때 나타나는 버튼을 미리 할당
    {
        anim = GetComponent<Animator>();
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

        //레벨업시 현재가지고 있는 무기를 체크하고 랜덤하게 무기를 표시해준다.
        if (currentExp > maxExp && isLive)
        {
            Level++;
            // Debug.Log("레벨업");
            currentExp = 0.0f;
            maxExp *= 1.6f; // 레벨업시 다음레벨을 위한 경험치흭득량을 높힌다.
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

            for (int i = 0; i < ButtonGruop.transform.childCount; i++)
            {

                LevelButton[i].BUTTONNUMBER = RandomChoice[i];
                LevelButton[i].GetComponent<Image>().sprite = ButtonSprite[RandomChoice[i]];
            }
            RandomChoice.Clear();
            Time.timeScale = 0f;
            Cursor.visible = true;

        }
    }
    //적들과 닿으면 데미지
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            CURRENTHP -= 1.0f;
            //Debug.Log(CURRENTHP);
        }
    }

}
