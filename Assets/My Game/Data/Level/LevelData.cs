using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData",menuName = "Level/LevelData",order =1)]
public class LevelData : ScriptableObject
{
    public int levelNumber;
    public int levelHearts;
    public string[] tasksAndRequirements;

}
