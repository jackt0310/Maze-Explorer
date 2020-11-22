using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryManagement
{
    private static bool[] unlockedMusic;
    private static int currentLevel;
    private static int songsUnlocked;
    private static string difficulty;
    private static int arrowAmt;
    private static int grenadeAmt;

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

    public static string Difficulty
    {
        get
        {
            return difficulty;
        }
        set
        {
            difficulty = value;
        }
    }

    public static int ArrowAmt
    {
        get
        {
            return arrowAmt;
        }
        set
        {
            arrowAmt = value;
        }
    }

    public static int GrenadeAmt
    {
        get
        {
            return grenadeAmt;
        }
        set
        {
            grenadeAmt = value;
        }
    }
}