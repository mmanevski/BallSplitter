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
}
