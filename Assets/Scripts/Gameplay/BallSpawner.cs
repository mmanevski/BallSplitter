using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    
    public GameObject ballPrefab;

    private float currentBallScale = 0.5f;

    [Inject] private GameParameters gameParameters;
    private void Awake()
    {
        BallSplitter.requestBalls.AddListener(OnRequestBalls);
        BallSpawnPoint.requestSingleSpawn.AddListener(OnRequestSingleSpawn);
        BallPortal.requestSingleSpawn.AddListener(OnRequestSingleSpawn);
    }

    private void OnRequestSingleSpawn(Vector3 pos, float scaleFactor, bool activate = true)
    {
        
        Debug.Log("SpawnSingle");
        BallController _newBall = Instantiate(ballPrefab, pos, Quaternion.identity, PlayAreaController.Instance.transform).GetComponent<BallController>();
        _newBall.Init(pos, scaleFactor, activate);
    }

    private void OnRequestBalls(int numOfBalls, SpawnArea spawnArea, bool activate)
    {
        float _newScaleFactor = numOfBalls <= 4 ? currentBallScale * 0.8f : currentBallScale * 0.5f;
        _newScaleFactor = Mathf.Clamp(_newScaleFactor, gameParameters.minBallScale, gameParameters.maxBallScale);
        currentBallScale = _newScaleFactor;

        StartCoroutine(SpawnBalls(numOfBalls, spawnArea, _newScaleFactor, activate));
    }

    private IEnumerator SpawnBalls(int numOfBalls, SpawnArea spawnArea, float _scaleFactor, bool activate = true)
    {
        for (int i = 0; i < numOfBalls; i++)
        {
            Vector3 spawnPos = new Vector3();

            float spawnPosZBound = spawnArea.rightBoundry;
            float spawnPosYBound = spawnArea.upperBoundry;

            float spawnPosZ = Random.Range(spawnArea.transform.position.z - spawnPosZBound, spawnArea.transform.position.z + spawnPosZBound);

            spawnPos = new Vector3(spawnArea.transform.position.x, spawnArea.transform.position.y, spawnPosZ);

            BallController _newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity, PlayAreaController.Instance.transform).GetComponent<BallController>();
            _newBall.Init(spawnPos, _scaleFactor, activate);
            yield return new WaitForSeconds(0.05f);
        }

    }
}
