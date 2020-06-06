using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class RequestSingleSpawn : UnityEvent<Vector3, float, bool, Transform>
{
}
public class BallSpawnPoint : MonoBehaviour
{
    [Inject] private GameParameters gameParameters;
    
    public static RequestSingleSpawn requestSingleSpawn = new RequestSingleSpawn();
    public void Init(Transform parent)
    {
        requestSingleSpawn.Invoke(transform.position, gameParameters.maxBallScale, false, parent);
    }
}
