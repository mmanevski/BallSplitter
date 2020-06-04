using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallFunnel : MonoBehaviour
{
    
    public TextMeshPro multiplierText;
    
    public int ballsToSplit = 30;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            

        }
    }
}
