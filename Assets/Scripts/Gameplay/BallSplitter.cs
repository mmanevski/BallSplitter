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
                ballSplit.Despawn();
                splitBalls.Invoke(ballsToSplit, this, true);
            }

        }
    }

}
