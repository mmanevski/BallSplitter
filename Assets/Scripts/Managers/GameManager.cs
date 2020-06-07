using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class GameOver : UnityEvent<bool>
{
}
public class UpdateScore : UnityEvent<int>
{
}
public class RequestNewlevel : UnityEvent<int>
{
}

public class GameManager : MonoBehaviour
{
    
    public static GameState gameState;
    public static Level currentLevel;
    
    [SerializeField]
    private int totalBalls = 0;
    private int score = 0;
    private List<BallFunnel> ballFunnels = new List<BallFunnel>();
    
    private LevelDefine currentLevelDefine;

    public static GameOver gameOver = new GameOver();
    public static UpdateScore updateScore = new UpdateScore();
    public static RequestNewlevel requestNewLevel = new RequestNewlevel();

    public void Awake()
    {
        AddListeners();
    }

    private void OnLevelLoaded(Level level)
    {
        currentLevel = level;
        gameState = GameState.Playing;
    }

    public void Start()
    {
        requestNewLevel.Invoke(-1);
        
    }

    private void OnRequestLevelReset()
    {
        ResetGame();
    }

    private void OnRequestNextLevel()
    {
        ResetGame();
        requestNewLevel.Invoke(-1);
    }

    private void ResetGame()
    {
        totalBalls = 0;
        score = 0;
        updateScore.Invoke(score);
    }

    private void OnAnnounceBallDespawned()
    {
        totalBalls--;
        if (totalBalls == 0)
        {
            CheckGameOver();
        }
    }   
    
    private void OnAnnounceBallInFunnel()
    {
        score++;
        updateScore.Invoke(score);
    }

    private void CheckGameOver()
    {
        bool isWin = currentLevel.CheckFunnelsFull();
        gameOver.Invoke(isWin);
        gameState = GameState.GameOver;
    }

    private void OnAnnounceBallSpawned()
    {
        totalBalls++;
    }

    public void OnDestroy()
    {
        RemoveListeners();

    }

    private void AddListeners()
    {
        BallFunnel.announceBallInFunnel.AddListener(OnAnnounceBallInFunnel);
        BallController.announceSpawned.AddListener(OnAnnounceBallSpawned);
        BallController.announceBallDespawned.AddListener(OnAnnounceBallDespawned);
        LevelCompletePanel.requestNextLevel.AddListener(OnRequestNextLevel);
        LevelFailedPanel.requestLevelReset.AddListener(OnRequestLevelReset);
        LevelManager.levelLoaded.AddListener(OnLevelLoaded);
    }



    private void RemoveListeners()
    {
        BallFunnel.announceBallInFunnel.RemoveListener(OnAnnounceBallInFunnel);
        BallController.announceSpawned.RemoveListener(OnAnnounceBallSpawned);
        BallController.announceBallDespawned.RemoveListener(OnAnnounceBallDespawned);
        LevelCompletePanel.requestNextLevel.RemoveListener(OnRequestNextLevel);
        LevelFailedPanel.requestLevelReset.RemoveListener(OnRequestLevelReset);
        LevelManager.levelLoaded.RemoveListener(OnLevelLoaded);
        ResetButton.requestLevelReset.RemoveListener(OnRequestLevelReset);
    }
}
