using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelCompletePanel levelCompletePanel;
    public LevelFailedPanel levelFailedPanel;
    
    public void Awake()
    {
        GameManager.gameOver.AddListener(OnGameOver);
    }

    private void OnGameOver(bool isWin)
    {
        if (isWin)
            levelCompletePanel.Open();
        else
            levelFailedPanel.Open();
        
    }
    
    public void OnDestroy()
    {
        GameManager.gameOver.AddListener(OnGameOver);
    }

}
