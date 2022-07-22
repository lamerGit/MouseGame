using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DamageRankPanel : MonoBehaviour
{
    int[] WeaponRanks;
    int[] WeaponDamages;

    public GameObject weaponDamage_UI;
    private GameObject grid;
    public Sprite[] WeaponSprite;

    private void Awake()
    {
        grid = transform.Find("Grid").gameObject;
    }

    private void Start()
    {
        LoadLevelData();
        for(int i=0; i<WeaponSprite.Length; i++)
        {
            if (WeaponDamages[i]==0)
            {
                break;
            }
            GameObject w = Instantiate(weaponDamage_UI);
            w.GetComponent<WeaponDamage_UI>().SetDamageUI(WeaponSprite[WeaponRanks[i]], WeaponDamages[i]);
            w.transform.SetParent(grid.transform);
        }

    }
    void LoadLevelData()
    {

        string path = $"{Application.dataPath}/Save/";
        string fullPath = $"{path}Save.json";
        if (Directory.Exists(path) && File.Exists(fullPath))
        {
            string json = File.ReadAllText(fullPath);
            SaveData saveData = JsonUtility.FromJson<SaveData>(json);
            WeaponRanks = saveData.WeaponName;
            WeaponDamages = saveData.WeaponDamage;

        }


    }
}
