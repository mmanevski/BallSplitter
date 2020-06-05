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
        GameManager.rewquestNewLevel.AddListener(OnRequestNewLevel);
        GameManager.gameOver.AddListener(OnGameOver);
    }

    private void OnRequestNewLevel()
    {
        levelCompletePanel.Toggle(false);
        levelFailedPanel.Toggle(false);
    }

    private void OnGameOver(bool isWin)
    {
        if (isWin)
            levelCompletePanel.Toggle(true);
        else
            levelFailedPanel.Toggle(true);
        
    }
    
    public void OnDestroy()
    {
        GameManager.gameOver.RemoveListener(OnGameOver);
        GameManager.rewquestNewLevel.RemoveListener(OnRequestNewLevel);
    }

}
