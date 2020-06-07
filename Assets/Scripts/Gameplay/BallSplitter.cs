using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SplitBalls : UnityEvent<int, BallSplitter, bool>
{
}
public class BallSplitter : MonoBehaviour
{
    public TextMeshPro multiplierText;
    public GameObject multiplierBkg;
    
    public int ballsToSplit = 30;
    public SpawnArea spawnArea;
    
    public static SplitBalls splitBalls = new SplitBalls();

    private void Start()
    {
        multiplierText.text = ballsToSplit.ToString() + "x";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            Vector3 _pos = other.transform.position;
            BallController ballSplit = other.GetComponent<BallController>();
            
            if (ballSplit.GetParentSplitter() != this)
            {
                BumpTextBkg();
                ballSplit.Despawn();
                splitBalls.Invoke(ballsToSplit, this, true);
            }

        }
    }

    private void BumpTextBkg()
    {
        if (multiplierBkg.gameObject.LeanIsTweening())
            return;
        
        float _scaleAdd = 0.2f;
        float _bumpTime = 0.1f;
        Vector3 _startScale = multiplierBkg.transform.localScale;
        Vector3 _scaleTo = new Vector3(_startScale.x +_scaleAdd, _startScale.x +_scaleAdd, 1f);
        LeanTween.scale(multiplierBkg, _scaleTo, _bumpTime).setEaseInBounce().setOnComplete(
            () => { LeanTween.scale(multiplierBkg, _startScale, _bumpTime);});
    }

}
