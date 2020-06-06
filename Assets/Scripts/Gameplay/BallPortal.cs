using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEngine;

public class BallPortal : MonoBehaviour
{
    public SpawnArea exitArea;
    public static RequestSingleSpawn requestSingleSpawn = new RequestSingleSpawn();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(ObjectTags.ballTag))
        {
            Vector3 spawnPos = exitArea.transform.position;
            
            float spawnPosZBound = exitArea.rightBoundry;
            float spawnPosZ = Random.Range(exitArea.transform.position.z - spawnPosZBound, exitArea.transform.position.z + spawnPosZBound);

            spawnPos = new Vector3(exitArea.transform.position.x, exitArea.transform.position.y, spawnPosZ);
            
            other.GetComponent<BallController>().Despawn();
            requestSingleSpawn.Invoke(spawnPos, other.transform.localScale.x, true, GameManager.currentLevel.transform);

        }
    }
    
}
