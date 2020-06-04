using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSplitter : MonoBehaviour
{
    
    public GameObject ballPrefab;
    public BoxCollider spawnArea;
    public TextMeshPro multiplierText;
    
    public int ballsToSplit = 30;

    public float minScale = 0.125f;

    private void Start()
    {
        multiplierText.text = ballsToSplit.ToString() + "x";
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 _pos = other.transform.position;
            float _scaleFactor = other.transform.localScale.x;

            _scaleFactor = Mathf.Clamp(_scaleFactor * 0.75f, 0.125f, 0.5f);
            
            Destroy(other.gameObject);

            StartCoroutine(SpawnBall(_pos, _scaleFactor));


            GetComponent<Collider>().enabled = false;

        }
    }

    private IEnumerator SpawnBall(Vector3 _pos, float _scaleFactor)
    {
        for (int i = 0; i < ballsToSplit; i++)
        {
            bool canSpawnHere = false;

            Vector3 spawnPos = new Vector3();

            float spawnPosZBound = _pos.z + spawnArea.size.z*0.5f;
            float spawnPosYBound = _pos.y + spawnArea.size.y*0.5f;

            float spawnPosZ = Random.Range(-spawnPosZBound, spawnPosZBound);
            spawnPos = new Vector3(1f, spawnArea.transform.position.y, spawnPosZ);

            BallController _newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity, PlayAreaController.Instance.transform).GetComponent<BallController>();
            _newBall.Init(spawnPos, _scaleFactor);
            yield return new WaitForSeconds(0.05f);
        }

    }
}
