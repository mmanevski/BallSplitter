using UnityEngine;
using GameData;
using UnityEngine.Events;
using Zenject;

public class BallController : MonoBehaviour
{
    [Inject] private GameParameters gameParameters;
    
    private Rigidbody body;

    private bool isActive = false;
    private BallSplitter parentSplitter;
    
    public static UnityEvent announceBallDespawned = new UnityEvent();
    public static UnityEvent announceSpawned = new UnityEvent();
    private void Awake()
    {
        PlayAreaController.announceAngle.AddListener(OnAngleChanged);
        body = GetComponent<Rigidbody>();
        body.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    void Start()
    {
        InputController.inputStartEvent.AddListener(HandleTouchStarted);
        announceSpawned.Invoke();

    }

    public void Init(Vector3 pos, float scaleFactor, bool activate, BallSplitter splitter = null)
    {
        transform.localPosition = pos;
        parentSplitter = splitter;
        if (activate)
        {
            
            ActivateRigidbody();
        }
        transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
    }

    private void OnAngleChanged(Vector3 angle)
    {
        
        //float _moveChange = Mathf.Clamp(moveChange.x, -gameParameters.moveChangeRange, gameParameters.moveChangeRange);
        float _playAreaRotationY = Utils.WrapAngle(angle.y);
        float _addedForce = -_playAreaRotationY * gameParameters.forceMultiplier;
        
        body.AddForce(new Vector3(0f, 0f, _addedForce), ForceMode.Impulse);

    }

    private void HandleTouchStarted(Vector3 arg0)
    {
        ActivateRigidbody();
    }

    private void ActivateRigidbody()
    {
        if (!isActive)
        {
            isActive = true;
            body.constraints = RigidbodyConstraints.None;
        }
    }
    public void Despawn()
    {
        //announceBallDespawned.Invoke();
        Destroy(gameObject, 0.1f);
    }

    private void OnDestroy()
    {
        announceBallDespawned.Invoke();
        InputController.inputStartEvent.RemoveListener(HandleTouchStarted);
    }

    public BallSplitter GetParentSplitter()
    {
        return parentSplitter;
    }
}
