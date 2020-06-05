using System;
using System.Collections;
using System.Collections.Generic;
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

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private int totalBalls = 0;
    private int score = 0;
    private List<BallFunnel> ballFunnels = new List<BallFunnel>();
    
    private LevelDefine currentLevelDefine;
    private Level currentLevel;

    public static GameOver gameOver = new GameOver();
    public static UpdateScore updateScore = new UpdateScore();
    public static UnityEvent rewquestNewLevel = new UnityEvent();

    public void Awake()
    {
        BallFunnel.announceBallInFunnel.AddListener(OnAnnounceBallInFunnel);
        BallController.announceSpawned.AddListener(OnAnnounceBallSpawned);
        BallController.announceBallDespawned.AddListener(OnAnnounceBallDespawned);
        LevelCompletePanel.requestNextLevel.AddListener(OnRequestNextLevel);
        LevelFailedPanel.requestLevelReset.AddListener(OnRequestLevelReset);
        LevelManager.levelLoaded.AddListener(OnLevelLoaded);
    }

    private void OnLevelLoaded(Level level)
    {
        currentLevel = level;
    }

    public void Start()
    {
        rewquestNewLevel.Invoke();
        
    }

    private void OnRequestLevelReset()
    {
        SceneManager.LoadScene(0);
    }

    private void OnRequestNextLevel()
    {
        Debug.Log("Move To Next Level");
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
    }

    private void OnAnnounceBallSpawned()
    {
        totalBalls++;
    }

    public void OnDestroy()
    {
        BallFunnel.announceBallInFunnel.RemoveListener(OnAnnounceBallInFunnel);
        BallController.announceSpawned.RemoveListener(OnAnnounceBallSpawned);
        BallController.announceBallDespawned.RemoveListener(OnAnnounceBallDespawned);
        LevelCompletePanel.requestNextLevel.RemoveListener(OnRequestNextLevel);
        LevelFailedPanel.requestLevelReset.RemoveListener(OnRequestLevelReset);
        LevelManager.levelLoaded.RemoveListener(OnLevelLoaded);
    }
}
