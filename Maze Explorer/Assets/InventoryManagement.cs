using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryManagement
{
    private static bool[] unlockedMusic;
    private static int currentLevel;
    private static int songsUnlocked;

    public static bool[] UnlockedMusic
    {
        get
        {
            return unlockedMusic;
        }
        set
        {
            unlockedMusic = value;
        }
    }

    public static int CurrentLevel
    {
        get
        {
            return currentLevel;
        }
        set
        {
            currentLevel = value;
        }
    }

    public static int SongsUnlocked
    {
        get
        {
            return songsUnlocked;
        }
        set
        {
            songsUnlocked = value;
        }
    }
}