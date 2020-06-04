using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOver : UnityEvent<bool>
{
}
public class GameManager : SingletonBehavior<GameManager>
{
    private int funnelsToFIll = 1;
    
    [SerializeField]
    private int totalBalls = 0;
    private List<BallFunnel> ballFunnels = new List<BallFunnel>();
    
    public static GameOver gameOver = new GameOver();
    
    public override void Awake()
    {
        base.Awake();
        BallFunnel.announceFunnel.AddListener(OnAnnounceFunnel);
        BallController.announceSpawned.AddListener(OnAnnounceBallSpawned);
        BallController.announceInFunnel.AddListener(OnAnnounceBallInFunnel);
        
    }

    private void OnAnnounceBallInFunnel()
    {
        totalBalls--;
        if (totalBalls == 0)
        {
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        for (int i = 0; i < ballFunnels.Count; i++)
        {
            if (!ballFunnels[i].isFull)
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
}
