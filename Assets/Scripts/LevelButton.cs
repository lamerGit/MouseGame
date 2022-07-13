using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelButton : MonoBehaviour ,IPointerEnterHandler
{

    public Text t = null;

    private int ButtonNumber = 0;

    public Text ButtonEx = null;
    public int BUTTONNUMBER
    {
        set { ButtonNumber = value; }
    }

    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Button");
        transform.SetSiblingIndex(100);
    }

    public void SelectWeapon()
    {
        if(ButtonNumber ==0)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[0] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().PUSHW = true;
        }else if (ButtonNumber == 1)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[1] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BOARDW = true;
        }else if (ButtonNumber ==2)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[2] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().TARGETW = true;
        }
        else if (ButtonNumber == 3)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[3] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().THUNDERW = true;
        }
        else if (ButtonNumber == 4)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[4] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().CHARGEW = true;
        }
        else if (ButtonNumber == 5)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[5] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().SLOOWW = true;
        }
        else if (ButtonNumber == 6)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[6] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().BLOODW = true;
        }
        else if (ButtonNumber == 7)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[7] = true;
            GameManager.INSTANCE.MOUSE.GetComponent<Mouse>().CLICKSTATEW = true;
        }
        else if (ButtonNumber == 8)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[8] = true;
            if (GameManager.INSTANCE.CHASEWEAPONQUEUE.Count > 0)
            {
                GameObject c = GameManager.INSTANCE.CHASEWEAPONQUEUE.Dequeue();
                c.transform.position = GameManager.INSTANCE.PLAYER.transform.position;
                c.SetActive(true);
            }
        }
        else if (ButtonNumber == 9)
        {
            GameManager.INSTANCE.PLAYER.GetComponent<Player>().BUTTONBOOL[9] = true;
            //Debug.Log(GameManager.INSTANCE.ROTATEWEAPON.Length);
            for (int i = 0; i < 2; i++)
            {
                GameObject r=GameManager.INSTANCE.ROTATEWEAPON[i];
                r.SetActive(true);
            }
        }
        else if (ButtonNumber == 10)
        {
            Debug.Log("Gold");
        }

    }

    public void StartTime()
    {
        SelectWeapon();
        Time.timeScale = 1.0f;
        ButtonEx.text = "";
        GameManager.INSTANCE.PLAYER.GetComponent<Player>().LevelButtonEx.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("마우스");
        if (ButtonNumber == 0)
        {
            ButtonEx.text = "공격모션이 끝나면 주변 적들을 밀어냅니다.";
        }
        else if (ButtonNumber == 1)
        {
            ButtonEx.text = "공격준비상태일때 장판이생기며 공격시 활성화됩니다.";
        }
        else if (ButtonNumber == 2)
        {
            ButtonEx.text = "공격모션이 끝나면 주변 3명의 적들을 공격합니다";
        }
        else if (ButtonNumber == 3)
        {
            ButtonEx.text = "공격모션이 끝나면 주변적에게 번개가 날라갑니다.";
        }
        else if (ButtonNumber == 4)
        {
            ButtonEx.text = "공격준비상태일때 에너지를 모으며 에너지를 전부모은 상태에서 공격모션이 끝나면 강력한 공격을 합니다.";
        }
        else if (ButtonNumber == 5)
        {
            ButtonEx.text = "공격준비상태일때 선택된 적들이 느려집니다.";
        }
        else if (ButtonNumber == 6)
        {
            ButtonEx.text = "공격당한 적들이 출혈상태가 됩니다.";
        }
        else if (ButtonNumber == 7)
        {
            ButtonEx.text = "공격준비상태일때 주변의 데미지를 줍니다";
        }
        else if (ButtonNumber == 8)
        {
            ButtonEx.text = "적을 쫓아다니면서 공격하는 물체를 소환합니다.";
        }
        else if (ButtonNumber == 9)
        {
            ButtonEx.text = "플레이어 주변을 빙글빙글 돌아가는 물체를 소환합니다.";
        }
        else if (ButtonNumber == 10)
        {
            ButtonEx.text = "Gold를 획득합니다.";
        }
    }
}
