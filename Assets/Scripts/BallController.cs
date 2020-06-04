using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody body;
    private float moveChangeRange = 0.4f;
    private float forceMultiplier = 10f;

    private void Awake()
    {
        body = GetComponent<Rigidbody>();
    }
    
    void Start()
    {
        InputController.inputStartEvent.AddListener(HandleTouchStarted);
        InputController.inputChangeEvent.AddListener(HandleTouchMoved);
        InputController.inputEndEvent.AddListener(HandleTouchEnded);
        
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
        float _moveChange = Mathf.Clamp(moveChange.x, -moveChangeRange, moveChangeRange);
        float _addedForce = _moveChange * 10f;
        
        body.AddForce(new Vector3(0f, 0f, _addedForce), ForceMode.Impulse);
    }

    private void HandleTouchStarted(Vector3 arg0)
    {
    }


    private void OnDestroy()
    {
        InputController.inputStartEvent.RemoveListener(HandleTouchStarted);
        InputController.inputChangeEvent.RemoveListener(HandleTouchMoved);
        InputController.inputEndEvent.RemoveListener(HandleTouchEnded);
    }
}
