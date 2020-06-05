using UnityEngine;
using UnityEngine.Events;

public class RequestSingleSpawn : UnityEvent<Vector3, float, bool>
{
}
public class BallSpawnPoint : MonoBehaviour
{
    public float ballStartScale;
    
    public static RequestSingleSpawn requestSingleSpawn = new RequestSingleSpawn();
    public void Init()
    {
        requestSingleSpawn.Invoke(transform.position, ballStartScale, false);
    }
}
