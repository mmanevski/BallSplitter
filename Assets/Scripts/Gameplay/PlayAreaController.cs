using System;
using UnityEngine;
using Zenject;

public class PlayAreaController : SingletonBehavior<PlayAreaController>
{
    [Inject] private GameParameters gameParameters; 
    
    public Transform playAreaHolder;
    private Vector3 startRotation;


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

    private void HandleTouchMoved(Vector3 moveChange)
    {
        float _moveChange = Mathf.Clamp(moveChange.x, -gameParameters.moveChangeRange, gameParameters.moveChangeRange);
        float _rotX = startRotation.x + _moveChange *gameParameters.rotationFactor;
        
        playAreaHolder.rotation = Quaternion.Euler(startRotation.x, -_rotX, startRotation.z);
        
        //playAreaHolder.RotateAround(playAreaHolder.position, Vector3.left, -moveChange.x * 0.25f);
    }

    private void HandleTouchStarted(Vector3 arg0)
    {
        startRotation = playAreaHolder.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
