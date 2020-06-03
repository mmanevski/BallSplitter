using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSplitter : MonoBehaviour
{
    
    public GameObject ballPrefab;
    public int ballsToSplit = 2;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 _pos = other.transform.position;
            
            Destroy(other.gameObject);

            for (int i = 0; i < ballsToSplit; i++)
            {
                Vector3 _newPos = new Vector3(_pos.x, _pos.y, _pos.z + i * 0.01f);
                
                BallController _newBall = Instantiate(ballPrefab, PlayAreaController.Instance.transform).GetComponent<BallController>();
                _newBall.Init(_newPos);
            }

            GetComponent<Collider>().enabled = false;

        }
    }
}
