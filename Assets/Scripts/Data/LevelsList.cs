using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "LevelsList", menuName = "Data/LevelsList")]
public class LevelsList : ScriptableObject
{
    public List<LevelDefine> allLevels;

    public LevelDefine GetLevel(int index)
    {
        return allLevels[index];
    }
}
