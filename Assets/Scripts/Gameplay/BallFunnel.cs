using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class FunnelFull : UnityEvent<BallFunnel>
{
}

public class BallFunnel : MonoBehaviour
{
    public TextMeshPro ballCounter;
    public int funnelCount = 3;
    
    public static FunnelFull funnelFull  = new FunnelFull();
    public static UnityEvent announceBallInFunnel  = new UnityEvent();
    
    private bool isFull = false;

    private void Start()
    {
        ballCounter.text = funnelCount.ToString();
    }

    public bool IsFull()
    {
        return isFull;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            other.GetComponent<BallController>().Despawn();
            funnelCount--;
            if (!isFull)
            { 
                ballCounter.text = funnelCount.ToString();
            }
            announceBallInFunnel.Invoke();
            if (funnelCount == 0)
            {
                isFull = true;
                funnelFull.Invoke(this);
            }

        }
    }
}
