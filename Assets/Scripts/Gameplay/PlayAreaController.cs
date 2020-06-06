using System;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class AnnounceAngle : UnityEvent<Vector3>
{
    
}

public class PlayAreaController : SingletonBehavior<PlayAreaController>
{
    [Inject] private GameParameters gameParameters; 
    
    public Transform playAreaHolder;
    private Vector3 startRotation;
    private float startRotY;
    
    public static AnnounceAngle announceAngle = new AnnounceAngle();

    // Start is called before the first frame update
    void Start()
    {
        InputController.inputStartEvent.AddListener(HandleTouchStarted);
        InputController.inputChangeEvent.AddListener(HandleTouchMoved);
        InputController.inputEndEvent.AddListener(HandleTouchEnded);
        
    }

    private void HandleTouchEnded(Vector3 arg0)
    {
        //throw new System.NotImplementedException();
    }

    private void HandleTouchMoved(Vector3 moveChangeVec)
    {
        float _moveChange = Mathf.Clamp(moveChangeVec.x, -gameParameters.moveChangeRange, gameParameters.moveChangeRange);
        
        float _rotY = -startRotY + _moveChange *gameParameters.rotationFactor;
        _rotY = Utils.ClampAngle(-_rotY, -gameParameters.playAreaRotationRange, gameParameters.playAreaRotationRange);
        Debug.Log("Rotation: " + _rotY);
        Vector3 _newRotation = new Vector3(startRotation.x, _rotY, startRotation.z);
        
        if (moveChangeVec.y > 0.5f)
            playAreaHolder.eulerAngles = new Vector3(startRotation.x, _rotY, 180f);

        announceAngle.Invoke(_newRotation);
        playAreaHolder.eulerAngles = _newRotation;

    }

    private void HandleTouchStarted(Vector3 arg0)
    {
        startRotation = playAreaHolder.rotation.eulerAngles;
        startRotY = Utils.ClampAngle(startRotation.y, -gameParameters.playAreaRotationRange,
            gameParameters.playAreaRotationRange);
    }

}
