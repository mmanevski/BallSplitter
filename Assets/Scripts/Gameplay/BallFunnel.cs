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
    public GameObject counterBkg;
    public int funnelCount = 3;

    public bool isGate = false;

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
            BumpTextBkg();
            funnelCount--;
            if (!isFull)
            { 
                ballCounter.text = funnelCount.ToString();
            }
            announceBallInFunnel.Invoke();
            if (funnelCount == 0)
            {
                isFull = true;
                //gameObject.SetActive(isGate);
                funnelFull.Invoke(this);
            }

        }
    }
    
    private void BumpTextBkg()
    {
        float _scaleAdd = 0.2f;
        float _bumpTime = 0.1f;
        Vector3 _startScale = counterBkg.transform.localScale;
        Vector3 _scaleTo = new Vector3(_startScale.x +_scaleAdd, _startScale.x +_scaleAdd, 1f);
        LeanTween.scale(counterBkg, _scaleTo, _bumpTime).setEaseInBounce().setOnComplete(
            () => { LeanTween.scale(counterBkg, _startScale, _bumpTime);});
    }
}
