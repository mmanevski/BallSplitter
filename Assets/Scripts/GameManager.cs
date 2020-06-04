using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehavior<GameManager>
{
    private int funnelsToFIll = 1;
    
    [SerializeField]
    private int totalBalls = 0;
    private List<BallFunnel> ballFunnels = new List<BallFunnel>();
    
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
