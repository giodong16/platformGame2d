using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    Starting,
    Playing,
    Pause,
    Gameover
}

public static class GameConst
{
    public static string UNLOCKEDLEVEL = "UnlockedLevel";
    public static string CURRENTLEVEL = "CurrentLevel";
    public static string MAXLEVEL = "MaxLevel";

    public static string VOLUMESFX = "VolumeSFX";
    public static string VOLUMEMUSIC = "VolumeMusic";

    public static string SWORD = "Sword";
    public static string BOW = "Bow";

    public static string COINS = "coins";
    public static string HEARTS = "hearts";
    public static string STONES = "Stones";
    public static string ARROWS = "Arrows";
    public static string HPPotion = "HpPotion";
    
    
}
