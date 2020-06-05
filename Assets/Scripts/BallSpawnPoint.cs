using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class RequestSingleSpawn : UnityEvent<Vector3, float, bool>
{
}
public class BallSpawnPoint : MonoBehaviour
{
    [Inject] private GameParameters gameParameters;
    
    public static RequestSingleSpawn requestSingleSpawn = new RequestSingleSpawn();
    public void Init()
    {
        requestSingleSpawn.Invoke(transform.position, gameParameters.maxBallScale, false);
    }
}
