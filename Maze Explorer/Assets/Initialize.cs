using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    public GameObject canvas;
    public GameObject mainMenu;
    public GameObject difficultyMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("/Canvas");
        mainMenu = canvas.transform.Find("MainMenu").gameObject;
        difficultyMenu = canvas.transform.Find("DifficultyMenu").gameObject;
        difficultyMenu.SetActive(false);
        int musicAmt = 25;
        bool[] unlockedMusic = new bool[musicAmt];
        unlockedMusic[0] = true;

        for (int i = 1; i < musicAmt; i++)
        {
            unlockedMusic[i] = false;
        }

        InventoryManagement.UnlockedMusic = unlockedMusic;
        InventoryManagement.CurrentLevel = 1;
        InventoryManagement.SongsUnlocked = 1;
    }
}
