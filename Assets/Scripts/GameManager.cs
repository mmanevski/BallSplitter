using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    
    public static GameOver gameOver = new GameOver();
    public static UpdateScore updateScore = new UpdateScore();

    public void Awake()
    {
        BallFunnel.announceFunnel.AddListener(OnAnnounceFunnel);
        BallFunnel.announceBallInFunnel.AddListener(OnAnnounceBallInFunnel);
        BallController.announceSpawned.AddListener(OnAnnounceBallSpawned);
        BallController.announceBallDespawned.AddListener(OnAnnounceBallDespawned);
        LevelCompletePanel.requestNextLevel.AddListener(OnRequestNextLevel);
        LevelFailedPanel.requestLevelReset.AddListener(OnRequestLevelReset);
        
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
        for (int i = 0; i < ballFunnels.Count; i++)
        {
            if (!ballFunnels[i].IsFull())
            {
                gameOver.Invoke(false);
                Debug.Log("Level failed!");
                return;
            }
        }
        gameOver.Invoke(true);
        Debug.Log("Level won!");
        
    }

    private void OnAnnounceBallSpawned()
    {
        totalBalls++;
    }

    private void OnAnnounceFunnel( BallFunnel funnel)
    {
        ballFunnels.Add(funnel);
    }
    
    public void OnDestroy()
    {
        BallFunnel.announceFunnel.RemoveListener(OnAnnounceFunnel);
        BallFunnel.announceBallInFunnel.RemoveListener(OnAnnounceBallInFunnel);
        BallController.announceSpawned.RemoveListener(OnAnnounceBallSpawned);
        BallController.announceBallDespawned.RemoveListener(OnAnnounceBallDespawned);
        LevelCompletePanel.requestNextLevel.RemoveListener(OnRequestNextLevel);
        LevelFailedPanel.requestLevelReset.RemoveListener(OnRequestLevelReset);
        
    }
}
