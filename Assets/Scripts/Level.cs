using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<BallSpawnPoint> ballSpawnPoints = new List<BallSpawnPoint>();
    public List<BallSplitter> ballSplitters = new List<BallSplitter>();
    public List<BallFunnel> ballFunnels = new List<BallFunnel>();

    public void Init()
    {
        SpawnBalls();
    }

    private void SpawnBalls()
    {
        for (int i = 0; i < ballSpawnPoints.Count; i++)
        {
            ballSpawnPoints[i].Init();
        }
    }

    public bool CheckFunnelsFull()
    {
        for (int i = 0; i < ballFunnels.Count; i++)
        {
            if (!ballFunnels[i].IsFull())
            {
                return false;
            }
        }

        return true;
    }
}
