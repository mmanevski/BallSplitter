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
public class AnnounceFunnel : UnityEvent<BallFunnel>
{
}
public class BallFunnel : MonoBehaviour
{
    public TextMeshPro ballCounter;
    public int funnelCount = 3;
    
    public static FunnelFull funnelFull  = new FunnelFull();
    public static AnnounceFunnel announceFunnel  = new AnnounceFunnel();
    public static UnityEvent announceBallInFunnel  = new UnityEvent();
    
    private bool isFull = false;

    private void Start()
    {
        announceFunnel.Invoke(this);
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
            if (isFull)
                return;
            
            Destroy(other.gameObject);
            funnelCount--;
            announceBallInFunnel.Invoke();
            if (funnelCount == 0)
            {
                isFull = true;
                funnelFull.Invoke(this);
            }

            ballCounter.text = funnelCount.ToString();

        }
    }
}
