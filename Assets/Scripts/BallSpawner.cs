using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BallSpawner : MonoBehaviour
{
    
    public GameObject ballPrefab;
    
    public float minScale = 0.125f;

    private void Awake()
    {
        BallSplitter.requestBalls.AddListener(OnRequestBalls);
        BallSpawnPoint.requestSingleSpawn.AddListener(OnRequestSingleSpawn);
    }

    private void OnRequestSingleSpawn(Vector3 pos, float scaleFactor, bool activate = true)
    {
        BallController _newBall = Instantiate(ballPrefab, pos, Quaternion.identity, PlayAreaController.Instance.transform).GetComponent<BallController>();
        _newBall.Init(pos, scaleFactor, activate);
    }

    private void OnRequestBalls(int numOfBalls, SpawnArea spawnArea, bool activate)
    {
        StartCoroutine(SpawnBalls(numOfBalls, spawnArea, 0.25f, activate));
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
