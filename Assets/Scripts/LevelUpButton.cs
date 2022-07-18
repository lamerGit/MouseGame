using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;

public class LevelUpButton : MonoBehaviour
{
    public Sprite[] SlotSprites;
    public Image[] Slotimages;

    int health = 0;
    int attack = 0;
    int exp = 0;
    int ToKen = 0;
    public Text tokenText;
   
    private void Start()
    {
        if(LoadGameData())
        {
            Slotimages[0].sprite = SlotSprites[health];
            Slotimages[1].sprite = SlotSprites[attack];
            Slotimages[2].sprite = SlotSprites[exp];
            tokenText.text=$"X{ToKen}";
        }else
        {
            for(int i=0; i < Slotimages.Length; i++)
            {
                Slotimages[i].sprite = SlotSprites[0];
            }
            ToKen = 3;
            tokenText.text = $"X{ToKen}";
        }
        gameObject.SetActive(false);
        
    }
    public void ExitPanel()
    {
        gameObject.SetActive(false);
    }

    

    public bool LoadGameData()
    {
        bool result = false;
        string path = $"{Application.dataPath}/Save/";
        string fullPath = $"{path}Save.json";
        if(Directory.Exists(path) && File.Exists(fullPath))
        {
            string json=File.ReadAllText(fullPath);
            SaveData saveData=JsonUtility.FromJson<SaveData>(json);
            health = saveData.HealthLevel;
            attack = saveData.AttackLevel;
            exp = saveData.ExpLevel;
            ToKen = saveData.Token;
            result = true;
        }

        return result;
    }

    public void SaveHealthData()
    {

        if (ToKen > 0)
        {
            ToKen--;
            health++;
            health = Mathf.Clamp(health, 0, 3);
            SaveData saveData = new SaveData();
            saveData.HealthLevel = health;
            saveData.AttackLevel = attack;
            saveData.ExpLevel = exp;
            saveData.Token = ToKen;
            Slotimages[0].sprite = SlotSprites[health];
            tokenText.text = $"X{ToKen}";
            string json = JsonUtility.ToJson(saveData);

            string path = $"{Application.dataPath}/Save/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = $"{path}Save.json";
            File.WriteAllText(fullPath, json);
        }


    }

    public void SaveAttackData()
    {

        if (ToKen > 0)
        {
            ToKen--;
            attack++;
            attack = Mathf.Clamp(attack, 0, 3);
            SaveData saveData = new SaveData();
            saveData.HealthLevel = health;
            saveData.AttackLevel = attack;
            saveData.ExpLevel = exp;
            saveData.Token = ToKen;
            Slotimages[1].sprite = SlotSprites[attack];
            tokenText.text = $"X{ToKen}";
            string json = JsonUtility.ToJson(saveData);

            string path = $"{Application.dataPath}/Save/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = $"{path}Save.json";
            File.WriteAllText(fullPath, json);

        }

    }

    public void SaveExpData()
    {

        if (ToKen > 0)
        {
            ToKen--;
            exp++;
            exp = Mathf.Clamp(exp, 0, 3);
            SaveData saveData = new SaveData();
            saveData.HealthLevel = health;
            saveData.AttackLevel = attack;
            saveData.ExpLevel = exp;
            saveData.Token = ToKen;
            Slotimages[2].sprite = SlotSprites[exp];
            tokenText.text = $"X{ToKen}";
            string json = JsonUtility.ToJson(saveData);

            string path = $"{Application.dataPath}/Save/";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fullPath = $"{path}Save.json";
            File.WriteAllText(fullPath, json);
        }


    }

    public void ResetButton()
    {
        SaveData saveData = new SaveData();
        health = 0;
        attack = 0;
        exp = 0;
        ToKen = 3;
        saveData.HealthLevel = health;
        saveData.AttackLevel = attack;
        saveData.ExpLevel = exp;
        saveData.Token = ToKen;
        Slotimages[0].sprite = SlotSprites[health];
        Slotimages[1].sprite = SlotSprites[attack];
        Slotimages[2].sprite = SlotSprites[exp];
        tokenText.text=$"X{ToKen}";
        string json = JsonUtility.ToJson(saveData);

        string path = $"{Application.dataPath}/Save/";

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string fullPath = $"{path}Save.json";
        File.WriteAllText(fullPath, json);
        
    }

}
