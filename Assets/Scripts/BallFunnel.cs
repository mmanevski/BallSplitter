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
    public bool isFull = false;
    public TextMeshPro ballCounter;
    public int funnelCount = 3;
    public static FunnelFull funnelFull  = new FunnelFull();
    public static AnnounceFunnel announceFunnel  = new AnnounceFunnel();

    private void Start()
    {
        announceFunnel.Invoke(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            if (isFull)
                return;
            
            funnelCount--;
            if (funnelCount == 0)
            {
                isFull = true;
                funnelFull.Invoke(this);
            }

            ballCounter.text = funnelCount.ToString();

        }
    }
}
