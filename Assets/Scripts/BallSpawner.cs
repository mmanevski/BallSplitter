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
    }

    private void OnRequestBalls(int numOfBalls, SpawnArea spawnArea)
    {
        StartCoroutine(SplitBalls(numOfBalls, spawnArea, 0.25f));
    }

    private void SpawnBalls(int numOfBalls, Vector3 pos, float _scaleFactor)
    {
        for (int i = 0; i < numOfBalls; i++)
        {
            //TODO: Refactor
            BallController _newBall = Instantiate(ballPrefab, pos, Quaternion.identity, PlayAreaController.Instance.transform).GetComponent<BallController>();
            _newBall.Init(pos, _scaleFactor);
        }
    }

    private IEnumerator SplitBalls(int numOfBalls, SpawnArea spawnArea, float _scaleFactor)
    {
        for (int i = 0; i < numOfBalls; i++)
        {
            Vector3 spawnPos = new Vector3();

            float spawnPosZBound = spawnArea.rightBoundry;
            float spawnPosYBound = spawnArea.upperBoundry;

            float spawnPosZ = Random.Range(spawnArea.transform.position.z - spawnPosZBound, spawnArea.transform.position.z + spawnPosZBound);

            spawnPos = new Vector3(1f, spawnArea.transform.position.y, spawnPosZ);

            BallController _newBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity, PlayAreaController.Instance.transform).GetComponent<BallController>();
            _newBall.Init(spawnPos, _scaleFactor);
            yield return new WaitForSeconds(0.05f);
        }

    }
}
