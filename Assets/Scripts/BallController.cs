using UnityEngine;
using GameData;
using UnityEngine.Events;
using Zenject;

public class BallController : MonoBehaviour
{
    [Inject] private GameParameters gameParameters;
    
    private Rigidbody body;

    private bool isActive = false;
    
    
    public static UnityEvent announceBallDespawned = new UnityEvent();
    public static UnityEvent announceSpawned = new UnityEvent();
    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        InputController.inputStartEvent.AddListener(HandleTouchStarted);
        InputController.inputChangeEvent.AddListener(HandleTouchMoved);
        InputController.inputEndEvent.AddListener(HandleTouchEnded);
        announceSpawned.Invoke();

        isActive = true;

    }

    public void Init (Vector3 pos, float scaleFactor)
    {
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    private void HandleTouchEnded(Vector3 arg0)
    {
        
    }

    private void HandleTouchMoved(Vector3 moveChange)
    {
        float _moveChange = Mathf.Clamp(moveChange.x, -gameParameters.moveChangeRange, gameParameters.moveChangeRange);
        float _addedForce = _moveChange * gameParameters.forceMultiplier;
        
        body.AddForce(new Vector3(0f, 0f, _addedForce), ForceMode.Impulse);
    }

    private void HandleTouchStarted(Vector3 arg0)
    {
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (!isActive)
            return;
        
    }

    private void OnDestroy()
    {
        announceBallDespawned.Invoke();
        InputController.inputStartEvent.RemoveListener(HandleTouchStarted);
        InputController.inputChangeEvent.RemoveListener(HandleTouchMoved);
        InputController.inputEndEvent.RemoveListener(HandleTouchEnded);
    }
}
