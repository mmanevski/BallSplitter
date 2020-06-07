using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class LevelLabel : MonoBehaviour
{
    private TextMeshProUGUI levelLabel;
    void Awake()
    {
        levelLabel = GetComponent<TextMeshProUGUI>();
        LevelManager.levelLoaded.AddListener(OnLevelLoaded);
    }

    private void OnLevelLoaded(Level level)
    {
        levelLabel.text = "Level " + Regex.Replace(level.gameObject.name, "[^0-9]", "");
        
    }

    void OnDestroy()
    {
        LevelManager.levelLoaded.RemoveListener(OnLevelLoaded);
    }
}
