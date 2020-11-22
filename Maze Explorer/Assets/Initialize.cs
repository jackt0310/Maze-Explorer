using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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
