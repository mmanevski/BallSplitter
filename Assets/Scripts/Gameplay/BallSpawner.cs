using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    
    public GameObject ballPrefab;

    private float currentBallScale = 0.5f;
    private Level currentLevel;

    [Inject] private GameParameters gameParameters;
    private void Awake()
    {
        BallSplitter.splitBalls.AddListener(OnSplitBalls);
        BallSpawnPoint.requestSingleSpawn.AddListener(OnRequestSingleSpawn);
        BallPortal.requestSingleSpawn.AddListener(OnRequestSingleSpawn);
        BallCombiner.requestSingleSpawn.AddListener(OnRequestSingleSpawn);
        LevelManager.levelLoaded.AddListener(OnLevelLoaded);
    }

    private void OnLevelLoaded(Level level)
    {
        currentLevel = level;
    }

    private void OnRequestSingleSpawn(Vector3 pos, float scaleFactor, bool activate = true, Transform parent = null)
    {
        if (parent == null)
        {
           parent = currentLevel.transform; 
        }
        
        BallController _newBall = Instantiate(ballPrefab, pos, Quaternion.identity, parent).GetComponent<BallController>();
        _newBall.Init(pos, scaleFactor, activate);
    }

    private void OnSplitBalls(int numOfBalls, BallSplitter parentSplitter, bool activate)
    {
        float _newScaleFactor = numOfBalls <= 4 ? currentBallScale * 0.8f : currentBallScale * 0.5f;
        _newScaleFactor = Mathf.Clamp(_newScaleFactor, gameParameters.minBallScale, gameParameters.maxBallScale);
        currentBallScale = _newScaleFactor;

        StartCoroutine(SpawnBalls(numOfBalls, parentSplitter, _newScaleFactor, activate));
    }

    private IEnumerator SpawnBalls(int numOfBalls, BallSplitter parentSplitter, float _scaleFactor, bool activate = true)
    {
        for (int i = 0; i < numOfBalls; i++)
        {
            Vector3 spawnPos = new Vector3();
            SpawnArea spawnArea = parentSplitter.spawnArea;

            float spawnPosZLBound = parentSplitter.transform.localPosition.z - spawnArea.rightBoundry;
            float spawnPosZRBound = parentSplitter.transform.localPosition.z + spawnArea.rightBoundry;

            float spawnPosZ = Random.Range( spawnPosZLBound, spawnPosZRBound);

            spawnPos = new Vector3(parentSplitter.transform.localPosition.x, parentSplitter.transform.localPosition.y, spawnPosZ);

            Transform _parent = currentLevel.transform;
                
            BallController _newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity, _parent).GetComponent<BallController>();
            _newBall.Init(spawnPos, _scaleFactor, activate, parentSplitter);
            yield return new WaitForSeconds(0.05f);
        }
    }
    
    void OnDestroy()
    {
        BallSplitter.splitBalls.RemoveListener(OnSplitBalls);
        BallSpawnPoint.requestSingleSpawn.RemoveListener(OnRequestSingleSpawn);
        BallPortal.requestSingleSpawn.RemoveListener(OnRequestSingleSpawn);
        BallCombiner.requestSingleSpawn.RemoveListener(OnRequestSingleSpawn);
        LevelManager.levelLoaded.RemoveListener(OnLevelLoaded);
    }
}
