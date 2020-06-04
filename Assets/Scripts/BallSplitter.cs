using System.Collections;
using System.Collections.Generic;
using GameData;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class RequestBalls : UnityEvent<int, SpawnArea>
{
}
public class BallSplitter : MonoBehaviour
{
    public TextMeshPro multiplierText;
    
    public int ballsToSplit = 30;
    public SpawnArea spawnArea;

    public static RequestBalls requestBalls = new RequestBalls();

    private void Start()
    {
        multiplierText.text = ballsToSplit.ToString() + "x";
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            Vector3 _pos = other.transform.position;
            
            Destroy(other.gameObject);
            requestBalls.Invoke(ballsToSplit, spawnArea);


            GetComponent<Collider>().enabled = false;

        }
    }
}
