using System.Collections;
using System.Collections.Generic;
using GameData;
using TMPro;
using UnityEngine;
using Zenject;

public class BallCombiner : MonoBehaviour
{
    [Inject] private GameParameters gameParameters;
    public TextMeshPro ballCounter;
    public GameObject counterBkg;
    public int ballsNeeded = 5;
    public static RequestSingleSpawn requestSingleSpawn = new RequestSingleSpawn();

    private bool isFull;
    
    private void Start()
    {
        ballCounter.text = ballsNeeded.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            other.GetComponent<BallController>().Despawn();
            BumpTextBkg();
            ballsNeeded--;
            if (!isFull)
            { 
                ballCounter.text = ballsNeeded.ToString();
            }
            if (ballsNeeded == 0)
            {
                isFull = true;
                requestSingleSpawn.Invoke(transform.localPosition, gameParameters.maxBallScale, true, GameManager.currentLevel.transform);
                gameObject.SetActive(false);
            }

        }
    }
    
    private void BumpTextBkg()
    {
        if (counterBkg.gameObject.LeanIsTweening())
            return;
        
        float _scaleAdd = 0.2f;
        float _bumpTime = 0.1f;
        Vector3 _startScale = counterBkg.transform.localScale;
        Vector3 _scaleTo = new Vector3(_startScale.x +_scaleAdd, _startScale.x +_scaleAdd, 1f);
        LeanTween.scale(counterBkg, _scaleTo, _bumpTime).setEaseInBounce().setOnComplete(
            () => { LeanTween.scale(counterBkg, _startScale, _bumpTime);});
    }
}
