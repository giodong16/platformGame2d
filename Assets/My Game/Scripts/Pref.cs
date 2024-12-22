using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public static class Pref
{
    //Consumables :tiêu hao 
    public static int Coins
    {
        set
        {
            PlayerPrefs.SetInt(GameConst.COINS, value);
        }
        get => PlayerPrefs.GetInt(GameConst.COINS, 1000);
    }
    public static int Hearts
    {
        set
        {
            PlayerPrefs.SetInt(GameConst.HEARTS, value);
        }
        get => PlayerPrefs.GetInt(GameConst.HEARTS, 3);
    }
    public static int Stones
    {
        set => PlayerPrefs.SetInt(GameConst.STONES, value);
        get => PlayerPrefs.GetInt(GameConst.STONES, 5);
    }
    public static int Arrows
    {
        set => PlayerPrefs.SetInt(GameConst.ARROWS, value);
        get => PlayerPrefs.GetInt(GameConst.ARROWS, 5);
    }

    public static int HPPotion
    {
        set => PlayerPrefs.SetInt(GameConst.HPPotion, value);
        get => PlayerPrefs.GetInt(GameConst.HPPotion, 1);
    }
    //------------------------------------------------------------------------------------------------------------------

    //Level and stars
    public static int UnlockLevel
    {
        set
        {
            int oldUnlockLevel = PlayerPrefs.GetInt(GameConst.UNLOCKEDLEVEL.ToString(), 1);
            if (oldUnlockLevel < value)
            {
                PlayerPrefs.SetInt(GameConst.UNLOCKEDLEVEL.ToString(), value);
            }
        }

        get => PlayerPrefs.GetInt(GameConst.UNLOCKEDLEVEL.ToString(), 1);
    }

    public static int CurrentLevelPlay
    {
        set
        {
            PlayerPrefs.SetInt(GameConst.CURRENTLEVEL.ToString(), value);
        }

        get => PlayerPrefs.GetInt(GameConst.CURRENTLEVEL.ToString(), 1);
    }
    public static int MaxLevel
    {
        set => PlayerPrefs.SetInt(GameConst.MAXLEVEL.ToString(), value);
        get => PlayerPrefs.GetInt(GameConst.MAXLEVEL, 5);
    }
    // stars
    public static void SaveStarsForLevel(int level, int stars = 0)
    {
        string key = "Level_" + level + "_Stars"; // Level_1_Stars
        int old = PlayerPrefs.GetInt(key);
        if(old < stars) 
            PlayerPrefs.SetInt(key, stars);
    }

    public static int GetStarsForLevel(int level)
    {
        string key = "Level_" + level + "_Stars";
        return PlayerPrefs.GetInt(key, 0);
    }

    //tính tổng số sao từ level [A,B) ví dụ 1-5 thì tính 1 đến hết 4
    public static int GetTotalStars(int A, int B)
    {
        int total = 0;
        for (int i = A; i < B; i++)
        {
            total += PlayerPrefs.GetInt("Level_" + i + "_Stars", 0);
        }
        return total;
    }

    //đặc biệt
    public static void CompleteSpecialLevel(int level)
    {
        if (IsSpecialLevel(level))
        {
            PlayerPrefs.SetInt($"SpecialLevel_{level}_Completed", 1); // Đánh dấu là hoàn thành
        }
    }

    public static bool IsSpecialLevel(int level)
    {
        int[] specialLevels = { 4 }; // Danh sách các level đặc biệt 
        return System.Array.Exists(specialLevels, l => l == level);
    }

    public static bool IsSpecialLevelCompleted(int level)
    {
        return PlayerPrefs.GetInt($"SpecialLevel_{level}_Completed", 0) == 1;
    }

    //------------------------------------------------------------------------------------------------------------------

    //AUDIO

    public static float VolumeMusic
    {
        set => PlayerPrefs.SetFloat(GameConst.VOLUMEMUSIC.ToString(), value);
        get => PlayerPrefs.GetFloat(GameConst.VOLUMEMUSIC.ToString(), 1.0f);
    }
    public static float VolumeSFX
    {
        set => PlayerPrefs.SetFloat(GameConst.VOLUMESFX.ToString(), value);
        get => PlayerPrefs.GetFloat(GameConst.VOLUMESFX.ToString(), 1.0f);
    }
    //------------------------------------------------------------------------------------------------------------------

    //SKILL
    public static int SwordSkill
    {
        set => PlayerPrefs.SetInt(GameConst.SWORD, 1);
        get => PlayerPrefs.GetInt(GameConst.SWORD, 0);
    }
    public static int BowSkill
    {
        set => PlayerPrefs.SetInt(GameConst.BOW, 1);
        get => PlayerPrefs.GetInt(GameConst.BOW, 0);
    }
    //------------------------------------------------------------------------------------------------------------------
}
