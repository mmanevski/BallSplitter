using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public LevelCompletePanel levelCompletePanel;
    public LevelFailedPanel levelFailedPanel;
    public LevelSelectionPanel LevelSelectionPanel;
    
    public void Awake()
    {
        GameManager.requestNewLevel.AddListener(OnRequestNewLevel);
        GameManager.gameOver.AddListener(OnGameOver);
        DebugButton.requestDebugMenu.AddListener(OnRequestDebug);
    }

    private void OnRequestDebug()
    {
        GameManager.gameState = GameState.Loading;
        LevelSelectionPanel.Toggle(true);
    }

    private void OnRequestNewLevel(int levelNum)
    {
        LevelSelectionPanel.Toggle(false);
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
        GameManager.requestNewLevel.RemoveListener(OnRequestNewLevel);
        DebugButton.requestDebugMenu.RemoveListener(OnRequestDebug);
    }

}
